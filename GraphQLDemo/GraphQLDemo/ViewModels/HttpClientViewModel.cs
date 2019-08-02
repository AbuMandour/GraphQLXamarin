using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GraphQLDemo.Models;
using GraphQLDemo.Transitionals;
using Newtonsoft.Json;
using WhiteMvvm.Bases;
using WhiteMvvm.Utilities;

namespace GraphQLDemo.ViewModels
{
    public class HttpClientViewModel : BaseViewModel
    {
        private async Task<ObservableRangeCollection<Quiz>> GetQuizList()
        {
            var jsonSerializer = new JsonSerializer();
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync("");
            httpResponse.EnsureSuccessStatusCode();
            using (var stream = await httpResponse.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                var quizListApi = jsonSerializer.Deserialize<List<QuizApi>>(json);
                var quizList = quizListApi.ToModel<Quiz>();
                return new ObservableRangeCollection<Quiz>(quizList);
            }
            //await Task.Delay(2000);
            //var quizList = new new ObservableRangeCollection<Quiz>()
            //{

            //}
        }
        protected override async Task<bool> HandleBackButton()
        {
            await NavigationService.PopModelAsync();
            return true;
        }
    }
}
