using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vehicle_tracking.DTO
{
    public class VehicleDTO {
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "Vehicle Name required"), MaxLength(30)]
        public string VehicleName { get; set; }

        [Required(ErrorMessage = "Vehicle Register Number required"), MaxLength(20)]
        public string VehicleRegisterNumber { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdateTimeStamp { get; set; }


        public UserRoleDTO UserRole { get; set; }
        public UserDTO User { get; set; }
        public VehicleDeviceDTO VehicleDevice { get; set; }
        public LocationDTO Location { get; set; }
    }
}
