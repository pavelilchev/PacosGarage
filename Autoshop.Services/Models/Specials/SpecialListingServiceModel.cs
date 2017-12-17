namespace Autoshop.Services.Models.Specials
{
    using System;

    public class SpecialListingServiceModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int DaysValid { get; set; }
    }
}
