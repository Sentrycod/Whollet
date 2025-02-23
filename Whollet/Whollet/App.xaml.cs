﻿using System;
using Xamarin.Essentials;
using Whollet.Model.Helpers;
using Whollet.Views;
using Whollet.Views.FirstTimeInApp;
using Whollet.Views.KYC;
using Whollet.Views.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Whollet.Model;
using Whollet.Views.CamViews;

[assembly: ExportFont("TitilliumWeb-Black.tff", Alias ="TitilliumNormal")]
[assembly: ExportFont("TitilliumWeb-Bold.tff", Alias ="TitilliumBold")]
[assembly: ExportFont("TitilliumWeb-BoldItalic.tff", Alias ="TitilliumBoldItallic")]
[assembly: ExportFont("TitilliumWeb-ExtraLight.tff", Alias ="TitilliumExtraLight")]
[assembly: ExportFont("TitilliumWeb-ExtraLightItalic.tff", Alias ="TitilliumExtraLightItallic")]
[assembly: ExportFont("TitilliumWeb-Italic.tff", Alias ="TitilliumItallic")]
[assembly: ExportFont("TitilliumWeb-Light.tff", Alias ="TitilliumLight")]
[assembly: ExportFont("TitilliumWeb-LightItalic.tff", Alias ="TitilliumLightItallic")]
[assembly: ExportFont("TitilliumWeb-Regular.tff", Alias ="TitilliumRegular")]
[assembly: ExportFont("TitilliumWeb-SemiBold.tff",Alias ="TitilliumSemiBold")]
[assembly: ExportFont("TitilliumWeb-SemiBoldItalic.tff", Alias ="TitilliumSemiBoldItallic")]

namespace Whollet
{
    public partial class App : Application
    {
        public App()
        {
            
            DevExpress.XamarinForms.Editors.Initializer.Init();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjE5NTk1QDMyMzAyZTMxMmUzMFp2V1JraXZiN2lPd3doUm4xSnozRThVaEdJa1NjSnIyaThWMlF4c2F2QVk9");
            InitializeComponent();
            Startup.ConfigureServices();
            //NTczNTcxQDMxMzkyZTM0MmUzMFZ4ODIxR29WNnNIdjVSMFlyVDg1NmF3dGpsdkNIMTRhRTFqSWhiRFRSSjg9
            MainPage = new NavigationPage(Startup.Resolve<WelcomeView>());
        }

        private static readonly string Databasename = "WholletDatabase.db3";
        static Database getDatabase;
        public static Database GetDatabase
        {
            get
            {
                if (getDatabase == null)
                {
                    getDatabase = new Database(Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory,Databasename));
                }
                return getDatabase;
            }
        }

        private static User getUser;

        public static User LoggedInUser
        {
            get 
            {
                return getUser;
            }
            set { getUser = value; }
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
