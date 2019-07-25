using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using WhiteMvvm.Bases;
using WhiteMvvm.Configuration;
using WhiteMvvm.Exceptions;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Locator;
using WhiteMvvm.Services.Navigation;
using Xamarin.Forms;

namespace WhiteMvvm.Services.Resolve
{
    public class ReflectionResolve : IReflectionResolve
    {
        public Page CreatePage(Type viewModelType)
        {
            var viewFolderName = ConfigurationManager.Current.ViewsFolderName;
            var viewModelFolderName = ConfigurationManager.Current.ViewModelFolderName;
            var viewFileName = ConfigurationManager.Current.ViewsFileName;
            var viewModelFileName = ConfigurationManager.Current.ViewModelFileName;
            var pageType = GetTypeFromAssembly(viewModelType, viewModelFolderName, viewFolderName, viewModelFileName, viewFileName);
            var page = GetPageInstance(pageType);
            return page;
        }

        public BaseContentView CreateContentView(string componentName, string layoutType)
        {
            var assemblyName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
            var viewType = GetTypeFromAssembly(componentName, layoutType, assemblyName);
            var view = GetViewInstance(viewType);
            return view as BaseContentView;
        }
        public BaseViewModel CreateViewModelFromView(Type viewType)
        {
            var viewModelFileName = ConfigurationManager.Current.ViewModelFileName;
            var viewmodelType = GetTypeFromAssembly(viewType, viewModelFileName);
            var viewModel = GetViewModelInstance(viewmodelType);
            return viewModel;
        }
        public BaseViewModel CreateViewModelFromPage(Type pageType)
        {
            var viewFolderName = ConfigurationManager.Current.ViewsFolderName;
            var viewModelFolderName = ConfigurationManager.Current.ViewModelFolderName;
            var viewFileName = ConfigurationManager.Current.ViewsFileName;
            var viewModelFileName = ConfigurationManager.Current.ViewModelFileName;

            var viewmodelType = GetTypeFromAssembly(pageType, viewFolderName, viewModelFolderName, viewFileName, viewModelFileName);
            var viewModel = GetViewModelInstance(viewmodelType);
            return viewModel;
        }
        private static Type GetTypeFromAssembly(Type typeFrom, string folderNameToBeReplaced,
            string replacedFolderName, string fileNameToBeReplaced, string replacedFileName)
        {
            if (typeFrom == null || string.IsNullOrEmpty(typeFrom.Namespace) ||
                string.IsNullOrEmpty(typeFrom.Name))
            {
                throw new Exception($"Cannot locate page or view type for this view model");
            }
            var fileName = typeFrom.Name.Replace(fileNameToBeReplaced, replacedFileName);
            var namespaceName = typeFrom.Namespace.Replace(folderNameToBeReplaced, replacedFolderName);

            var fileFullName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, fileName);
            var assemblyName = typeFrom.Assembly.FullName;
            var fileAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", fileFullName, assemblyName);

            var newType = Type.GetType(fileAssemblyName);
            return newType;
        }
        private static Type GetTypeFromAssembly(Type typeFrom, string replacedFileName)
        {
            if (typeFrom == null || string.IsNullOrEmpty(typeFrom.Namespace) ||
                string.IsNullOrEmpty(typeFrom.Name))
            {
                throw new Exception($"Cannot locate page or view type for this view model");
            }

            var fileFullName = "";
            var assemblyQualifiedName = typeFrom.AssemblyQualifiedName;
            if (assemblyQualifiedName != null)
            {
                var separatedAssemblyQualifiedName = assemblyQualifiedName.Split('.');
                var fileName = separatedAssemblyQualifiedName[2] + replacedFileName;
                fileFullName = separatedAssemblyQualifiedName[0] + "." + separatedAssemblyQualifiedName[1] + "." +
                               separatedAssemblyQualifiedName[2] + "." + fileName;
            }

            var assemblyName = typeFrom.Assembly.FullName;
            var fileAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", fileFullName, assemblyName);
            var newType = Type.GetType(fileAssemblyName);
            return newType;
        }
        private static Type GetTypeFromAssembly(string componentName, string layoutType, string assemblyName)
        {
            var contentViewFileName = ConfigurationManager.Current.ContentViewsFileName;
            var contentViewFolderNameName = ConfigurationManager.Current.ContentViewsFolderName;

            var fileFullName = assemblyName + "." + contentViewFolderNameName + "." +
                               componentName + "." + componentName + layoutType + contentViewFileName;
            var fileAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", fileFullName, assemblyName);
            var newType = Type.GetType(fileAssemblyName);
            return newType;
        }
        private static Page GetPageInstance(Type pageType)
        {
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for this view model");
            }

            var page = Activator.CreateInstance(pageType);
            return page as Page;
        }
        private static ContentView GetViewInstance(Type viewType)
        {
            if (viewType == null)
            {
                throw new ReflectionResolveException("Cannot locate view type for this view model");
            }
            try
            {
                var view = Activator.CreateInstance(viewType);
                return view as ContentView;
            }
            catch(Exception exception)
            {
                throw new ReflectionResolveException("ContentView not found",exception);
            }
        }
        private static BaseViewModel GetViewModelInstance(Type viewModelType)
        {
            if (viewModelType == null)
            {
                throw new Exception($"Cannot locate view model type for this page");
            }
            var viewModel = LocatorService.Instance.Resolve(viewModelType) as BaseViewModel;
            return viewModel;
        }
    }
}
