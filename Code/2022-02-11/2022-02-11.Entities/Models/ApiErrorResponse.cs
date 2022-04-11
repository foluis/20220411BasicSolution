using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_02_11.Entities.Models
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public bool IsSuccess { get; set; }

        //public Userinfo UserInfo { get; set; }
        //public DateTime ExpireDate { get; set; }
    }
}
