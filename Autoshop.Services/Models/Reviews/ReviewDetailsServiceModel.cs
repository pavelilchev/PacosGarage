namespace Autoshop.Services.Models.Reviews
{
	using System;
	
    public class ReviewDetailsServiceModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPublished { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
    }
}
