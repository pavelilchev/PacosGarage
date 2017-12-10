namespace Autoshop.Services.Implementations
{
    using System.Collections.Generic;
    using Autoshop.Services.Models.Blog;
    using Autoshop.Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Autoshop.Models;

    public class BlogService : IBlogService
    {
        private readonly AutoshopDbContext db;

        public BlogService(AutoshopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<PostListingServiceModel>> All(int page, int perPage)
        {
            return await this.db
                .Posts
                .OrderByDescending(p => p.CreatedOn)
                .Skip(page - 1)
                .Take(perPage)
                .ProjectTo<PostListingServiceModel>()
                .ToListAsync();
        }

        public async Task<PostListingServiceModel> GetById(int id)
        {
            return await this.db
               .Posts
               .Where(p => p.Id == id)
               .ProjectTo<PostListingServiceModel>()
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LatestPostsServiceModel>> LatestPosts(int count)
        {
            return await this.db
                .Posts
                .OrderByDescending(p => p.CreatedOn)
                .Take(count)
                .ProjectTo<LatestPostsServiceModel>()
                .ToListAsync();
        }

        public async Task<int> TotalCount()
        {
            return await this.db.Posts.CountAsync();
        }

        public async Task<IEnumerable<CategoryListingServiceModel>> Categories()
        {
            var categ = this.db.Categories.ToList();
            return await this.db
                .Categories
                .ProjectTo<CategoryListingServiceModel>()
                .ToListAsync();
        }
    }
}
