namespace Autoshop.Web.Controllers
{
    using Autoshop.Services;
    using Microsoft.AspNetCore.Mvc;

    public class BlogController : Controller
    {
        private readonly IBlogService blog;

        public BlogController(IBlogService blog)
        {
            this.blog = blog;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
