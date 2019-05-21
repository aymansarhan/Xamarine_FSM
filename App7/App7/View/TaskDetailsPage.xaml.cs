using Acr.UserDialogs;
using Android.Locations;
using Android.Opengl;
using App7.Helpers;
using App7.Model;
using App7.Services;
using App7.ViewModel;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App7.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskDetailsPage : ContentPage, INotifyPropertyChanged
    {
        private int id;
        public APIServices api = new APIServices();
        Tasks MyTasks;
        decimal latitude;
        decimal longitude;
        bool checkGPS;
        ObservableCollection<JsonTasks> tasksList;
        public event PropertyChangedEventHandler PropertyChanged;
        TaskDetailsViewModel taskviewmodel = new TaskDetailsViewModel();

        public TaskDetailsPage()
		{
			InitializeComponent();
            BindingContext = taskviewmodel;
            tasksList = new ObservableCollection<JsonTasks>();
        }
        
        public TaskDetailsPage(int id)
        {
            InitializeComponent();
            this.id = id;
            GetTaskDetails(id);
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CrossConnectivity.Current.IsConnected)
            {
                App.Database.GetReports();
            }
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                else if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(10));
                    latitude = Convert.ToDecimal(results.Latitude);
                    longitude = Convert.ToDecimal(results.Longitude);
                    checkGPS = CrossGeolocator.Current.IsGeolocationEnabled;
                }
            }
            catch (Exception ex)
            {
                //CrossToastPopUp.Current.ShowToastMessage("GPS Not Enabled");
            }
        }
        
        public async void GetTaskDetails(int id)
        {
            MyTasks = await api.GetTasksById(id, Settings.accessToken);
            
            if (MyTasks.employeeStatusOnJob == "Assigned")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    actions.Text = "Start";
                    report.IsEnabled = false;
                });
            }
            
            if (MyTasks.employeeStatusOnJob == "On Way")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    actions.Text = "Check In";
                });
            }

            if (MyTasks.employeeStatusOnJob == "Checked In")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    actions.Text = "Check Out";
                    report.IsEnabled = true;
                    report.Text = "Report";
                });
            }

            Id.Text = MyTasks.Id.ToString();
            CategoryName.Text = MyTasks.Category.Name;
            TypeName.Text = MyTasks.Type.Name;
            ProjectNumber.Text = MyTasks.ProjectNumber;
            ScheduledAt.Text = MyTasks.ScheduledAt.ToString();
            TransportationMeanName.Text = MyTasks.TransportationMean.Name;
            Description.Text = MyTasks.Description;
            customername.Text = MyTasks.customer.Name;
            customeraliasName.Text = MyTasks.customer.AliasName;
            customeraddress.Text = MyTasks.customer.Address;
            branchname.Text = MyTasks.Branch.Name;
        }

        private async void actions_Clicked(object sender, EventArgs e)
        {
            EmployeeStatusOnTask employeeStatus = new EmployeeStatusOnTask();
            employeeStatus.Status = MyTasks.employeeStatusOnJob;
            tasksList = await api.GetTasks(Settings.accessToken);
            actions.IsEnabled = false;

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                else if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(10));
                    latitude = Convert.ToDecimal(results.Latitude);
                    longitude = Convert.ToDecimal(results.Longitude);
                    checkGPS = CrossGeolocator.Current.IsGeolocationEnabled;
                }
            }
            catch (Exception ex)
            {
            }

            if (!checkGPS)
            {
               await DisplayAlert("Notify", "GPS IS NOT ENABLED", "OK");
            }

            else
            {
                foreach (var item in tasksList)
                {
                    foreach (var items in item.jobs)
                    {
                        if (items.employeeStatusOnJob == "Checked In" || items.employeeStatusOnJob == "On Way")
                        {
                            if (items.Id != MyTasks.Id)
                            {

                                bool result = await DisplayAlert("Notify", "You have another task with ID: " + items.Id, "OK", "Cancel");
                                if (result)
                                {
                                    await Navigation.PushAsync(new TaskDetailsPage(items.Id));
                                    Navigation.RemovePage(this);
                                    break;
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                using(UserDialogs.Instance.Loading("Please wait.."))
                                {
                                    ActionsRequest();
                                }
                                break;
                            }
                        }
                        else
                        {
                            using (UserDialogs.Instance.Loading("Please wait.."))
                            {
                                ActionsRequest();
                            }
                            break;
                        }
                    }
                    break;
                }
               
            }
        }

        private async void ActionsRequest()
        {
            EmployeeStatusOnTask employeeStatus = new EmployeeStatusOnTask();
            TaskDetailsViewModel detailsViewModel = new TaskDetailsViewModel();
            detailsViewModel.status = MyTasks.employeeStatusOnJob;
            employeeStatus.Status = MyTasks.employeeStatusOnJob;
            
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                else if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(10));
                    latitude = Convert.ToDecimal(results.Latitude);
                    longitude = Convert.ToDecimal(results.Longitude);
                    checkGPS = CrossGeolocator.Current.IsGeolocationEnabled;
                }
            }
            catch (Exception ex)
            {
               //CrossToastPopUp.Current.ShowToastMessage("GPS Not Enabled");
            }

            if (checkGPS)
            {
                if (employeeStatus.Status == "Assigned")
                {
                    actions.Text = "Check In";
                    employeeStatus.Status = "OnWay";
                    CrossToastPopUp.Current.ShowToastMessage("Start Success");
                    var EmployeeStatus = new EmployeeStatusOnTask
                    {
                        Status = employeeStatus.Status,
                        Latitude = latitude,
                        Longitude = longitude,
                        Time = DateTime.Now,
                        taskId = id,
                        Speed = 0,
                        distanceInKM = 20,
                        batteryStatus = 0,
                        gpsIsOpened = checkGPS,
                        networkIsAvailable = true
                    };
                    using(UserDialogs.Instance.Loading("Please wait.."))
                    {
                        MyTasks = await api.GetAndUpdate(id, Settings.accessToken, EmployeeStatus);
                    }
                    OnPropertyChanged("EmployeeStatus");
                }

                else if (employeeStatus.Status == "On Way")
                {
                    actions.Text = "Check Out";
                    report.Text = "Report";
                    report.IsEnabled = true;
                    employeeStatus.Status = "CheckedIn";
                    var EmployeeStatus = new EmployeeStatusOnTask
                    {
                        Status = employeeStatus.Status,
                        Latitude = latitude,
                        Longitude = longitude,
                        Time = DateTime.Now,
                        taskId = id,
                        Speed = 0,
                        distanceInKM = 20,
                        batteryStatus = 0,
                        gpsIsOpened = checkGPS,
                        networkIsAvailable = true
                    };
                    using (UserDialogs.Instance.Loading("Please wait"))
                    {
                        await api.UpdateStatusAndGetTasks(EmployeeStatus, Settings.accessToken);
                    }
                    actions.IsEnabled = true;
                    OnPropertyChanged("EmployeeStatus");

                }

                else if (employeeStatus.Status == "Checked In")
                {
                    employeeStatus.Status = "CheckedOut";
                    var EmployeeStatus = new EmployeeStatusOnTask
                    {
                        Status = employeeStatus.Status,
                        Latitude = latitude,
                        Longitude = longitude,
                        Time = DateTime.Now,
                        taskId = id,
                        Speed = 0,
                        distanceInKM = 20,
                        batteryStatus = 0,
                        gpsIsOpened = checkGPS,
                        networkIsAvailable = true
                    };
                    using (UserDialogs.Instance.Loading("Please wait..."))
                    {
                        MyTasks = await api.GetAndUpdate(id, Settings.accessToken, EmployeeStatus);
                    }
                    actions.IsEnabled = true;
                    OnPropertyChanged("EmployeeStatus");
                    bool result = await DisplayAlert("", "Task Finished.", "OK", "Cancel");
                    if (result)
                    {
                        await Navigation.PushAsync(new HomePage());
                        Navigation.RemovePage(this);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
            }
        }
        
        protected virtual void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async void report_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new ReportPage(MyTasks));
            Navigation.RemovePage(this);
        }
    }
}