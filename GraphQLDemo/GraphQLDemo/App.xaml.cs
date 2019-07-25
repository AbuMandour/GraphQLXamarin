using GraphQLDemo.ViewModels;
using System;
using WhiteMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GraphQLDemo
{
    public partial class App : WhiteApplication
    {
        public App()
        {
            InitializeComponent();
            SetHomePage<MainViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
