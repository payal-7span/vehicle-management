using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Service.Models;

namespace VM.Service.Services
{
    public interface IFeesStructureService : IBaseService<FeesStructures>
    {
        Task<APIResult<List<FeesStructures>>?> GetFeesStructureByTypeIdAsync(int id);
    }
    public class FeesStructureService(HttpClient httpClient) : BaseService<FeesStructures>(httpClient), IFeesStructureService
    {
        public async Task<APIResult<List<FeesStructures>>?> GetFeesStructureByTypeIdAsync(int id)
        {
            var response = await httpClient.GetAsync($"FeesStructures/by-type-id/{id}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<APIResult<List<FeesStructures>>>(json);
                return apiResult;
            }
            return null;
        }
    }
}
