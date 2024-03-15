using SwimmingPool_V1.Models;
using SwimmingPool_V1.ViewModels;
using System.Threading.Tasks;

namespace SwimmingPool_V1.Interface
{
    public interface IPoolRepository
    {
        // Lane
        Task<IEnumerable<Pool>> GetAll();
        Task<Pool> GetByIdAsync(int id);
        Task<Pool> GetByIdAsyncNoTracking(int id);
        void UpdateCurrentReserved(int id);

        // Lane
        Task<Lane> GetByIdAsyncEdit(int id); // GET Edit
        Task<Lane> GetByIdAsyncNoTrackingEdit(int id); // POST Edit
                                        
        bool Update(Lane lane);
        bool Save();
        bool Delete(Lane lane);

        bool Add(Reservation Reservation);

    }
}
