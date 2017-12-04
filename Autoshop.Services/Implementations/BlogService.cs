namespace Autoshop.Services.Implementations
{
    using System.Collections.Generic;
    using Autoshop.Services.Models.Blog;
    using Autoshop.Data;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    public class BlogService : IBlogService
    {
        private readonly AutoshopDbContext db;

        public BlogService(AutoshopDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LatestPostsServiceModel> LatestPosts(int count)
        {
            return this.db
                .Posts
                .OrderByDescending(p => p.CreatedOn)
                .Take(count)
                .ProjectTo<LatestPostsServiceModel>()
                .ToList();
        }
    }
}
