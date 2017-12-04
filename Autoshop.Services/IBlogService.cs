namespace Autoshop.Services
{
    using Autoshop.Services.Models.Blog;
    using System.Collections.Generic;

    public interface IBlogService
    {
        IEnumerable<LatestPostsServiceModel> LatestPosts(int count);
    }
}
