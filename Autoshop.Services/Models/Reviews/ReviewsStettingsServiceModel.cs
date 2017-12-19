namespace Autoshop.Services.Models.Reviews
{
    using System.Collections.Generic;

    public class ReviewsStettingsServiceModel
    {
        public bool AutoPublish { get; set; }

        public double MinRatingAutoPublish { get; set; }

        public List<ReviewDetailsServiceModel> Reviews { get; set; }
    }
}
