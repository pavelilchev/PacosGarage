namespace Autoshop.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage = FirstNameMinLengthErrorMessgae)]
        [MaxLength(FirstNameMaxLength, ErrorMessage = FirstNameMaxLengthErrorMessgae)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = LastNameMinLengthErrorMessgae)]
        [MaxLength(LastNameMaxLength, ErrorMessage = LastNameMaxLengthErrorMessgae)]
        public string LastName { get; set; }
    }
}
