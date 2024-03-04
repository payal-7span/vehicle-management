using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Service.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; } = false;
        public string NewPassword { get; set; } = string.Empty;
    }
}
