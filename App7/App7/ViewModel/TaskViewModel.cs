using Acr.UserDialogs;
using App7.Helpers;
using App7.Model;
using App7.Services;
using Newtonsoft.Json;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace App7.ViewModel
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        APIServices apiSevices = new APIServices();
        public event PropertyChangedEventHandler PropertyChanged;
        private JsonTasks _oldTask;

        public ObservableCollection<JsonTasks> _tasks;

        public ObservableCollection<JsonTasks> tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged("tasks");
            }
        }
        
        public string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
        
        public ICommand GetTasks
        {
            get
            {
                return new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(async() => {
                        using(UserDialogs.Instance.Loading("Please wait.."))
                        {
                            tasks = await apiSevices.GetTasks(Settings.accessToken);
                            if (tasks.Count == 0)
                            {
                                Message = "No pending tasks";
                            }
                        }
                       
                    });
                });
            }
        }

        public void HideOrShow(JsonTasks jsonTask)
        {
            if (_oldTask == jsonTask)
            {
                jsonTask.IsVisible = !jsonTask.IsVisible;
                Update(jsonTask);
            }
            else
            {
                if (_oldTask != null)
                {
                    _oldTask.IsVisible = false;
                    Update(_oldTask);
                }
                jsonTask.IsVisible = true;
                Update(jsonTask);
            }
            _oldTask = jsonTask;
        }

        private void Update(JsonTasks jsonTask)
        {
            var index = Settings.index;
            
            foreach(var item in tasks)
            {
                if (tasks.Contains(jsonTask))
                {
                    tasks.Remove(jsonTask);
                    tasks.Insert(index, jsonTask);
                    break;
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}