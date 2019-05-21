using Acr.UserDialogs;
using Android.Content.Res;
using App7.Helpers;
using App7.Model;
using App7.Services;
using App7.Sqlite;
using App7.View;
using Plugin.Connectivity;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App7.ViewModel
{
    public class ReportViewModel
    {
        private APIServices apiServices = new APIServices();

        public DateTime Time { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public decimal Cost { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public int EmployeeId { get; set; }

        public int TaskId { get; set; }
        
        public List<Report> reports { get; set; }
        
        public ICommand ReportCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        Latitude = Convert.ToDecimal(location.Latitude);
                        Longitude = Convert.ToDecimal(location.Longitude);
                    }

                    Report report = new Report
                    {
                        Time = DateTime.Now,
                        Comment = Comment,
                        Cost = Cost,
                        Status = Status,
                        TaskId = TaskId,
                        EmployeeId = EmployeeId,
                        Latitude = Latitude,
                        Longitude = Longitude,
                    };
                    //using(UserDialogs.Instance.Loading("Please wait.."))
                    //{
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            await apiServices.CreateReport(report, Settings.accessToken);
                        }
                        else
                        {
                            App.Database.SaveReports(report);
                        }
                    //}
                    
                    //await App.Current.MainPage.DisplayAlert("", "Sending Report Success", "OK");
                }
                );
            }
        }
    }
}