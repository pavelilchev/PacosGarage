namespace Autoshop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Autoshop.Data;
    using Autoshop.Models;
    using Autoshop.Services.Models.Reviews;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static Autoshop.Common.Constants.CommonConstants;

    public class ReviewsService : IReviewsService
    {
        private readonly AutoshopDbContext db;

        public ReviewsService(AutoshopDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Add(double rating, string text, string userId)
        {
            var review = new Review
            {
                Rating = rating,
                Text = text,
                AuthorId = userId,
                CreatedOn = DateTime.UtcNow,
                IsPublished = ReviewsAutoPublish && rating >= ReviewsMinRatingToPublish
            };

            await this.db.Reviews.AddAsync(review);
            var result = await this.db.SaveChangesAsync();
            if (result < 1)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<ReviewListingServiceModel>> All(double minRating, int page, int reviewsPerPage)
        {
            return await this.db
                .Reviews
                .Where(r => r.IsPublished && r.Rating >= minRating)
                .OrderByDescending(r => r.CreatedOn)
                .Skip((page - 1) * reviewsPerPage)
                .Take(reviewsPerPage)
                .ProjectTo<ReviewListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewListingServiceModel>> ByUser(string id)
        {
            return await this.db
                .Reviews
                .Where(r => r.AuthorId == id)
                .OrderByDescending(r => r.CreatedOn)
                .ProjectTo<ReviewListingServiceModel>()
                .ToListAsync();
        }

        public async Task<int> TotalCount(double minRatingToShow)
        {
            return await this.db
                .Reviews
                .Where(r => r.IsPublished && r.Rating >= minRatingToShow)
                .CountAsync();
        }
    }
}
