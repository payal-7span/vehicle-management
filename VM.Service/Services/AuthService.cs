using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VM.Service.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VM.Service.Services
{

    public interface IAuthService
    {
        Task<APIResult<LoginResult>> LoginAsync(Login data);
    }
    public class AuthService: IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string endPoint;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            endPoint = typeof(Login).Name;
        }
        public async Task<APIResult<LoginResult>> LoginAsync(Login data)
        {

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"Auth/{endPoint}", content);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<LoginResult>>(json);
                return apiResult;
            }
            return null;

            throw new NotImplementedException();
        }
    }
}
