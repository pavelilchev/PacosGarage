namespace Autoshop.Web.Controllers
{
    using Autoshop.Models;
    using Autoshop.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.AppointmentsViewModels;
    using System.Threading.Tasks;

    public class AppointmentsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAppointmetsService appointments;

        public AppointmentsController(UserManager<User> userManager, IAppointmetsService appointments)
        {
            this.userManager = userManager;
            this.appointments = appointments;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AppointmentViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);

                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
            }

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
    }
}