using Microsoft.EntityFrameworkCore;
using SwimmingPool_V1.Data;
using SwimmingPool_V1.Interface;
using SwimmingPool_V1.Models;

namespace SwimmingPool_V1.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetByIdAsync(string id)
        {
            return await _context.Reservation.Include(f => f.AppUser).Include(l => l.Pool).Where(p => p.AppUserId == id).ToListAsync();
        }

        public async Task<Reservation> GetByIdAsyncDelete(int id)
        {
            return await _context.Reservation.Include(i => i.Lane).Where(p => p.ReservationId == id).FirstOrDefaultAsync();
        }

        public bool Delete(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        
        public async Task<IEnumerable<Reservation>> GetReservationAsync(int id, string userId)
        {
            return await _context.Reservation.Include(i => i.Lane).Where(c => c.LaneId == id && c.AppUserId == userId).ToListAsync();
        }

   
        public async void RemoveCurrentReserved(int id)
        {
            await _context.Lane
                .Where(l => l.LaneId == id)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.CurrentReserved, c => c.CurrentReserved - 1));
        }

    }
}
