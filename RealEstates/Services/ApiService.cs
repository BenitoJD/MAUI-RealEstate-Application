using Microsoft.Win32;
using Newtonsoft.Json;
using RealEstates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public class ApiService
    {
        public async Task<bool> RegisterUser(string name, string email, string password, string phone)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone
            };
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(register);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.
                PostAsync("https://realestateapps-a0fccpg2gaftcgdk.canadacentral-01.azurewebsites.net/api/users/register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            { Email = email, Password = password };
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(login);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.
                PostAsync("https://realestateapps-a0fccpg2gaftcgdk.canadacentral-01.azurewebsites.net/api/users/login", content);
            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            if (result != null)
            {
                Preferences.Set("accessToken", result.AccessToken);
                Preferences.Set("userId", result.UserId);
                Preferences.Set("userName", result.UserName);
            }
            return true;
        }
    }
}
