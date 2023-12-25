using Xamarin.Forms;
using App2.Data;
using App2.Views;
using System;
using System.IO;

namespace App2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(DitalFilePage), typeof(DitalFilePage));

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
