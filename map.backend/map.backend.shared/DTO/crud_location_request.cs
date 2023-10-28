using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class crud_location_request
    {
        public string locationid { get; set; }
        public string projectid { get; set; }
        public string locationname { get; set; }
        public string locationinfo { get; set; }
        public string ward_code { get; set; }
        public string district_code { get; set; }
        public string province_code { get; set; }
        public string address_detail { get; set; }
        public string address { get; set; }
        public double? location_lon { get; set; }
        public double? location_lat { get; set; }
        public string locationstatus { get; set; } //0: không trồng cây, 1: cây ổn định, 2: cây bị chết
        public string treecode { get; set; }
        public string treename { get; set; }
        public string treeinfor { get; set; }
        public string treetype { get; set; }
        public string treestatus { get; set; } //0: đã chết, 1: đang sống, 2: đã chuyển sang vị trí khác
        public string record_stat { get; set; } //O: Mở, C: Đóng
    }
}
