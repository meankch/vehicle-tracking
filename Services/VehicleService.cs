using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vehicle_tracking.DTO;
using vehicle_tracking.Persistences;

namespace vehicle_tracking.Services {
    public class VehicleService : IVehicleService {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository) {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehiclePositionDTO> GetCurrentVehiclePositionAsync(int userID, int deviceID) {
            VehiclePositionDTO result = null;
            try {
                // Check that a device or user cannot update the position of another vehicle
                int CheckUserDevicePosition = await _vehicleRepository.CheckUserWithVehicleDevice(userID, deviceID);
                if (CheckUserDevicePosition > 0) {
                    result = await _vehicleRepository.GetCurrentVehiclePositionAsync(userID, deviceID);
                }
            }
            catch (Exception) {
                return null;
            }

            return result;
        }

        public async Task<List<VehiclePositionDTO>> GetVehiclePositionRangeAsync(int userID, int deviceID, DateTime startDate, DateTime endDate) {
            List<VehiclePositionDTO> result = null;
            try {
                // Check that a device or user cannot update the position of another vehicle
                int CheckUserDevicePosition = await _vehicleRepository.CheckUserWithVehicleDevice(userID, deviceID);
                if (CheckUserDevicePosition > 0) {
                    result = await _vehicleRepository.GetVehiclePositionRangeAsync(userID, deviceID, startDate, endDate);
                }
            }
            catch (Exception) {
                return null;
            }

            return result;
        }

        public async Task<bool> RecordVehiclePositionAsync(VehiclePositionDTO vehiclePosition) {
            // Check that a device or user cannot update the position of another vehicle
            int checkUserDevicePosition = await _vehicleRepository.CheckUserWithVehicleDevice(vehiclePosition.UserID, vehiclePosition.DeviceID);
            if (checkUserDevicePosition > 0) {
                return await _vehicleRepository.RecordVehiclePositionAsync(vehiclePosition);
            }
            return false;
        }

        public async Task<bool> RegisterVehicleAsync(VehicleDTO vehicle) {
            //Check if user exist by Mail id
            int userExist = await _vehicleRepository.CheckUserExist(vehicle.User.EmailID);
            if (userExist == 0) {
                // Check if VehicleRegisterNumber already exist
                int vehicleExist = await _vehicleRepository.CheckVehicleExist(vehicle.VehicleRegisterNumber);
                if (vehicleExist == 0) {
                    return await _vehicleRepository.RegisterVehicleAsync(vehicle);
                }
            }
            return false;
        }
    }
}
