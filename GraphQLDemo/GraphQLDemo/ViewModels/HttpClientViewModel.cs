using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GraphQLDemo.Models;
using GraphQLDemo.Transitionals;
using WhiteMvvm.Bases;
using WhiteMvvm.Utilities;

namespace GraphQLDemo.ViewModels
{
    public class HttpClientViewModel : BaseViewModel
    {
        protected override async Task<bool> HandleBackButton()
        {
            await NavigationService.PopModelAsync();
            return true;
        }
    }
}
