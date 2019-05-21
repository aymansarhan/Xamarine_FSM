using App7.Model;
using App7.Services;
using App7.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App7.Helpers;
using Plugin.Connectivity;
using Plugin.Toast;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;

namespace App7.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        APIServices api = new APIServices();
        TaskViewModel taskview = new TaskViewModel();

        public HomePage()
		{
			InitializeComponent();
            BindingContext = new TaskViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = taskview;
            taskview.GetTasks.Execute(null);
            if (CrossConnectivity.Current.IsConnected)
            {
                App.Database.GetReports();
            }
        }

        private void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Tasks item = (Tasks)((ListView)sender).SelectedItem;
            ((ListView)sender).SelectedItem = null;
            Navigation.PushAsync(new TaskDetailsPage(item.Id));
        }

        private void TasksList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as TaskViewModel;
            var tasks = e.Item as JsonTasks;
            Settings.index = e.ItemIndex;
            vm.HideOrShow(tasks);
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            BindingContext = taskview;
            taskview.GetTasks.Execute(null);
        }
    }
}