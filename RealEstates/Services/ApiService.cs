using Microsoft.Win32;
using Newtonsoft.Json;
using RealEstateApp.Models;
using RealEstates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password, string phone)
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
                PostAsync(AppSettings.ApiUrl + "/api/users/register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            { Email = email, Password = password };
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(login);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.
                PostAsync(AppSettings.ApiUrl + "/api/users/login", content);
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
        public static async Task<List<Category>?> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/categories");
            return response != null ? JsonConvert.DeserializeObject<List<Category>>(response) : null;
        }

        public static async Task<List<TrendingProperty>?> GetTrendingProperties()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Properties/TrendingProperties");
            return response != null ? JsonConvert.DeserializeObject<List<TrendingProperty>>(response) : null;
        }
        public static async Task<List<SearchProperty>?> FindProperties(string address)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Properties/SearchProperties?address="+ address);
            return response != null ? JsonConvert.DeserializeObject<List<SearchProperty>>(response) : null;
        }
        public static async Task<List<PropertyByCategory>?> GetPropertyByCategory(int categoryid)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Properties/PropertyList?categoryId=" + categoryid);
            return response != null ? JsonConvert.DeserializeObject<List<PropertyByCategory>>(response) : null;
        }

        public static async Task<PropertyDetail?> GetPropertyDetail(int propertyid)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Properties/PropertyDetail?id=" + propertyid);
            return response != null ? JsonConvert.DeserializeObject<PropertyDetail>(response) : null;
        }
        public static async Task<List<BookmarkList>?> GetBookMarkList()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/bookmarks");
            return response != null ? JsonConvert.DeserializeObject<List<BookmarkList>>(response) : null;
        }
        public static async Task<bool> AddBookmark(AddBookmark addBookmark)
        {        
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(addBookmark);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.
                PostAsync(AppSettings.ApiUrl + "/api/bookmarks", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> RemoveBookmark(int bookMarkId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "/api/bookmarks/" + bookMarkId);
            if(!response.IsSuccessStatusCode) return false;
            return true;
        }


    }
}
