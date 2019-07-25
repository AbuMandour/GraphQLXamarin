using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using WhiteMvvm.Utilities;

namespace WhiteMvvm.Services.Api
{
    public class GraphQLApiService : IApiService
    {
        public Task<TBaseTransitional> Get<TBaseTransitional>(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null) where TBaseTransitional : BaseTransitional
        {
            throw new NotImplementedException();
        }

        public Task<TransitionalList<TBaseTransitional>> GetList<TBaseTransitional>(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null) where TBaseTransitional : BaseTransitional
        {
            throw new NotImplementedException();
        }

        public Task<TransitionalList<TBaseTransitional>> GetListAsString<TBaseTransitional>(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null) where TBaseTransitional : BaseTransitional
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> Post<TResponse, TRequest>(TRequest entity, Dictionary<string, string> headers, string contentType, string uri) where TResponse : class where TRequest : BaseTransitional
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> PostWithOutContent<TResponse>(Dictionary<string, string> headers, string contentType, string uri) where TResponse : class
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRedirect(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null)
        {
            throw new NotImplementedException();
        }
    }
}
