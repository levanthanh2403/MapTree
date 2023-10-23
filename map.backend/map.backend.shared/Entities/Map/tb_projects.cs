using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Entities.Map
{
    public class tb_projects : EntityBase
    {
        [MaxLength(100)]
        public string projectid { get; set; } //Mã dự án
        [MaxLength(255)]
        public string projectname { get; set; } //Tên dự án
        [MaxLength(255)]
        public string projectdesc { get; set; } //Mô tả dự án
        public string? projectdetail { get; set; } //Mô tả chi tiết dự án
        [MaxLength(255)]
        public string investor { get; set; } //Chủ đầu tư
        [MaxLength(255)]
        public string contractors { get; set; } //Nhà thầu
        public string total_value { get; set; } //Tổng giá trị
        public DateTime opendate { get; set; } //Ngày mở thầu
        public DateTime receiptdate { get; set; } //Ngày nhận thầu
        public DateTime enddate { get; set; } //Ngày hoàn thành
        public string img { get; set; } //Ngày hoàn thành
    }
}
