namespace Autoshop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.AppointmentsViewModels;

    public class AppointmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Success));
            }

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}