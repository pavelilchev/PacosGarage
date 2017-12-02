namespace Autoshop.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(FirstNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(LastNameMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string LastName { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
