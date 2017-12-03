namespace Autoshop.Web.Controllers
{
    using Autoshop.Models;
    using Autoshop.Services;
    using Autoshop.Web.Models;
    using Autoshop.Web.Models.ReviewsViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static Autoshop.Common.Constants.CommonConstants;

    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviews;
        private readonly UserManager<User> userManager;

        public ReviewsController(IReviewsService reviews, UserManager<User> userManager)
        {
            this.reviews = reviews;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var reviewsToShow = await this.reviews.All(ReviewsMinRatingToPublish, page, ReviewsPerPage);
            return View(new ReviewListingViewModel
            {
                Reviews = reviewsToShow,
                Navigation = new NavigationViewModel
                {
                    TotalCount = await this.reviews.TotalCount(ReviewsMinRatingToPublish),
                    PerPage = ReviewsPerPage,
                    CurrentPage = page
                }
            });
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(User);

            var model = new ReviewCreateViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Rating = 5
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.userManager.GetUserId(User);
                await this.reviews.Add(model.Rating, model.Text, userId);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
