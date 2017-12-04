namespace Autoshop.Web.Models.ManageViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class IndexViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(FirstNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(LastNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
