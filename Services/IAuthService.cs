using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vehicle_tracking.DTO;
using vehicle_tracking.Models.Responses;

namespace vehicle_tracking.Services {
    public interface IAuthService {
        Task<LoginResponse> CreateToken(UserDTO user);
    }
}
