using App7.View;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Plugin.Connectivity;
using Plugin.Toast;
using App7.Sqlite;
using System.IO;
using SQLite;
using App7.Services;
using App7.Helpers;
using System.Collections.Generic;
using App7.Model;
using App7.ViewModel;
using Android.Telephony;
using Android.Net;
using Plugin.Permissions;
using Android.Content.PM;
using Plugin.Permissions.Abstractions;
using Permission = Plugin.Permissions.Abstractions.Permission;
using Com.OneSignal;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App7
{
    public partial class App : Application
    {
        public static ReportDB database;
        private APIServices apiServices = new APIServices();
        public List<Reports> lisrofReports = new List<Reports>();
        ReportViewModel vm = new ReportViewModel();

        public App()
        {
            InitializeComponent();
            try
            {
                var oauthToken = SecureStorage.GetAsync("username");
                var oauthToken1 = SecureStorage.GetAsync("password");
                var token = SecureStorage.GetAsync("token");
                var expires = Settings.expire;
                var email = Settings.email;
                var user = Settings.username;

                if (CrossConnectivity.Current.IsConnected)
                {
                    if (oauthToken.Result != null && oauthToken1.Result != null && expires >= DateTime.Today)
                    {
                        MainPage = new MainPage();
                        
                    }

                    else
                    {
                        MainPage = new LoginPage();
                    }
                }
                else
                {
                    MainPage = new OfflinePage();
                }
               
            }

            catch(Exception ex)
            {

            }
        }
        
        protected override void OnStart()
        {
           // AppCenter.Start("d6911f84-ee63-41d6-91b2-a8caf53aab87", typeof(Push));
        }
        
        public static ReportDB Database
        {
            get
            {
                if (database == null)
                {
                    var DbName = "Reports.db3";
                    var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DbName);
                    database = new ReportDB(path);
                }
                
                return database;
                
            }
        }
        
        protected override void OnSleep()
        {
            //Handle when your app sleeps
            //System.Environment.Exit(0);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
