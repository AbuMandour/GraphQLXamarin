using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Remotion.Linq.Clauses;
using WhiteMvvm.Bases;
using WhiteMvvm.Exceptions;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Services.Resolve;
using WhiteMvvm.Utilities;
using Xamarin.Essentials;

namespace WhiteMvvm.Services.Api
{
    public class HttpApiService : IApiService
    {
        private readonly IMainThread _mainThread;
        private readonly HttpClient _httpClient = new HttpClient(new HttpClientHandler()
        {
            ClientCertificateOptions = ClientCertificateOption.Automatic
        });
        private readonly JsonSerializer _serializer = new JsonSerializer();

        private List<CancellationTokenSource> _cancellationTokens;
        private List<CancellationTokenSource> CancellationTokens
        {
            get
            {
                if (_cancellationTokens != null)
                    return _cancellationTokens;
                _cancellationTokens = new List<CancellationTokenSource>();
                return _cancellationTokens;

            }
        }
        public HttpApiService(INavigationService navigationService, IMainThread mainThread)
        {
            _mainThread = mainThread;
            navigationService.PagePopup += _navigationService_PagePopup;
        }
        private void _navigationService_PagePopup(object sender, EventArgs e)
        {
            foreach (var tokenSource in CancellationTokens)
            {
                tokenSource.Cancel();
            }
        }
        public async Task<TBaseTransitional> Get<TBaseTransitional>(Dictionary<string, string> headers, string uri,
            Dictionary<string, string> param = null) where TBaseTransitional : BaseTransitional
        {
            try
            {
                var tokenSource = new CancellationTokenSource();
                CancellationTokens.Add(tokenSource);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
                if (uri == "668cadf9-0517-456a-a9a2-0bf13d073dc3")
                {
                    using (var stream = await GetJsonAsStream<TBaseTransitional>("AppConfig.json"))
                    using (var reader = new StreamReader(stream))
                    using (var json = new JsonTextReader(reader))
                    {
                        return _serializer.Deserialize<TBaseTransitional>(json);
                    }
                }
                var fullUri = GetFullUrl(uri, param);
                var response = await _httpClient.GetAsync(fullUri, tokenSource.Token);

                response.EnsureSuccessStatusCode();
                CancellationTokens.Remove(tokenSource);

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<TBaseTransitional>(json);
                }
            }
            catch(Exception exception)
            {
                throw new Exception("Error while get date from internet",exception);
            }
        }
        public async Task<TransitionalList<TBaseTransitional>> GetList<TBaseTransitional>(
            Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null)
            where TBaseTransitional : BaseTransitional
        {
            try
            {
                var tokenSource = new CancellationTokenSource();
                CancellationTokens.Add(tokenSource);

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
                var fullUri = GetFullUrl(uri, param);

                var response = await _httpClient.GetAsync(fullUri, tokenSource.Token);

                response.EnsureSuccessStatusCode();
                CancellationTokens.Remove(tokenSource);

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {                    
                    var list = _serializer.Deserialize<TransitionalList<TBaseTransitional>>(json);
                    return list;
                }
            }
            catch (Exception exception)
            {

                throw new ApiException("Unable to get list ", exception);
            }
        }
        /// <summary>
        /// get list from api link as string first then parse to json then serialize to list of transitional list
        /// </summary>
        /// <typeparam name="TBaseTransitional"></typeparam>
        /// <param name="headers"></param>
        /// <param name="uri"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TransitionalList<TBaseTransitional>> GetListAsString<TBaseTransitional>(
            Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null)
            where TBaseTransitional : BaseTransitional
        {
            try
            {
                var tokenSource = new CancellationTokenSource();
                CancellationTokens.Add(tokenSource);

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
                var fullUri = GetFullUrl(uri, param);

                var response = await _httpClient.GetAsync(fullUri, tokenSource.Token);

                response.EnsureSuccessStatusCode();
                CancellationTokens.Remove(tokenSource);

                var jsonAsString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<TransitionalList<TBaseTransitional>>(jsonAsString);
                return list;
            }
            catch (Exception exception)
            {

                throw new ApiException("Unable to get list ", exception);
            }
        }
        public async Task<TResponse> Post<TResponse, TRequest>(TRequest entity,
            Dictionary<string, string> headers, string contentType, string uri) where TRequest : BaseTransitional
            where
            TResponse : class
        {
            var client = new HttpClient();
            if (headers != null)
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            StringContent dateContent = null;
            if (entity != null)
            {
                var json = JsonConvert.SerializeObject(entity);
                dateContent = new StringContent(json, Encoding.UTF8, contentType);
            }
            var response = await client.PostAsync(uri, dateContent);
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<TResponse>(jsonString);
            return jsonObject;

        }
        public async Task<TResponse> PostWithOutContent<TResponse>(Dictionary<string, string> headers, string contentType, string uri) where
            TResponse : class
        {
            var client = new HttpClient();
            if (headers != null)
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            var response = await client.PostAsync(uri, null);
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<TResponse>(jsonString);
            return jsonObject;
        }
        public string GetFullUrl(string uri, Dictionary<string, string> param)
        {
            StringBuilder query = new StringBuilder().Append("?");
            if (param == null || param.Count <= 0)
            {
                return !string.IsNullOrEmpty(uri) ? uri : "";
            }

            KeyValuePair<string, string> lastElement = param.ElementAt(param.Count - 1);
            foreach (KeyValuePair<string, string> item in param)
            {
                bool flag = item.Key == lastElement.Key;
                if (!flag)
                {
                    query.Append(item.Key + "=" + item.Value).Append("&");
                }
                else
                {
                    query.Append(item.Key + "=" + item.Value);
                }
            }
            return $"{uri}/{query}";
        }
        public async Task<string> GetRedirect(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null)
        {
            try
            {
                var tokenSource = new CancellationTokenSource();
                CancellationTokens.Add(tokenSource);

                var httpClientHandler = new HttpClientHandler { AllowAutoRedirect = false };

                var client = new HttpClient(httpClientHandler);

                client.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
                var fullUri = GetFullUrl(uri, param);
                var response = await client.GetAsync(fullUri, tokenSource.Token);
                var stringResponse = await response.Content.ReadAsStringAsync();

                CancellationTokens.Remove(tokenSource);

                return stringResponse;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        private async Task<Stream> GetJsonAsStream<TBaseTransitional>(string jsonFileName) where TBaseTransitional : BaseTransitional
        {
            try
            {
                var assembly = typeof(TBaseTransitional).Assembly;
                await Task.Delay(5000);
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
                return stream;
            }
            catch
            {
                throw new Exception("Error while get json from stream");
            }
        }
    }
}
