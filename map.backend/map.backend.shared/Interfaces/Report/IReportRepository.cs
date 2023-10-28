using map.backend.shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Interfaces.Report
{
    public interface IReportRepository
    {
        Task<rpt_param_dto> getParams();
        Task<rpt_dto> exportReportLocation(string projectId, string wardCode, string districtCode, string provinceCode);
    }
}
