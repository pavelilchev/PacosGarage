namespace Autoshop.Web.Models.ReviewsViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class ReviewCreateViewModel
    {
        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(FirstNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(LastNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = EmailErrorMessage)]
        public string Email { get; set; }

        [Required]
        [MinLength(ReviewTextMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(ReviewTextMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Text { get; set; }

        [Range(ReviewRatingMinValue, ReviewRatingMaxValue)]
        public double Rating { get; set; }
    }
}
