using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApplicationServices;

namespace RestaurantBooking.API.Controllers
{
    public class SystemController : ApiController
    {
        ISystemAppService _appService;

        public SystemController(ISystemAppService appService)
        {
            _appService = appService;
        }

        [Route("api/system/manager/{managerUsername}", Name = "SystemRoute")]
        public IHttpActionResult GetSystemManager(string managerUsername)
        {
            try
            {
                var systemManager = _appService.GetSystemManager(managerUsername);
                if (systemManager == null)
                    return NotFound();
                return Ok(systemManager);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("api/system/managers")]
        public IHttpActionResult GetSystemManagers()
        {
            try
            {
                var systemManagers = _appService.GetSystemManagers();
                if (systemManagers == null)
                    return NotFound();
                return Ok(systemManagers);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("api/system/add/{username}")]
        public IHttpActionResult AddSystemManager([FromBody] string username)
        {
            try
            {
                var result = _appService.AddSystemManager(username);
                if (result.IsSuccess)
                    return Created(Url.Link("SystemManagerRoute", new { managerUsername = username }), result.Message);
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("api/system/remove/{username}")]
        public IHttpActionResult RemoveSystemManager(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid request");

            var result = _appService.RemoveSystemManager(username);
            if (result.IsSuccess)
                return Ok(result.Message);
            return BadRequest("Invalid request");
        }
    }
}