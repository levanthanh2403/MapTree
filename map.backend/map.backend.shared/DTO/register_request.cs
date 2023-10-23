using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class register_request
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập!")]
        public string userId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng!")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu!")]
        public string rePassword { get; set; }
        //[DataType(DataType.EmailAddress, ErrorMessage = "Email không đúng định dạng!")]
        public string email { get; set; }
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không đúng định dạng!")]
        //[StringLength(12, ErrorMessage = "Quá số lượng ký tự cho phép!")]
        public string phone { get; set; }
        public string img { get; set; }
        public string status { get; set; }
        public string rolecode { get; set; }
    }
}
