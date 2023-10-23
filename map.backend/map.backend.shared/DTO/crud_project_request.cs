using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class crud_project_request
    {
        public string projectid { get; set; }
        public string projectname { get; set; }
        public string projectdesc { get; set; }
        public string projectdetail { get; set; }
        public string investor { get; set; }
        public string contractors { get; set; }
        public string total_value { get; set; }
        public string opendate { get; set; }
        public string receiptdate { get; set; }
        public string enddate { get; set; }
        public string img { get; set; }
    }
}
