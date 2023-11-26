using map.backend.shared.DTO;
using map.backend.shared.Interfaces.Map;
using map.backend.shared.Repositories.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace map.backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        }
        [Route("get-list-location")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> getListLocation(string locationid, string projectid, string projectname,
            string locationname, string address, string locationstatus, string treecode,
            string treename, string treetype, string treestatus, string record_stat,
            string fromCreatedDate, string toCreatedDate)
        {
            try
            {
                var res = await _locationRepository.getListLocation(locationid, projectid, projectname,
                    locationname, address, locationstatus, treecode,
                    treename, treetype, treestatus, record_stat,
                    fromCreatedDate, toCreatedDate);
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
        [Route("get-list-location-hist")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> getListLocationHist(string locationid, string projectid, string projectname,
            string locationname, string address, string locationstatus, string treecode,
            string treename, string treetype, string treestatus, string record_stat,
            string fromCreatedDate, string toCreatedDate)
        {
            try
            {
                var res = await _locationRepository.getListLocationHist(locationid, projectid, projectname,
                    locationname, address, locationstatus, treecode,
                    treename, treetype, treestatus, record_stat,
                    fromCreatedDate, toCreatedDate);
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
        [Route("get-detail-location")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> getDetailLocation(string locationid)
        {
            try
            {
                var res = await _locationRepository.getDetailLocation(locationid);
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
        [Route("get-detail-location-hist")]
        [HttpGet]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> getDetailLocationHist(string locationid, string id)
        {
            try
            {
                var res = await _locationRepository.getDetailLocationHist(locationid, id);
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
        [Route("create-location")]
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> createProject([FromBody] crud_location_request req)
        {
            try
            {
                var res = await _locationRepository.createLocation(req);
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
        [Route("update-location")]
        [HttpPost]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<object>> updateProject([FromBody] crud_location_request req)
        {
            try
            {
                var res = await _locationRepository.updateLocation(req);
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
