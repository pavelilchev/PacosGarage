namespace Autoshop.Web.Controllers
{
    using Autoshop.Models;
    using Autoshop.Services;
    using Autoshop.Services.Implementations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.AppointmentsViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AppointmentsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAppointmetsService appointments;
        private readonly ISpecialsService specials;

        public AppointmentsController(UserManager<User> userManager, IAppointmetsService appointments, ISpecialsService specials)
        {
            this.userManager = userManager;
            this.appointments = appointments;
            this.specials = specials;
        }

        public async Task<IActionResult> Index(int? specialId = null)
        {
            var model = new AppointmentViewModel();
            var specialToShowId = specialId != null ? specialId.Value : 0;
            if (specialToShowId > 0)
            {
                var special = await this.specials.Find(specialToShowId);
                if (special != null)
                {
                    model.SpecialId = special.Id;
                    model.Special = special;
                }
            }

            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);

                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
            }

            model.Specials = await this.PopulateSpeciaslOptions(specialToShowId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);
                model.UserId = userId;
            }

            await this.appointments.Add(model.FirstName, model.LastName, model.Email, model.PhoneNumber, model.VehicleInformation, model.Reason, model.DateTime, model.SpecialId, model.UserId);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
        
        private async Task<List<SelectListItem>> PopulateSpeciaslOptions(int specialId)
        {
            var allSpecials = await this.specials.All(true);

            return allSpecials.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Id.ToString(),
                Selected = s.Id == specialId
            })
            .ToList();
        }
    }
}