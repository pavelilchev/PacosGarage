namespace Autoshop.Test.Services.Implementations
{
    using Autoshop.Data;
    using Autoshop.Models;
    using Autoshop.Services.Implementations;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ReviewsServiceTest
    {
        private readonly List<Review> reviews = new List<Review>
        {
            new Review{ Id = 1, AuthorId = "10", Rating = 5, IsPublished = true, CreatedOn = DateTime.Now },
            new Review{ Id = 2, AuthorId = "20",Rating = 4, IsPublished = true, CreatedOn = DateTime.Now.AddDays(-1) },
            new Review{ Id = 3, AuthorId = "10",Rating = 3, IsPublished = false, CreatedOn = DateTime.Now.AddDays(-2) }
        };

        private readonly List<User> users = new List<User>
        {
            new User{ Id = "10", FirstName ="Firs", LastName ="FLast"},
            new User{ Id = "20", FirstName ="Second", LastName ="SLast"}
        };

        public ReviewsServiceTest()
        {
            Configuration.Initialize();
        }

        [Fact]
        public async Task AllShoulReturnCorrectResult()
        {
            var db = this.GetDatabase();
            db.Reviews.AddRange(this.reviews);
            db.Users.AddRange(this.users);
            db.SaveChanges();
            var reviewService = new ReviewsService(db, null);

            var allReviews = await reviewService.All(1, 1, 10);

            allReviews
               .Should()
               .Match(r => r.ElementAt(0).Rating == 5
                   && r.ElementAt(1).Rating == 4)
               .And
               .HaveCount(2);
        }

        [Fact]
        public async Task AllShoulReturnCorrectResultPerPage()
        {
            var db = this.GetDatabase();
            db.Reviews.AddRange(this.reviews);
            db.Users.AddRange(this.users);
            db.SaveChanges();
            var reviewService = new ReviewsService(db, null);

            var allReviews = await reviewService.All(1, 1, 1);

            allReviews
               .Should()
               .Match(r => r.ElementAt(0).Rating == 5)
               .And
               .HaveCount(1);

            var allReviewsSecondPage = await reviewService.All(1, 2, 10);
            allReviewsSecondPage
             .Should()
             .HaveCount(0);
        }

        [Fact]
        public async Task AddShouldAddCorrectDataAndReturnTrue()
        {
            var db = this.GetDatabase();
            var config = new Mock<IConfiguration>();
            config.Setup(c => c["WebSiteSettings:Reviews:AutoPublish"])
                .Returns("true");
            config.Setup(c => c["WebSiteSettings:Reviews:ReviewsMinRatingToPublish"])
              .Returns("4");
            var reviewService = new ReviewsService(db, config.Object);
            double rating = 5;
            string text = "Review";
            string userId = "1";

            var result = await reviewService.Add(rating, text, userId);

            result.Should().Be(true);
            var reviews = await db.Reviews.ToListAsync();
            reviews.Should().HaveCount(1);
            reviews.Should().Match(r => r.ElementAt(0).Rating == rating
            && r.ElementAt(0).Text == text && r.ElementAt(0).AuthorId == userId);
        }

        [Fact]
        public async Task ByUserShoulReturnCorrectResult()
        {
            var db = this.GetDatabase();
            db.Reviews.AddRange(this.reviews);
            db.Users.AddRange(this.users);
            db.SaveChanges();
            var reviewService = new ReviewsService(db, null);

            var user = this.users.First();
            var allReviews = await reviewService.ByUser(user.Id);

            allReviews
               .Should()
               .Match(r => r.ElementAt(0).Rating == 5
                   && r.ElementAt(1).Rating == 3)
               .And
               .HaveCount(2);
        }

        [Fact]
        public async Task TotalCountShoulReturnCorrectResult()
        {
            var db = this.GetDatabase();
            db.Reviews.AddRange(this.reviews);
            db.SaveChanges();
            var reviewService = new ReviewsService(db, null);

            var allReviews = await reviewService.TotalCount(4);

            allReviews.Should().Be(2);
        }

        private AutoshopDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<AutoshopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AutoshopDbContext(dbOptions);
        }
    }
}
