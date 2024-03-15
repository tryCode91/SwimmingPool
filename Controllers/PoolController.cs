using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwimmingPool_V1.Interface;
using SwimmingPool_V1.Models;
using SwimmingPool_V1.ViewModels;
using System.Linq;
using System.Numerics;

namespace SwimmingPool_V1.Controllers
{
    public class PoolController : Controller
    {
        private readonly IPoolRepository _poolRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservationRepository _reservationRepository;
        public PoolController(IPoolRepository poolRepository, UserManager<AppUser> userManager, IReservationRepository reservationRepository)
        {
            _poolRepository = poolRepository;
            _userManager = userManager;
            _reservationRepository = reservationRepository;
        }

        // GET: Pool
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Pool> pool = await _poolRepository.GetAll();
            return View(pool);
        }

        // GET: Pool/Details/5
        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            Pool pool = await _poolRepository.GetByIdAsyncNoTracking(id);

            return View(pool);

        }
        
        [Authorize]
        // GET Pool/Edit/2 (from Lane Model)
        public async Task<IActionResult> Edit(int id)
        {
            var lane = await _poolRepository.GetByIdAsyncEdit(id);
            
            if (lane == null) return View("Error");
            
            var laneVM = new EditLaneViewModel
            {
                LaneId = id,
                Type = lane.Type,
                Limit = lane.Limit,
                Image = lane.Image
            };

            return View(laneVM);
        }

        // POST Pool/Edit/2
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditLaneViewModel laneVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", laneVM);
            }

            var lanePool = await _poolRepository.GetByIdAsyncNoTrackingEdit(id);
            
            if (lanePool != null)
            {

                var lane = new Lane
                {
                    LaneId = id,
                    Type = laneVM.Type,
                    Limit = laneVM.Limit,
                    Image = lanePool.Image,
                    PoolId = lanePool.PoolId
                };

                _poolRepository.Update(lane);
                
                return RedirectToAction("Detail", new { id = lanePool.PoolId});

            }
            else
            {
                return View(laneVM);
            }

        }

        [Authorize]
        public async Task<IActionResult> Reserve(int id, ReservationViewModel reservationVM)
        {
            var lane = await _poolRepository.GetByIdAsyncEdit(id);
            if (lane == null) return View("Error");

            // has Lane limit has been reached
            if (lane.CurrentReserved < lane.Limit)
            {

                var reserveVM = new ReservationViewModel
                {
                    LaneId = id,
                    Type = lane.Type,
                    Limit = lane.Limit,
                    Image = lane.Image
                };

                return View(reserveVM);

            }
            else
            {
                // Do not make any reservations for this lane
                ViewBag.LaneLimitReached = "True";
                return View(reservationVM);
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reserve(string submitButton, int id, ReservationViewModel reserveVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", reserveVM);
            }

            var lane = await _poolRepository.GetByIdAsyncNoTrackingEdit(id); // To get the pool and lane id's
            
            // Get user
            var user = await _userManager.GetUserAsync(HttpContext.User);

            switch (submitButton)
            {
                case "Yes":

                    var reserve = new Reservation
                    {
                        LaneId = lane.LaneId,
                        PoolId = lane.PoolId,
                        AppUserId = user.Id,
                        DateTime = DateTime.UtcNow,
                        Type = lane.Type,
                        Image = lane.Image,
                        Limit = lane.Limit
                    };
                    
                    _poolRepository.UpdateCurrentReserved(lane.LaneId);
                    _poolRepository.Add(reserve);
                 
                    break;

                case "No":
                    // return
                    break;
            }

            return RedirectToAction("Detail", new { id = lane.PoolId });

        }
    }
}
