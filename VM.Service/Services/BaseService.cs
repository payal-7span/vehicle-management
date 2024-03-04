using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Service.Models;

namespace VM.Service.Services
{
    public interface IBaseService<T>
    {
        Task<APIResult<List<T>>?> GetAllAsync();
        Task<APIResult<T>?> GetByIdAsync(int id);
        Task<APIResult<T>?> ModifyByIdAsync(int id, T data);
        Task<APIResult<T>?> CreateAsync(T data);
        Task<APIResult<T>?> DeleteAsync(int id);
    }
    public class BaseService<T> : IBaseService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string endPoint;
        public BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            endPoint = typeof(T).Name;
        }

        public async Task<APIResult<List<T>>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(endPoint);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<List<T>>>(json);
                return apiResult;
            }
            return null;
        }

        public async Task<APIResult<T>?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{endPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<T>>(json);
                return apiResult;
            }
            return null;
        }

        public async Task<APIResult<T>?> ModifyByIdAsync(int id, T data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{endPoint}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<T>>(json);
                return apiResult;
            }
            return null;
        }
        public async Task<APIResult<T>?> CreateAsync(T data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endPoint, content);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<T>>(json);
                return apiResult;
            }
            return null;
        }
        public async Task<APIResult<T>?> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{endPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<T>>(json);
                return apiResult;
            }
            return null;
        }
    }
}
