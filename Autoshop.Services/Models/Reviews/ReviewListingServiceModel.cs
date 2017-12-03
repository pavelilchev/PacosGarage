namespace Autoshop.Services.Models.Reviews
{
    using System;

    public class ReviewListingServiceModel
    {
        public string Text { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPublished { get; set; }

        public string Author { get; set; }
    }
}
