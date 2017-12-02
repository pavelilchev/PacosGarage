﻿namespace Autoshop.Web.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage = FirstNameMinLengthErrorMessgae)]
        [MaxLength(FirstNameMaxLength, ErrorMessage = FirstNameMaxLengthErrorMessgae)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = LastNameMinLengthErrorMessgae)]
        [MaxLength(LastNameMaxLength, ErrorMessage = LastNameMaxLengthErrorMessgae)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(UserPasswordMaxLength, ErrorMessage = UserPasswordErrorMessage, MinimumLength = UserPasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = UserConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
