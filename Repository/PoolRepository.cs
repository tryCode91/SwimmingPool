using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SwimmingPool_V1.Data;
using SwimmingPool_V1.Interface;
using SwimmingPool_V1.Models;
using SwimmingPool_V1.ViewModels;
using System.Linq;

namespace SwimmingPool_V1.Repository
{
    public class PoolRepository : IPoolRepository
    {
        private readonly ApplicationDbContext _context;
        public PoolRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pool>> GetAll()
        {
            return await _context.Pool.ToListAsync();
        }

        public async Task<Pool> GetByIdAsync(int id)
        { 
            return await _context.Pool.Include(i => i.Lane).FirstOrDefaultAsync(c => c.PoolId == id);
        }

        public async Task<Pool> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Pool.Include(i => i.Lane).AsNoTracking().FirstOrDefaultAsync(c => c.PoolId == id);
        }

        public async Task<Lane> GetByIdAsyncEdit(int id)
        {
            return await _context.Lane.FirstOrDefaultAsync(c => c.LaneId == id);
        }

        public async Task<Lane> GetByIdAsyncNoTrackingEdit(int id)
        {
            return await _context.Lane.AsNoTracking().FirstOrDefaultAsync(c => c.LaneId == id);
        }

        public bool Update(Lane lane)
        {
            _context.Update(lane);
            return Save();
        }
        // Reservation
        public bool Add(Reservation reservation)
        {
            _context.Add(reservation);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Add(Lane lane)
        {
            _context.Add(lane);
            return Save();
        }
        public bool Delete(Lane lane)
        {
            _context.Remove(lane);
            return Save();
        }

        public async void UpdateCurrentReserved(int id)
        {
             await _context.Lane
                .Where(l => l.LaneId == id)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.CurrentReserved, c => c.CurrentReserved + 1));
        }

    }
}
