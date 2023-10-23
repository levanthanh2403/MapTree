using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Entities.Auth
{
    public class tb_screens : EntityBase
    {
        [MaxLength(50)]
        public string screencode { get; set; }
        [MaxLength(255)]
        public string screenname { get; set; }
        [MaxLength(50)]
        public string screenorg { get; set; }
        [MaxLength(50)]
        public string? group { get; set; }
        [MaxLength(100)]
        public string? screenurl { get; set; }
        [MaxLength(255)]
        public string? icon { get; set; }
        public int index { get; set; }
    }
}
