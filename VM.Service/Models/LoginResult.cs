using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Service.Models
{
    public class LoginResult
    {
        public string token { get; set; }
        public Users user { get; set; }
    }
}
