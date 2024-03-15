using Microsoft.EntityFrameworkCore;
using SwimmingPool_V1.Models;

namespace SwimmingPool_V1.Interface
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetByIdAsync(string id);

        Task<Reservation> GetByIdAsyncDelete(int id);
        bool Delete(Reservation reservation);
        bool Save();

        Task<IEnumerable<Reservation>> GetReservationAsync(int id, string userId);
        void RemoveCurrentReserved(int id);
    }
}
