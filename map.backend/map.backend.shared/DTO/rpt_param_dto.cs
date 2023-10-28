using map.backend.shared.Entities.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class rpt_param_dto
    {
        public List<tb_projects> lstProject { get; set; }
        public List<sttm_ward_standard> lstWard { get; set; }
        public List<sttm_district_standard> lstDistrict { get; set; }
        public List<sttm_province_standard> lstProvince { get; set; }
    }
}
