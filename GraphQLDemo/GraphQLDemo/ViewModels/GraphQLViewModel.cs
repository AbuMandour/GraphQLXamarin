using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;

namespace GraphQLDemo.ViewModels
{
    public class GraphQLViewModel : BaseViewModel
    {
        protected override async Task<bool> HandleBackButton()
        {
            await NavigationService.PopModelAsync();
            return true;
        }
    }
}
