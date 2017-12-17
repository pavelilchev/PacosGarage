namespace Autoshop.Web.Controllers
{
    using Autoshop.Services;
    using Autoshop.Web.Models;
    using Autoshop.Web.Models.BlogViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Autoshop.Models;
    using Microsoft.AspNetCore.Identity;
    using Autoshop.Web.Infrastructure.Extensions;
    using System;
    
    using static Autoshop.Common.Constants.CommonConstants;

    public class BlogController : Controller
    {
        private readonly IBlogService blog;
        private readonly UserManager<User> userManager;

        public BlogController(IBlogService blog, UserManager<User> userManager)
        {
            this.blog = blog;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Articles(int page = 1, string categoryName = "")
        {
            var posts = await this.blog.All(page, PostsPerPage, categoryName);
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

        public async Task<IActionResult> Article(int id)
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
                var userId = userManager.GetUserId(User);

                var categoryExist = await this.blog.CategoryExist(model.CategoryId);
                if (!categoryExist)
                {
                    return BadRequest();
                }

                var postId = await this.blog.AddArticle(model.Title, model.Text, model.CategoryId, userId);

                return RedirectToAction(nameof(Article), new { id = postId });
            }

            model.Categories = await GetCategoriesToListItems();

            return View(model);
        }

        [Authorize(Roles = Writer + "," + Admin)]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Writer + "," + Admin)]
        public async Task<IActionResult> AddCategory(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.blog.AddCategory(model.Name);

            return RedirectToAction(nameof(Articles));
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
