namespace Autoshop.Web.Controllers
{
    using Autoshop.Services;
    using Autoshop.Web.Models;
    using Autoshop.Web.Models.BlogViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq;
    using static Autoshop.Common.Constants.CommonConstants;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Autoshop.Models;
    using Microsoft.AspNetCore.Identity;
    using Autoshop.Web.Infrastructure.Extensions;
    using System;

    public class BlogController : Controller
    {
        private readonly IBlogService blog;
        private readonly UserManager<User> userManager;

        public BlogController(IBlogService blog, UserManager<User> userManager)
        {
            this.blog = blog;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var posts = await this.blog.All(page, PostsPerPage);
            var categories = await this.blog.Categories();

            foreach (var post in posts)
            {
                post.Text = post.Text.RemoveHtmlTags();
                post.Text = post.Text.Substring(0, Math.Min(post.Text.Length, 500));
            }

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

        [Authorize(Roles = Writer + "," + Admin)]
        public async Task<IActionResult> Add()
        {
            var categories = await GetCategoriesToListItems();

            return View(new CreatePostViewModel
            {
                Categories = categories
            });
        }

        [HttpPost]
        [Authorize(Roles = Writer + "," + Admin)]
        public async Task<IActionResult> Add(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add sanitarize logic
                // var text = text.Sanitarize();
                var userId = userManager.GetUserId(User);

                var categoryExist = await this.blog.CategoryExist(model.CategoryId);
                if (!categoryExist)
                {
                    return BadRequest();
                }

                var postId = await this.blog.AddArticle(model.Title, model.Text, model.CategoryId, userId);

                return RedirectToAction(nameof(Articles), new { id = postId });
            }

            model.Categories = await GetCategoriesToListItems();

            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoriesToListItems()
        {
            var categories = await blog.Categories();

            var categoriesListItems = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return categoriesListItems;
        }
    }
}
