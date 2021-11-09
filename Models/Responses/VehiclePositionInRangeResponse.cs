using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vehicle_tracking.DTO;

namespace vehicle_tracking.Models.Responses
{
    public class VehiclePositionInRangeResponse
    {
        public List<VehiclePositionDTO> Positions { get; set; }
    }
}
