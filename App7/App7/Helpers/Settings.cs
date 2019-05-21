// Helpers/Settings.cs
using App7.Model;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;

namespace App7.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }


        public static string username
        {
            get
            {
                return AppSettings.GetValueOrDefault("username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("username", value);
            }
        }

        public static string Empname
        {
            get
            {
                return AppSettings.GetValueOrDefault("Empname", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Empname", value);
            }
        }

        public static string error
        {
            get
            {
                return AppSettings.GetValueOrDefault("error", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("error", value);
            }
        }

        public static string email
        {
            get
            {
                return AppSettings.GetValueOrDefault("email", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("email", value);
            }
        }

        public static string Number
        {
            get
            {
                return AppSettings.GetValueOrDefault("Number", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Number", value);
            }
        }

        public static string password
        {
            get
            {
                return AppSettings.GetValueOrDefault("password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("password", value);
            }
        }

        public static string accessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("accessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("accessToken", value);
            }
        }
       
        public static string roles
        {
            get
            {
                return AppSettings.GetValueOrDefault("roles", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("roles", value);
            }
        }
        
        public static DateTime expire
        {
            get
            {
                return AppSettings.GetValueOrDefault("expire", DateTime.Now);
            }
            set
            {
                AppSettings.AddOrUpdateValue("expire", value);
            }
        }

        public static string serialNum
        {
            get
            {
                return AppSettings.GetValueOrDefault("serialNum", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("serialNum", value);
            }
        }

        public static string notificationToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("notificationToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("notificationToken", value);
            }
        }

        public static string IMNum
        {
            get
            {
                return AppSettings.GetValueOrDefault("IMNum", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("IMNum", value);
            }
        }

        public static int id
        {
            get
            {
                return AppSettings.GetValueOrDefault("id", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("id", value);
            }
        }

        public static int index
        {
            get
            {
                return AppSettings.GetValueOrDefault("index", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("index", value);
            }
        }

    }
}