using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;

namespace GraphQLDemo.ViewModels
{
    public class HttpClientViewModel : BaseViewModel
    {
        protected override bool HandleBackButton()
        {
            MainThreadService.BeginInvokeOnMainThread((async () =>
            {
                await NavigationService.PopModelAsync();
            }));
            return true;
        }

    }
}
