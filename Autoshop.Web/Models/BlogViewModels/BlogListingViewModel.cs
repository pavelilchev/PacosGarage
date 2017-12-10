namespace Autoshop.Web.Models.BlogViewModels
{
    using Autoshop.Services.Models.Blog;
    using System.Collections.Generic;

    public class BlogListingViewModel
    {
        public IEnumerable<PostListingServiceModel> Posts { get; set; }

        public IEnumerable<CategoryListingServiceModel> Categories { get; set; }

        public NavigationViewModel Navigation { get; set; }
    }
}
