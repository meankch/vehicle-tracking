using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vehicle_tracking.DTO;
using vehicle_tracking.Models;
using vehicle_tracking.Persistences.Contexts;

namespace vehicle_tracking.Persistences {
    public class VehicleRepository : BaseRepository, IVehicleRepository {

        private readonly AppDbContext _dbContext;

        public VehicleRepository(AppDbContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public async Task<int> CheckUserExist(string emailId) {
            return await _dbContext.UsersDb.CountAsync(x => x.EmailID == emailId);
        }

        public async Task<int> CheckUserWithVehicleDevice(int userID, int deviceId) {
            return await _dbContext.Vehicles
                .Join(_dbContext.VehicleDevices,
                    vehicle => vehicle.VehicleID,
                    device => device.VehicleID,
                    (vehicle, device) => new { VEHICLE = vehicle, DEVICE = device })
                .Where(vehicleAndDevice => vehicleAndDevice.VEHICLE.UserID == userID 
                            && vehicleAndDevice.DEVICE.DeviceID == deviceId)
                .CountAsync();
        }

        public async Task<int> CheckVehicleExist(string vehicleRegisterNumber) {
            return await _dbContext.Vehicles.CountAsync(x => x.VehicleRegisterNumber == vehicleRegisterNumber);
        }

        public async Task<VehiclePositionDTO> GetCurrentVehiclePositionAsync(int userID, int deviceId) {
            var queryObj = await _dbContext.Vehicles
                       .Join(
                           _dbContext.VehicleDevices,
                           v => v.VehicleID,
                           vd => vd.VehicleID,
                           (v, vd) => new { VEHICLE = v, DEVICE = vd })
                       .Join(
                           _dbContext.Locations,
                           vdd => vdd.DEVICE.DeviceID,
                           loc => loc.DeviceID,
                           (vdd, loc) => new { vdd.VEHICLE, vdd.DEVICE, LOCATION = loc })
                       .Select(s => new {
                           UserID = s.VEHICLE.UserID,
                           DeviceID = s.DEVICE.DeviceID,
                           Latitude = s.LOCATION.Latitude,
                           Longitude = s.LOCATION.Longitude,
                           UpdateLocationTimeStamp = s.LOCATION.UpdateLocationTimeStamp
                       })
                       .Where(w => w.UserID == userID && w.DeviceID == deviceId)
                       .OrderByDescending(o => o.UpdateLocationTimeStamp).Take(1).FirstOrDefaultAsync();

            var result = new VehiclePositionDTO() {
                UserID = queryObj.UserID,
                DeviceID = queryObj.DeviceID,
                Latitude = queryObj.Latitude,
                Longitude = queryObj.Longitude,
                UpdateLocationTimeStamp = queryObj.UpdateLocationTimeStamp
            };

            return result;
        }

        public async Task<List<VehiclePositionDTO>> GetVehiclePositionRangeAsync(int userID, int deviceId, DateTime startDate, DateTime endDate) {
            var queryObj = await _dbContext.Vehicles
                     .Join(
                         _dbContext.VehicleDevices,
                         v => v.VehicleID,
                         vd => vd.VehicleID,
                         (v, vd) => new { VEHICLE = v, DEVICE = vd })
                     .Join(
                         _dbContext.Locations,
                         vdd => vdd.DEVICE.DeviceID,
                         loc => loc.DeviceID,
                         (vdd, loc) => new { vdd.VEHICLE, vdd.DEVICE, LOCATION = loc })
                     .Select(s => new {
                         UserID = s.VEHICLE.UserID,
                         DeviceID = s.DEVICE.DeviceID,
                         Latitude = s.LOCATION.Latitude,
                         Longitude = s.LOCATION.Longitude,
                         UpdateLocationTimeStamp = s.LOCATION.UpdateLocationTimeStamp
                     })
                     .Where(
                       w => w.UserID == userID
                       && w.DeviceID == deviceId
                       && w.UpdateLocationTimeStamp >= startDate
                       && w.UpdateLocationTimeStamp <= endDate)
                     .OrderByDescending(o => o.UpdateLocationTimeStamp)
                     .ToListAsync();

            return queryObj.Select(x => new VehiclePositionDTO {
                UserID = x.UserID,
                DeviceID = x.DeviceID,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                UpdateLocationTimeStamp = x.UpdateLocationTimeStamp
            }).ToList();
        }

        public async Task<bool> RecordVehiclePositionAsync(VehiclePositionDTO vehiclePosition) {
            if (vehiclePosition != null) {
                var objVehiclePosition = new Location() {
                    DeviceID = vehiclePosition.DeviceID,
                    Latitude = vehiclePosition.Latitude,
                    Longitude = vehiclePosition.Longitude,
                    UpdateLocationTimeStamp = vehiclePosition.UpdateLocationTimeStamp
                };

                _dbContext.Locations.Add(objVehiclePosition);
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                    return true;
            }

            return false;
        }

        public async Task<bool> RegisterVehicleAsync(VehicleDTO vehicle) {

            if (vehicle != null) {
                var objUser = new User() {
                    UserName = vehicle.User.UserName,
                    EmailID = vehicle.User.EmailID,
                    Password = vehicle.User.Password,
                    RoleID = 2
                };
                await _dbContext.UsersDb.AddAsync(objUser);
                await _dbContext.SaveChangesAsync();

                var objVehicle = new Vehicle() {
                    UserID = objUser.UserID,
                    VehicleName = vehicle.VehicleName,
                    VehicleRegisterNumber = vehicle.VehicleRegisterNumber,
                    UpdateTimeStamp = DateTime.Now
                };
                await _dbContext.Vehicles.AddAsync(objVehicle);
                await _dbContext.SaveChangesAsync();

                var objDevice = new VehicleDevice() {
                    VehicleID = objVehicle.VehicleID,
                    DeviceName = vehicle.VehicleDevice.DeviceName
                };
                await _dbContext.VehicleDevices.AddAsync(objDevice);
                await _dbContext.SaveChangesAsync();
                
                return true;
            }

            return false;
        }
    }
}
