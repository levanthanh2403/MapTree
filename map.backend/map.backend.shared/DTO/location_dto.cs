using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class location_dto
    {
        public string id { get; set; }
        public string locationid { get; set; }
        public string projectid { get; set; }
        public string projectname { get; set; }
        public string locationname { get; set; }
        public string locationinfo { get; set; }
        public string ward_code { get; set; }
        public string district_code { get; set; }
        public string province_code { get; set; }
        public string address_detail { get; set; }
        public string address { get; set; }
        public Geometry location { get; set; }
        public string locationstatus { get; set; } //0: không trồng cây, 1: cây ổn định, 2: cây bị chết
        public string treecode { get; set; }
        public string treename { get; set; }
        public string treeinfor { get; set; }
        public string treetype { get; set; }
        public string treestatus { get; set; } //0: đã chết, 1: đang sống, 2: đã chuyển sang vị trí khác
        public string color { get; set; } //A: tất cả các điểm đồng màu, B: đánh dấu điểm cần khác màu
        public string record_stat { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string modify_by { get; set; }
        public string modify_date { get; set; }
        public List<location_dto> lstlocations { get; set; }
        public List<string> lstUser { get; set; }
    }
}
