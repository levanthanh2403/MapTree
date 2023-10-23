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
    }
}
