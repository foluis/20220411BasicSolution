using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_02_11.Entities.Models
{
    public class LoginResponse : BaseResponse
    {
        public string AccessToken { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
