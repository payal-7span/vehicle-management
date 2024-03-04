using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Service.Models
{
    public class APIResult
    {
        public bool Success { get; set; }
        public ServiceError? Error { get; set; }
    }

    public class APIResult<T> : APIResult
    {
        public required T Data { get; set; }
    }
}
