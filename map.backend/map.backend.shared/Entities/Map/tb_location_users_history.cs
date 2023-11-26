using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Entities.Map
{
    public class tb_location_users_history : EntityBase
    {
        [MaxLength(20)]
        public string locationid { get; set; }
        [MaxLength(50)]
        public string userid { get; set; }
        public DateTime backupdt { get; set; }
        public string histid { get; set; }
    }
}
