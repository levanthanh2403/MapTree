using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class news_dto
    {
        public List<project_dto> top10 { get; set; }
        public List<location_dto> badLocation { get; set; }
    }
}
