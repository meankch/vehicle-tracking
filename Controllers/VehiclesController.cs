using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using vehicle_tracking.DTO;
using vehicle_tracking.Services;
using vehicle_tracking.Extension;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using vehicle_tracking.Models.Responses;

namespace vehicle_tracking.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase {

        private readonly ILogger<VehiclesController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehiclesController(ILogger<VehiclesController> logger, IVehicleService vehicleService) {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        // POST: api/vehicles
        [HttpPost]
        public async Task<IActionResult> RegisterVehicle([FromBody] VehicleDTO vehicle) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            bool result = await _vehicleService.RegisterVehicleAsync(vehicle);
            if (result)
                return StatusCode(((int)HttpStatusCode.Created));
            else
                return StatusCode((int)HttpStatusCode.BadRequest);
        }

        // PUT: api/vehicles/positions
        [HttpPut]
        [Route("positions")]
        public async Task<IActionResult> PutVehiclePosition([FromBody] VehiclePositionDTO vehiclePosition) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            bool result = await _vehicleService.RecordVehiclePositionAsync(vehiclePosition);
            if (result)
                return StatusCode(((int)HttpStatusCode.Created));
            else
                return StatusCode((int)HttpStatusCode.BadRequest);
        }

        // GET: api/vehicles/positions/current
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("positions/current")]
        public async Task<IActionResult> GetCurrentVehiclePosition([FromQuery] int userId, [FromQuery] int deviceId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            VehiclePositionDTO vehicle;
            if (userId != 0 && deviceId != 0) {
                vehicle = await _vehicleService.GetCurrentVehiclePositionAsync(userId, deviceId);
                if (vehicle == null) {
                    return NotFound();
                }
            }
            else {
                return StatusCode(((int)HttpStatusCode.BadRequest));
            }

            return Ok(vehicle);
        }

        // GET: api/vehicles/positions
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("positions")]
        public async Task<IActionResult> GetVehiclePositionInRange([FromQuery] int userId, [FromQuery] int deviceId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate) {
            List<VehiclePositionDTO> vehicles;
            if (userId != 0 && deviceId != 0 && startDate.HasValue && endDate.HasValue) {
                vehicles = await _vehicleService.GetVehiclePositionRangeAsync(userId, deviceId, startDate.Value, endDate.Value);

                if (vehicles == null) {
                    return NotFound();
                }
            }
            else {
                return StatusCode(((int)HttpStatusCode.BadRequest));
            }

            return Ok(new VehiclePositionInRangeResponse() { 
                Positions = vehicles
            });
        }
    }
}
