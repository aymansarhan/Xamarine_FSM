using App7.MenuItems;
using App7.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using App7.Helpers;
using App7.ViewModel;
using Plugin.Connectivity;
using App7.Services;
using App7.Model;

namespace App7
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }
        APIServices api = new APIServices();

        public MainPage()
        {
            InitializeComponent();

            menuList = new List<MasterPageItem>();
            menuList.Add(new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType=typeof(HomePage)});
            menuList.Add(new MasterPageItem() { Title = "Roles"+"\n"+Settings.roles, Icon = "roles.png"});
            menuList.Add(new MasterPageItem() { Title = "Historical", Icon = "historical.png"});
            menuList.Add(new MasterPageItem() { Title = "Logout", Icon = "logout.png"});

            navigationDrawerList.ItemsSource = menuList;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
            //username.Text = Settings.username;
            //email.Text = Settings.email;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            if (item.Title == "Logout")
            {
                var vm = new LoginViewModel();
                vm.Logout.Execute(null);
            }
            else if (item.Title == "Home")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            }
            IsPresented = false;
        }
    }
}