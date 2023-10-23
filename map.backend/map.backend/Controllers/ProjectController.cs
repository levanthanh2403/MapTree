using map.backend.shared.DTO;
using map.backend.shared.Interfaces.Auth;
using map.backend.shared.Interfaces.Map;
using map.backend.shared.Repositories.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace map.backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }
        [Route("get-list-projects")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> getListProjects(string projectId, string projectName,
            string investor, string contractors, string total_value,
            string fromOpen, string toOpen,
            string fromEnd, string toEnd,
            string fromReceipt, string toReceipt)
        {
            try
            {
                var res = await _projectRepository.getListProjects(projectId, projectName,
                    investor, contractors, total_value,
                    fromOpen, toOpen,
                    fromEnd, toEnd,
                    fromReceipt, toReceipt);
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
        [Route("get-detail-project")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> getDetailProjects(string projectId)
        {
            try
            {
                var res = await _projectRepository.getDetailProjects(projectId);
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
        [Route("create-project")]
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> createProject([FromBody] crud_project_request req)
        {
            try
            {
                var res = await _projectRepository.createProject(req);
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
        [Route("update-project")]
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> updateProject([FromBody] crud_project_request req)
        {
            try
            {
                var res = await _projectRepository.updateProject(req);
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
