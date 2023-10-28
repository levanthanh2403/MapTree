using map.backend.shared.DTO;
using map.backend.shared.Interfaces.Map;
using map.backend.shared.Interfaces.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace map.backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        }
        [Route("get-params")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> getParams()
        {
            try
            {
                var res = await _reportRepository.getParams();
                return Ok(res);
            }
            catch (Exception ex)
            {
                message_response res = new message_response();
                res.resCode = "999";
                res.resDesc = ex.Message;
                return BadRequest(res);
            }
        }
        [Route("export-report-location")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> exportReportLocation(string projectId, string wardCode, string districtCode, string provinceCode)
        {
            try
            {
                var res = await _reportRepository.exportReportLocation(projectId, wardCode, districtCode, provinceCode);
                return Ok(res);
            }
            catch (Exception ex)
            {
                message_response res = new message_response();
                res.resCode = "999";
                res.resDesc = ex.Message;
                return BadRequest(res);
            }
        }
    }
}
