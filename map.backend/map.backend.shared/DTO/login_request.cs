using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class login_request
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập!")]
        public string userid { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string password { get; set; }
    }
}
