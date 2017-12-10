namespace Autoshop.Services
{
    using Autoshop.Services.Models.Blog;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogService
    {
        Task<IEnumerable<LatestPostsServiceModel>> LatestPosts(int count);

        Task<IEnumerable<PostListingServiceModel>> All(int page, int perPage);

        Task<PostListingServiceModel> GetById(int id);

        Task<int> TotalCount();

        Task<IEnumerable<CategoryListingServiceModel>> Categories();

        Task<bool> CategoryExist(int? categoryId);

        Task<int> AddArticle(string title, string text, int? categoryId, string authorId);
    }
}
