using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class news_dto
    {
        public List<project_dto> news { get; set; }
        public List<project_dto> inWeek { get; set; }
        public List<project_dto> inMonth { get; set; }
        public List<location_dto> locationInfor { get; set; }
    }
}
