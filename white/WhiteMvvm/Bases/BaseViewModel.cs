using System;
using System.Threading.Tasks;
using WhiteMvvm.Configuration;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Locator;
using WhiteMvvm.Services.Logging;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Utilities;
using Xamarin.Forms;

namespace WhiteMvvm.Bases
{
    public class BaseViewModel : NotifiedObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly IConnectivity ConnectivityService;
        protected readonly IMainThread MainThreadService;

        protected readonly ILoggerService LoggerService;
        private volatile bool _isInitialize;
        private bool _isBusy = true;
        private volatile bool _isOnAppeared;
        private string _title;
        private string _icon;
        private string _linkText;
        public object NavigationData { get; private set; }
        public BaseViewModel()
        {
            DialogService = LocatorService.Instance.Resolve<IDialogService>();
            NavigationService = LocatorService.Instance.Resolve<INavigationService>();
            ConnectivityService = LocatorService.Instance.Resolve<IConnectivity>();
            MainThreadService = LocatorService.Instance.Resolve<IMainThread>();
            LoggerService = LocatorService.Instance.Resolve<ILoggerService>();
        }

        private string _itemLink;

        public string ItemLink
        {
            get => _itemLink;
            set
            {
                _itemLink = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }
        public string Link { get; set; }
        public Task LoadData(string uri)
        {
            var isMain = MainThreadService.IsMainThread;
            if (isMain)
            {
                OnLoadData(uri);
            }
            else
            {
                MainThreadService.BeginInvokeOnMainThread(() =>
                {
                    Task.Run(() => { OnLoadData(uri); });
                });

            }
            return Task.CompletedTask;
        }
        protected virtual Task OnLoadData(string uri)
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnNavigateFrom(BaseViewModel page, object parameter)
        {
            return Task.CompletedTask;
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (ConfigurationManager.Current.UseBaseIndicator)
                    {
                        if (value)
                        {
                            DialogService.ShowLoading(ConfigurationManager.Current.IndicatorMaskType);
                        }
                        else
                        {
                            DialogService.HideLoading();
                        }
                    }
                    _isBusy = value;
                    OnPropertyChanged();
                });
            }
        }
        public string LinkText
        {
            get => _linkText;
            set
            {
                _linkText = value;
                OnPropertyChanged();
            }
        }
        protected internal virtual Task OnPopupAppearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnPopupDisappearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnDisappearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task InitializeAsync(object navigationData)
        {
            NavigationData = navigationData;
            return Task.CompletedTask;
        }
        protected internal virtual Task InitializeVolatileAsync(object navigationData)
        {
            NavigationData = navigationData;
            return Task.CompletedTask;
        }
        internal void InternalInitialize(object navigationData)
        {
            InitializeVolatileAsync(navigationData);
            if (_isInitialize)
                return;
            _isInitialize = true;
            InitializeAsync(navigationData);
        }
        protected virtual Task OnAppeared()
        {
            return Task.CompletedTask;
        }
        internal void InternalOnAppeared()
        {
            if (_isOnAppeared)
                return;
            _isOnAppeared = true;
            OnAppeared();
        }
        protected internal virtual bool HandleBackButton()
        {
            return true;
        }
        public void OnPagePopup()
        {
            NavigationService.OnPagePopup(this, new EventArgs());
        }
    }
}
