using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Entities.Auth
{
    public class tb_role_mapping : EntityBase
    {
        [MaxLength(50)]
        public string rolecode { get; set; }
        [MaxLength(50)]
        public string screencode { get; set; }
        [MaxLength(50)]
        public string screenorg { get; set; }
    }
}
