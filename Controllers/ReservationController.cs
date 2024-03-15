using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwimmingPool_V1.Data;
using SwimmingPool_V1.Interface;
using SwimmingPool_V1.Models;
using SwimmingPool_V1.Repository;
using SwimmingPool_V1.ViewModels;
using System.Collections.Generic;
using System.Numerics;

namespace SwimmingPool_V1.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservationRepository _reservationRepository;
        public ReservationController(IReservationRepository reservationRepository, UserManager<AppUser> userManager)
        {
            _reservationRepository = reservationRepository;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get the current user
            
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                RedirectToAction("Index", "Pool ");
            }

            // get all reservations from reservation where User Id = id
            var reservation = await _reservationRepository.GetByIdAsync(user.Id);

            return View(reservation);
        
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            if (user == null)
            {
                RedirectToAction("Index", "Pool ");
            }

            var reservation = await _reservationRepository.GetByIdAsyncDelete(id);
            if(reservation.Lane != null)
            {
                // Only decrease the reservation count if the number is larger than 0
                if (reservation.Lane.CurrentReserved > 0)
                {
                    _reservationRepository.RemoveCurrentReserved(reservation.Lane.LaneId);

                }

                _reservationRepository.Delete(reservation);

            }

            return RedirectToAction("Index", "Reservation");
        }
    }
}
