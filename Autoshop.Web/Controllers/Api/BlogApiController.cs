namespace Autoshop.Web.Controllers.Api
{
    using Autoshop.Services;
    using Autoshop.Services.Models.Blog;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/Blog")]
    public class BlogApiController : Controller
    {
        private readonly IBlogService blog;
        private readonly IMemoryCache cache;

        public BlogApiController(IBlogService blog, IMemoryCache cache)
        {
            this.blog = blog;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IEnumerable<LatestPostsServiceModel>> LatestPosts()
        {
            const string cacheLastPostsKey = "Cache_Last_Posts";

            var posts = this.cache.Get<IEnumerable<LatestPostsServiceModel>>(cacheLastPostsKey);

            if (posts == null)
            {
                posts = await this.blog.LatestPosts(3);
                this.cache.Set(cacheLastPostsKey, posts, DateTimeOffset.UtcNow.AddHours(1));
            }

            return posts;
        }
    }
}