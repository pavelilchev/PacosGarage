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
    using System;

    public class BlogService : IBlogService
    {
        private readonly AutoshopDbContext db;

        public BlogService(AutoshopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<PostListingServiceModel>> All(int page, int perPage, string categoryName)
        {
            var posts = this.db.Posts.AsQueryable();
            if (!string.IsNullOrEmpty(categoryName))
            {
                posts = posts.Where(p => p.Category.Name == categoryName);
            }

           return await posts
                .OrderByDescending(p => p.CreatedOn)
                .Skip((page - 1) * perPage)
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
                .OrderBy(c => c.Name)
                .ProjectTo<CategoryListingServiceModel>()
                .ToListAsync();
        }

        public async Task<bool> CategoryExist(int? categoryId)
        {
            if (categoryId != null)
            {
                var category = await this.db.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<int> AddArticle(string title, string text, int? categoryId, string authorId)
        {
            var post = new Post
            {
                Title = title,
                Text = text,
                AuthorId = authorId,
                CategoryId = categoryId,
                CreatedOn = DateTime.UtcNow
            };

            await this.db.Posts.AddAsync(post);
            await this.db.SaveChangesAsync();

            return post.Id;
        }

        public async Task<bool> AddCategory(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await this.db.Categories.AddAsync(category);

            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
