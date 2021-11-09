using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vehicle_tracking.Models {
    public class VehicleDevice {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeviceID { get; set; }

        public string DeviceName { get; set; }

        public int VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
