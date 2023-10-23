using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class locationlist_dto
    {
        public List<location_dto> lstlocations { get; set; }
        public List<project_dto> lstProject { get; set; }
    }
}
