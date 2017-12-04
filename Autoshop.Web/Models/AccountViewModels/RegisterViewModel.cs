namespace Autoshop.Web.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(FirstNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(LastNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = PhoneNumberErrorMessage)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(UserPasswordMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(UserPasswordMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = UserConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
