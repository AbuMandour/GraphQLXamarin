using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WhiteMvvm;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Bases;
using WhiteMvvmUnitTest.Mocks;
using WhiteMvvmUnitTest.Mocks.ViewModels;
using WhiteMvvmUnitTest.Mocks.Views;
using Xamarin.Forms;
using WhiteMvvm.Services.Locator;

namespace WhiteMvvmUnitTest.Services
{
    [TestClass]
    public class NavigationServicesTest : BaseTest
    {
        [TestMethod]
        public void InitializeAppNavigationWithContentPage()
        {
            //Arrange   
            var app = new MockApp();
            //Act
            MockApp.SetHomePage<HomeViewModel>();            
            //Assert
            var homePage = app.MainPage.BindingContext.GetType() == typeof(HomeViewModel);
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void InitializeAppNavigationWithTabbedPage()
        {
            //Arrange                      
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true, Parameter = "Home Page", ViewModel = new HomeViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page One", ViewModel = new View1ViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page two", ViewModel = new View2ViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page three", ViewModel = new View3ViewModel()
                }
            };
            var app = new MockApp();
            //Act
            MockApp.SetHomePage(pageContainers);
            //Assert
            var tabbedIsExists = app.MainPage.GetType() == typeof(TabbedPage);
            Assert.IsTrue(tabbedIsExists);
        }
        [TestMethod]
        public void InitializeAppNavigationWithMasterDetialPage()
        {
            //Arrange                      
            var detialPage = new PageContainer() { IsNavigationPage = true, Parameter = "Detail Page", ViewModel = new HomeViewModel(), PageName = "Detail Page" };
            var masterPage = new PageContainer() { IsNavigationPage = false, Parameter = "Master Page", ViewModel = new View1ViewModel(), PageName = "Master Page" };
            Application.Current = new MockApp();
            //Act
            MockApp.SetHomePage(masterPage,detialPage);
            //Assert
            var masterIsExists = Application.Current.MainPage.GetType() == typeof(MasterDetailPage);
            Assert.IsTrue(masterIsExists);
        }
        [TestMethod]
        public void NavigateTo_WithOutParameter()
        {
            //Arrange   
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            //Act
            MockApp.SetHomePage<View1ViewModel>(isNavigationPage: true);
            navigationService.NavigateToAsync<HomeViewModel>();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateTo_WithParameter()
        {
            //Arrange  
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            //Act
            MockApp.SetHomePage<View1ViewModel>(isNavigationPage:true);
            navigationService.NavigateToAsync<HomeViewModel>("Hello Test");
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            var homeViewModel = (HomeViewModel)homePage?.BindingContext;
            //Assert
            Assert.IsTrue((string)homeViewModel?.NavigationData == "Hello Test");
            Assert.IsNotNull(homePage);            
        }
        [TestMethod]
        public void NavigateToModal_WithOutParameter()
        {
            //Arrange          
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            //Act
            MockApp.SetHomePage<View1ViewModel>();
            navigationService.NavigateToModalAsync<HomeViewModel>();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateToModal_WithParameter()
        {
            //Arrange           
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            //Act
            MockApp.SetHomePage<View1ViewModel>();
            navigationService.NavigateToModalAsync<HomeViewModel>("Hello Test");
            var homePage = MockApp.Current.MainPage.Navigation.ModalStack.LastOrDefault(x => x.GetType() == typeof(HomePage));           
            var homeViewModel = (HomeViewModel)homePage?.BindingContext;
            //Assert
            Assert.IsNotNull(homePage);
            Assert.IsTrue((string)homeViewModel?.NavigationData == "Hello Test");            
        }
        [TestMethod]
        public void NavigateToTabbedPage()
        {
            //Arrange
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true,
                    Parameter = "Home Page",
                    ViewModel = LocatorService.Instance.Resolve<HomeViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page One",
                    ViewModel = LocatorService.Instance.Resolve<View1ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page two",
                    ViewModel = LocatorService.Instance.Resolve<View2ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page three",
                    ViewModel = LocatorService.Instance.Resolve<View3ViewModel>()
                }
            };
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            //Act
            MockApp.SetHomePage<HomeViewModel>();
            var navigateToTabbedAsyncTask = navigationService.NavigateToTabbedAsync(pageContainers);
            var isTabbedExists = ModalStack.Any(x => x.GetType() == typeof(TabbedPage)) ||
                                 NavigationStack.Any(x => x.GetType() == typeof(TabbedPage));

            //Assert
            Assert.IsTrue(navigateToTabbedAsyncTask.IsCompleted);
            Assert.IsTrue(isTabbedExists);
        }
        [TestMethod]
        public void NavigateToMasterDetailPage()
        {
            //Arrange
            var app = new MockApp();
            var masterPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Master Page",
                ViewModel = LocatorService.Instance.Resolve<HomeViewModel>()
            };
            var detailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Detail Page",
                ViewModel = LocatorService.Instance.Resolve<HomeViewModel>()
            }; ;
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            //Act
            MockApp.SetHomePage<HomeViewModel>();
            var pushMasterDetailsTask = navigationService.NavigateToMasterDetailsAsync(masterPage,detailPage);
            var isMasterDetailExists = ModalStack.Any(x => x.GetType() == typeof(MasterDetailPage)) ||
                                       NavigationStack.Any(x => x.GetType() == typeof(MasterDetailPage));
            //Assert
            Assert.IsTrue(pushMasterDetailsTask.IsCompleted);
            Assert.IsTrue(isMasterDetailExists);
        }
        [TestMethod]
        public  void ChangeDetailPageInMasterDetailTest()
        {
            //Arrange
            var masterPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Master Page",
                ViewModel = LocatorService.Instance.Resolve<HomeViewModel>()
            };
            var detailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Detail Page",
                ViewModel = LocatorService.Instance.Resolve<HomeViewModel>()
            };
            var newDetailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "New Detail Page",
                ViewModel = LocatorService.Instance.Resolve<HomeViewModel>()
            };
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            //Act
            MockApp.SetHomePage(masterPage, detailPage);
            var isDetailPageChanged =  navigationService.ChangeDetailPage(newDetailPage,null);
            //Assert
            Assert.IsTrue(isDetailPageChanged);
        }
        [TestMethod]
        public void AddTabbedPageOnRunTime()
        {
            //Arrange              
            var navigationService = LocatorService.Instance.Resolve<INavigationService>();
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true, Parameter = "Home Page", ViewModel = new HomeViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page One", ViewModel = new View1ViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page two", ViewModel = new View2ViewModel()
                }                
            };
            var newPage = new PageContainer()
            {
                IsNavigationPage = false,
                Parameter = "Page three",
                ViewModel = new View3ViewModel()
            };
            //Act
            MockApp.SetHomePage(pageContainers);            
            var isPageAdded = navigationService.AddPageToTabbedPage(newPage);
            //Assert
            Assert.IsTrue(isPageAdded);
        }        
    }
}
