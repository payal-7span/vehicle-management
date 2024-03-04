using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Service.Models;

namespace VM.Service.Services
{
    public interface IFeesHeadService : IBaseService<FeesHeads>
    {
    }
    public class FeesHeadService(HttpClient httpClient) : BaseService<FeesHeads>(httpClient), IFeesHeadService
    {
    }
}
