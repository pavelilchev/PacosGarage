namespace Autoshop.Web.Controllers.Api
{
    using Autoshop.Services;
    using Autoshop.Services.Models.Blog;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    
    [Route("api/Blog")]
    public class BlogApiController : Controller
    {
        private readonly IBlogService blog;

        public BlogApiController(IBlogService blog)
        {
            this.blog = blog;
        }

        [HttpGet]
        public IEnumerable<LatestPostsServiceModel> LatestPosts()
        {
            return this.blog.LatestPosts(3);
        }
    }
}