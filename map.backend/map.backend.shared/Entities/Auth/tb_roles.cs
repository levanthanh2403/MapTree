using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Entities.Auth
{
    public class tb_roles : EntityBase
    {
        [MaxLength(50)]
        public string rolecode { get; set; }
        [MaxLength(255)]
        public string rolename { get; set; }
    }
}
