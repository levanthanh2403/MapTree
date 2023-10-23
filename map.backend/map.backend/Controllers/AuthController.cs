using map.backend.shared.DTO;
using map.backend.shared.Interfaces.Auth;
using map.backend.shared.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using System.Net;

namespace map.backend.Controllers
{
    [Route("api/auth")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
        }
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> LoginApp([FromBody] login_request req)
        {
            try
            {
                login_response res = new login_response();
                res = await _authRepository.loginAction(req);
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
        [Route("register-user")]
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> RegisterUser([FromBody] register_request req)
        {
            try
            {
                register_response res = new register_response();
                res = await _authRepository.RegisterUser(req);
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
        [Route("update-user")]
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> UpdateUser([FromBody] register_request req)
        {
            try
            {
                register_response res = new register_response();
                res = await _authRepository.UpdateUser(req);
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
        [Route("get-list-user")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> GetListUser(int page, int pagesize, string userId, 
            string username, string phone, 
            string email, string status, 
            string rolecode)
        {
            try
            {
                var res = await _authRepository.GetListUser(page, pagesize, userId, username, phone, email, status, rolecode);
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
        [Route("get-detail-user")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> GetDetailUser(string userId)
        {
            try
            {
                var res = await _authRepository.GetDetailUser(userId);
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
