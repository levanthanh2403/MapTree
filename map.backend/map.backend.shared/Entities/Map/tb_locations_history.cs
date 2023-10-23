using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Entities.Map
{
    public class tb_locations_history : EntityBase
    {
        [MaxLength(20)]
        public string locationid { get; set; }
        [MaxLength(100)]
        public string projectid { get; set; }
        [MaxLength(255)]
        public string locationname { get; set; }
        public string? locationinfo { get; set; }
        [MaxLength(255)]
        public string address { get; set; }
        public Geometry location { get; set; }
        [MaxLength(1)]
        public string locationstatus { get; set; } //0: không trồng cây, 1: cây ổn định, 2: cây bị chết
        [MaxLength(50)]
        public string? treecode { get; set; }
        [MaxLength(255)]
        public string? treename { get; set; }
        public string? treeinfor { get; set; }
        [MaxLength(100)]
        public string? treetype { get; set; }
        [MaxLength(100)]
        public string? treestatus { get; set; }
        public DateTime backupdt { get; set; }
    }
}
