using System;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace WhiteMvvm.Services.Resolve
{
    public interface IReflectionResolve
    {
        BaseViewModel CreateViewModelFromPage(Type pageType);
        Page CreatePage(Type viewModelType);
        BaseViewModel CreateViewModelFromView(Type viewType);
        BaseContentView CreateContentView(string componentName, string layoutType);

    }
}
