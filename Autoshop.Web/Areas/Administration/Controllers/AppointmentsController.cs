namespace Autoshop.Web.Areas.Admin.Controllers
{
    using Autoshop.Services;
    using Autoshop.Services.Models.Appointments;
    using Autoshop.Web.Areas.Administration.Controllers;
    using Autoshop.Web.Areas.Administration.Models.Appointments;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AppointmentsController : BaseAdminController
    {
        IAppointmetsService appointments;

        public AppointmentsController(IAppointmetsService appointments)
        {
            this.appointments = appointments;
        }

        public async Task<IActionResult> Index(AppointmentStatus status = AppointmentStatus.Unconfirmed)
        {
            var apps = await this.appointments.All(status);
            var model = new AppointmentsListingViewModel
            {
                Appointments = apps,
                AppointmentStatus = status
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Filter(AppointmentsListingViewModel model)
        {
            return RedirectToAction(nameof(Index), new { status = model.AppointmentStatus });
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await this.appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public async Task<IActionResult> Confirm(int id)
        {
             await this.appointments.Confirm(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
