namespace Autoshop.Web.Controllers
{
    using Autoshop.Services;
    using Autoshop.Web.Models;
    using Autoshop.Web.Models.BlogViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static Autoshop.Common.Constants.CommonConstants;

    public class BlogController : Controller
    {
        private readonly IBlogService blog;

        public BlogController(IBlogService blog)
        {
            this.blog = blog;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var posts = await this.blog.All(page, PostsPerPage);
            var categories = await this.blog.Categories();

            var model = new BlogListingViewModel
            {
                Posts = posts,
                Categories = categories,
                Navigation = new NavigationViewModel
                {
                    TotalCount = await this.blog.TotalCount(),
                    PerPage = PostsPerPage,
                    CurrentPage = page
                }
            };

            return View(model);
        }

        public async Task<IActionResult> Articles(int id)
        {
            var model = await this.blog.GetById(id);

            return View(model);
        }
    }
}
