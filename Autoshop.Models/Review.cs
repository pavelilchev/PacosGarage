namespace Autoshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ReviewTextMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(ReviewTextMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Text { get; set; }

        [Range(ReviewRatingMinValue, ReviewRatingMaxValue)]
        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPublished { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
