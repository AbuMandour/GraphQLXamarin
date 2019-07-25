using System;
using Acr.UserDialogs;

namespace WhiteMvvm.Configuration
{
    public sealed class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> Lazy = new Lazy<ConfigurationManager>(() => new ConfigurationManager());
        private string _viewsFolderName;
        private string _viewModelFolderName;
        private string _viewsFileName;
        private string _loadingDisplay;
        private string _viewModelFileName;


        public static ConfigurationManager Current => Lazy.Value;
        private ConfigurationManager()
        {
        }
        public bool UseBaseIndicator { get; set; } = true;
        public MaskType IndicatorMaskType { get; set; } = MaskType.Gradient;
        public string ContentViewsFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewsFolderName))
                    return "ContentViews";
                return _viewsFolderName;
            }
            set => _viewsFolderName = value;
        }
        public string ContentViewsFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewsFileName))
                    return "View";
                return _viewsFileName;
            }
            set => _viewsFileName = value;
        }
        public string ViewsFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewsFolderName))
                    return "Views";
                return _viewsFolderName;
            }
            set => _viewsFolderName = value;
        }
        public string ViewsFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewsFileName))
                    return "Page";
                return _viewsFileName;
            }
            set => _viewsFileName = value;
        }
        public string ViewModelFolderName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewModelFolderName))
                    return "ViewModels";
                return _viewModelFolderName;
            }
            set => _viewModelFolderName = value;
        }
        public string ViewModelFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_viewModelFileName))
                    return "ViewModel";
                return _viewModelFileName;
            }
            set => _viewModelFileName = value;
        }
        public string LoadingDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(_loadingDisplay))
                    return "Loading...";
                return _loadingDisplay;
            }
            set => _loadingDisplay = value;
        }
    }
}
