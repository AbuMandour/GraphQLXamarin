using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using WhiteMvvm.Utilities;

namespace GraphQLDemo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand ToGraphQLCommand { get; set; }
        public ICommand ToGraphQLWithHttpClintCommand { get; set; }
        public ICommand ToHttpClientCommand { get; set; }

        public MainViewModel()
        {
            ToGraphQLCommand = new TaskCommand((OnNavigateToGraphQl));
            ToGraphQLWithHttpClintCommand = new TaskCommand(OnNavigateToGraphQLWithHttpClient);
            ToHttpClientCommand = new TaskCommand(OnNavigateHttpClient);
        }

        private Task OnNavigateHttpClient(object arg)
        {
            return NavigationService.NavigateToModalAsync<HttpClientViewModel>();
        }

        private Task OnNavigateToGraphQLWithHttpClient(object arg)
        {
            return NavigationService.NavigateToModalAsync<GraphQLWithHttpClientViewModel>();
        }

        private Task OnNavigateToGraphQl(object arg)
        {
            return NavigationService.NavigateToModalAsync<GraphQLViewModel>();
        }
    }
}
