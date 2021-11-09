using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vehicle_tracking.DTO
{
    public class VehiclePositionDTO
    {
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime UpdateLocationTimeStamp { get; set; }
    }
}
