using App7.Helpers;
using App7.Model;
using App7.Services;
using App7.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App7.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportPage : ContentPage
	{
        private Tasks myTasks;
        ReportViewModel rp=new ReportViewModel();
        APIServices api = new APIServices();

        public List<JsonReportStatus> _status;

        public List<JsonReportStatus> status
        {
            get { return _status; }
            set
            {
                _status = value;
            }
        }

        public ReportPage()
		{
			InitializeComponent();
            BindingContext = new ReportViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public async void GetReportStatus()
        {
            status = await api.GetReportStatus(Settings.accessToken);
            foreach (var item in status)
            {
                statusPicker.Items.Add(item.Name);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        public ReportPage(Tasks myTasks)
        {
            InitializeComponent();
            rp = new ReportViewModel();
            BindingContext = rp;
            this.myTasks = myTasks;
            rp.TaskId = myTasks.Id;
            rp.EmployeeId = myTasks.Employee.Id;
            GetReportStatus();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            commentText.Text = string.Empty;
            costText.Text = string.Empty;
            statusPicker.Items.Add(string.Empty);
        }

        private void StatusPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = statusPicker.Items[statusPicker.SelectedIndex];
            rp.Status = name;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
        
        private async void Send_Clicked(object sender, EventArgs e)
        {
            rp.ReportCommand.Execute(null);
            bool re= await DisplayAlert("", "Report sent success", "OK", "Cancel");
            if (re)
            {
               await Navigation.PushAsync(new TaskDetailsPage(rp.TaskId));
                Navigation.RemovePage(this);
            }
        }
    }
}