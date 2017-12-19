namespace Autoshop.Web.Areas.Administration.Controllers
{
    using Autoshop.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ReviewsController : BaseAdminController
    {
        private readonly IReviewsService reviews;

        public ReviewsController(IReviewsService reviews)
        {
            this.reviews = reviews;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.reviews.AllWithSettings();

            return View(model);
        }
    }
}
