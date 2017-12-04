namespace Autoshop.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
