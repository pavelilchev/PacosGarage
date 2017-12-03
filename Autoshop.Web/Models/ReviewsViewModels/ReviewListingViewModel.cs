namespace Autoshop.Web.Models.ReviewsViewModels
{
    using Autoshop.Services.Models.Reviews;
    using System.Collections.Generic;

    public class ReviewListingViewModel
    {
        public IEnumerable<ReviewListingServiceModel> Reviews { get; set; }

        public NavigationViewModel Navigation { get; set; }
    }
}
