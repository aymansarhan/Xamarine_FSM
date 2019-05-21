using Acr.UserDialogs;
using App7.Helpers;
using App7.Model;
using App7.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App7.ViewModel
{
    public class TaskDetailsViewModel : INotifyPropertyChanged
    {
        APIServices apiServices = new APIServices();
        
        public int taskId { get; set; }
        public string status { get; set; }
        public DateTime time { get; set; }
        public decimal speed { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsEnabled { get; set; }

        public ICommand UpdateTaskStatus
        {
            get
            {
                return new Command(async() =>
                {
                    using (UserDialogs.Instance.Loading("Please wait..."))
                    {
                        var tasks = new EmployeeStatusOnTask
                        {
                            taskId = taskId,
                            Status = status,
                            Time = time,
                            Speed = speed,
                            Latitude = Latitude,
                            Longitude = Longitude,
                        };
                        await apiServices.UpdateStatusAndGetTasks(tasks, Settings.accessToken);
                    }
                });
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}