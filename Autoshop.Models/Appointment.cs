namespace Autoshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class Appointment
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage = FirstNameMinLengthErrorMessgae)]
        [MaxLength(FirstNameMaxLength, ErrorMessage = FirstNameMaxLengthErrorMessgae)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = LastNameMinLengthErrorMessgae)]
        [MaxLength(LastNameMaxLength, ErrorMessage = LastNameMaxLengthErrorMessgae)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = PhoneNumberErrorMessage)]
        public string Phone { get; set; }

        public string VehicleInformation { get; set; }

        public string Reason { get; set; }

        public int? SpecialId { get; set; }

        public Special Special { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = DateErrorMessage)]
        public DateTime? Date { get; set; }

        [Required]
        public TimeSpan? Time { get; set; }
    }
}
