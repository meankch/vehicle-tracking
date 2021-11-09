using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vehicle_tracking.Models {
    public class Vehicle {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "Vehicle Name is missing"), MaxLength(30)]
        public string VehicleName { get; set; }

        [Required(ErrorMessage = "Vehicle Register Number is missing"), MaxLength(20)]
        public string VehicleRegisterNumber { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdateTimeStamp { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public ICollection<VehicleDevice> VehicleDevices { get; set; }
    }
}
