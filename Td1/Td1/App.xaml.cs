using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Td1.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Td1
{
    public partial class App : Application

    {
        public static RestService restService { get; set; }

        public App()
        {

            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());

            restService = new RestService();

           // MainPage = new MainPage();
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

        /*public static RestService RestService
        {
            get
            {
                if(restService == null)
                {
                    restService = new RestService();
                }
                return restService;
            }
        }*/

    }
}
