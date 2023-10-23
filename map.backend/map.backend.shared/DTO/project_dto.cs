using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class project_dto
    {
        public string projectid { get; set; } //Mã dự án
        public string projectname { get; set; } //Tên dự án
        public string projectdesc { get; set; } //Mô tả dự án
        public string projectdetail { get; set; } //Mô tả chi tiết dự án
        public string investor { get; set; } //Chủ đầu tư
        public string contractors { get; set; } //Nhà thầu
        public string total_value { get; set; } //Tổng giá trị
        public string opendate { get; set; } //Ngày mở thầu
        public string receiptdate { get; set; } //Ngày nhận thầu
        public string enddate { get; set; } //Ngày hoàn thành
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string modify_by { get; set; }
        public string modify_date { get; set; }
        public string img { get; set; }
    }
}
