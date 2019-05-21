using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using App7.Model;
using System.Collections.ObjectModel;
using App7.Helpers;
using Acr.UserDialogs;
using Plugin.Geolocator;
using Plugin.Connectivity;
using Android.Net;

namespace App7.Services
{
    public class APIServices
    {
        public string accessToken;

        public async Task<string> LoginAysnc(string username, string password)
        {
            var results = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(10));
            decimal latitude = Convert.ToDecimal(results.Latitude);
            decimal longitude = Convert.ToDecimal(results.Longitude);

            var KeyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_Id", "f0fc04db181f4d53b1560851ebf3b23d"),
                new KeyValuePair<string, string>("isappuser", true.ToString().ToLower()),
                new KeyValuePair<string, string>("SerialSIMCard", Settings.serialNum),
                new KeyValuePair<string, string>("IMEICode", Settings.IMNum),
                new KeyValuePair<string, string>("Latitude", latitude.ToString()),
                new KeyValuePair<string, string>("Longitude", longitude.ToString()),
                new KeyValuePair<string, string>("gcmKey", Settings.notificationToken)
            };

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://tracker-magconsulting.azurewebsites.net/Token");
                request.Content = new FormUrlEncodedContent(KeyValues);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                var response = await client.SendAsync(request);
                var jwt = await response.Content.ReadAsStringAsync();
                JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);
                accessToken = jwtDynamic.Value<string>("access_token");
                var Roles = jwtDynamic.Value<string>("roles");
                var expire = jwtDynamic.Value<DateTime>(".expires");
                var employeeId = jwtDynamic.Value<int>("employeeId");
                //error_description
                var errorMessage = jwtDynamic.Value<string>("error_description");
                var UserName = jwtDynamic.Value<string>("userName");
                var Email = jwtDynamic.Value<string>("email");
                var empName = jwtDynamic.Value<string>("employeeName");
                Settings.Empname = empName;
                Settings.email = Email;
                Settings.username = UserName;
                Settings.expire = expire;
                Settings.roles = Roles;
                Settings.id = employeeId;
                Settings.error = errorMessage;

            }

            catch (Exception ex)
            {
            }

            return accessToken;
        }

        public async Task<ObservableCollection<JsonTasks>> GetTasks(string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = await client.GetStringAsync("http://tracker-magconsulting.azurewebsites.net/api/Employees/MyTasks");
            var values = JsonConvert.DeserializeObject<ObservableCollection<JsonTasks>>(json);

            return values;

        }

        public async Task<Tasks> GetTasksById(int Id, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var json = await client.GetStringAsync("http://tracker-magconsulting.azurewebsites.net/api/tasks/" + Id);
            var values = JsonConvert.DeserializeObject<Tasks>(json);

            return values;

        }

        public async Task<Employee> GetEmployeeById(int Id, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync("http://tracker-magconsulting.azurewebsites.net/api/Employees/" + Id);
            var values = JsonConvert.DeserializeObject<Employee>(json);
            return values;
        }

        public async Task<Tasks> GetAndUpdate(int Id, string accessToken, EmployeeStatusOnTask emp)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json2 = JsonConvert.SerializeObject(emp);
            HttpContent content = new StringContent(json2);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //using(UserDialogs.Instance.Loading("Please wait.."))
            //{
                var response = await client.PostAsync("http://tracker-magconsulting.azurewebsites.net/api/Employees/updateEmployeeStatus", content);
                var json = await client.GetStringAsync("http://tracker-magconsulting.azurewebsites.net/api/tasks/" + Id);
                var values = JsonConvert.DeserializeObject<Tasks>(json);
                return values;
            //}
        }
        
        public async Task<List<JsonReportStatus>> GetReportStatus(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var json = await client.GetStringAsync("http://tracker-magconsulting.azurewebsites.net/api/ReportStatus");
            var values = JsonConvert.DeserializeObject<List<JsonReportStatus>>(json);

            return values;
            
        }

        public async Task UpdateStatus(EmployeeStatusOnTask emp, string accessToken)
        {
            using(UserDialogs.Instance.Loading("Please wait.."))
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    ("Bearer", accessToken);
                var json = JsonConvert.SerializeObject(emp);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync("http://tracker-magconsulting.azurewebsites.net/api/Employees/updateEmployeeStatus", content);

            }
        }

        public async Task UpdateStatusAndGetTasks(EmployeeStatusOnTask emp, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(emp);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("http://tracker-magconsulting.azurewebsites.net/api/Employees/updateEmployeeStatusAndGetTaskById", content);
        }

        public async Task CreateReport(Report report, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", accessToken);
            var json = JsonConvert.SerializeObject(report);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("http://tracker-magconsulting.azurewebsites.net/api/reports", content);
        }

        public async Task ReportList(List<Report> ListReports, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Bearer", accessToken);
            var json = JsonConvert.SerializeObject(ListReports);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("http://tracker-magconsulting.azurewebsites.net/api/reports/reportList", content);
        }
    }
}