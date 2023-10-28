using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class rpt_project_dto
    {
        public string projectid { get; set; }
        public string projectname { get; set; }
        public string province_name { get; set; }
        public string province_name_value { get; set; }
        public string district_name { get; set; }
        public string district_name_value { get; set; }
        public string ward_name { get; set; }
        public string ward_name_value { get; set; }
        public string address { get; set; }
        public string locationid { get; set; }
        public string locationname { get; set; }
        public string locationstatus { get; set; }
        public string treename { get; set; }
        public string treeinfor { get; set; }
        public string treestatus { get; set; }
    }
}
