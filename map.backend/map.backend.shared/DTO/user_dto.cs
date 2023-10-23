using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class user_dto
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string img { get; set; }
        public string rolecode { get; set; }
        public string rolename { get; set; }
    }
}
