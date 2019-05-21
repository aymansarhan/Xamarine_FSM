using Android.Content.Res;
using App7.Helpers;
using App7.Services;
using App7.View;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Connectivity;
using Android.Telephony;
using App7.Model;
using Acr.UserDialogs;
using Plugin.Geolocator;

namespace App7.ViewModel
{
    public class LoginViewModel
    {
        private APIServices apiServices = new APIServices();

        public string username { get; set; }
        public string password { get; set; }
        
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (CrossGeolocator.Current.IsGeolocationEnabled)
                    {
                        try
                        {
                            using (UserDialogs.Instance.Loading("Please wait..."))
                            {
                                var accessToken = await apiServices.LoginAysnc(username, password);
                                Settings.accessToken = accessToken;
                                Employee emp = await apiServices.GetEmployeeById(Settings.id, Settings.accessToken);

                                try
                                {
                                    await SecureStorage.SetAsync("username", username);
                                    await SecureStorage.SetAsync("password", password);
                                    await SecureStorage.SetAsync("token", accessToken);
                                }

                                catch (Exception ex)
                                {
                                    // Possible that device doesn't support secure storage on device.
                                }

                                if (accessToken == null)
                                {

                                    CrossToastPopUp.Current.ShowToastMessage("Username or password incorrect!");

                                }
                                else
                                {
                                    if (emp.SerialSIMCard != Settings.serialNum || emp.IMEI != Settings.IMNum)
                                    {
                                        CrossToastPopUp.Current.ShowToastMessage("This is not the same SIM card!");
                                    }

                                    else if (accessToken != null && (emp.SerialSIMCard != null && emp.SerialSIMCard == Settings.serialNum) && (emp.IMEI != null && emp.IMEI == Settings.IMNum))
                                    {
                                        CrossToastPopUp.Current.ShowToastMessage("Login success");
                                        await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(), true);
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            CrossToastPopUp.Current.ShowToastMessage(Settings.error);
                            //"SerialSIMCard or IMEICode already taken for another User"
                        }
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("GPS MUST BE ENABLED!");
                    }

                }
                );
            }
        }

        public ICommand Logout
        {
            get
            {
                return new Command(() =>
                {
                    Settings.accessToken = "";
                    Settings.username = "";
                    Settings.password = "";
                    SecureStorage.Remove("username");
                    SecureStorage.Remove("password");
                    CrossToastPopUp.Current.ShowToastMessage("Logged Out");

                    App.Current.MainPage = new LoginPage();
                });
            }
        }

        public LoginViewModel()
        {
            username = Settings.username;
            password = Settings.password;
        }
    }
}
