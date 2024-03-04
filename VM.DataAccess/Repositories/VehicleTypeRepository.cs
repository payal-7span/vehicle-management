using Microsoft.EntityFrameworkCore;
using VM.Common.Models;
using VM.DataAccess.Context;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories.Shared;

namespace VM.DataAccess.Repositories
{
    public interface IVehicleTypeRepository : IBaseRepository<VehicleType>
    {
        Task<VehicleType?> GetVehicalTypeAsync(string name);
    }

    public class VehicleTypeRepository(DatabaseContext context, ICurrentUser currentUser) :
        BaseRepository<VehicleType>(context, currentUser), IVehicleTypeRepository
    {
        public Task<VehicleType?> GetVehicalTypeAsync(string name)
        {
            return Query.FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
