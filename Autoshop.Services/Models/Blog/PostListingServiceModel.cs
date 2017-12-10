namespace Autoshop.Services.Models.Blog
{
    using System;

    public class PostListingServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public string Author { get; set; }
    }
}
