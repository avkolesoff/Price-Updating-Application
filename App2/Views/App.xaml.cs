using Xamarin.Forms;
using App2.Data;
using System.IO;
using System;

namespace App2
{

    public partial class App : Application
    {
        static EstimateFileDB estimateFileDB;
        public static EstimateFileDB EstimateFileDB
        {
            get
            {
                if (estimateFileDB == null)
                {
                    estimateFileDB = new EstimateFileDB(
                        Path.Combine(Environment.GetFolderPath(Environment
                        .SpecialFolder
                        .LocalApplicationData), "EstimateFilesDatabase.db3"));
                }
                return estimateFileDB;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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
