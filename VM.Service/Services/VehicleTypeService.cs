using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VM.Service.Models;

namespace VM.Service.Services
{
    public interface IVehicleTypeService : IBaseService<VehicleType>
    {
    }
    public class VehicleTypeService(HttpClient httpClient) : BaseService<VehicleType>(httpClient), IVehicleTypeService
    {
    }
}
