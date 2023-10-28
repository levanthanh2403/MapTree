using map.backend.shared.Entities.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class locationdetail_dto
    {
        public location_dto data { get; set; }
        public List<project_dto> lstProject { get; set; }
        public List<sttm_ward_standard> lstWard { get; set; }
        public List<sttm_district_standard> lstDistrict { get; set; }
        public List<sttm_province_standard> lstProvince { get; set; }
    }
}
