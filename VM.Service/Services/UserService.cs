using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VM.Service.Models;

namespace VM.Service.Services
{
    public interface IUserService : IBaseService<Users>
    {
        Task<APIResult<Users>?> SetPasswordAsync(Users data);
    }
    public class UserService(HttpClient httpClient) : BaseService<Users>(httpClient), IUserService
    {
        public async Task<APIResult<Users>?> SetPasswordAsync(Users data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"Users/SetPassword", content);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<Users>>(json);
                return apiResult;
            }
            return null;
        }
    }
}
