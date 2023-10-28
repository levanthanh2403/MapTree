using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Entities.Map
{
    public class sttm_ward_standard : EntityBase
    {
        [MaxLength(8)]
        public string? ward_code {  get; set; }
        [MaxLength(255)]
        public string? ward_name {  get; set; }
        [MaxLength(8)]
        public string? district_code {  get; set; }
        [MaxLength(8)]
        public string? province_code {  get; set; }
        [MaxLength(255)]
        public string? maker_id {  get; set; }
        public DateTime? maker_dt_stamp {  get; set; }
        [MaxLength(255)]
        public string? checker_id {  get; set; }
        public DateTime? checker_dt_stamp {  get; set; }
        [MaxLength(1)]
        public string? record_stat {  get; set; }
        [MaxLength(255)]
        public string? ward_name_value {  get; set; }
    }
}
