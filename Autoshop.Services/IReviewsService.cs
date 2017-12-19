namespace Autoshop.Services
{
    using Autoshop.Services.Models.Reviews;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReviewsService
    {
        Task<IEnumerable<ReviewListingServiceModel>> All(double minRating, int page, int reviewsPerPage);

        Task<int> TotalCount(double minRatingToShow);

        Task<bool> Add(double rating, string text, string userId);

        Task<IEnumerable<ReviewListingServiceModel>> ByUser(string id);

        Task<ReviewsStettingsServiceModel> AllWithSettings();
    }
}
