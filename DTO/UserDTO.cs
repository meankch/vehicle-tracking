using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vehicle_tracking.DTO
{
    public class UserDTO {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
