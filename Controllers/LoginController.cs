using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using vehicle_tracking.DTO;
using vehicle_tracking.Models.Responses;
using vehicle_tracking.Services;

namespace vehicle_tracking.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase {

        private readonly ILogger<LoginController> _logger;
        private readonly IAuthService _authenticationService;

        public LoginController(ILogger<LoginController> logger, IAuthService authenticationService) {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        // POST: api/login
        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] UserDTO user){
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            LoginResponse result = await _authenticationService.CreateToken(user);
            if (result.Token != null)
                return Ok(new LoginResponse()
                {
                    Result = true,
                    Token = result.Token
                });
            else
                return new JsonResult(new LoginResponse()
                {
                    Result = false,
                    Errors = result.Errors
                })
                { StatusCode = 500 };
        }
    }
}
