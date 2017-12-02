namespace Autoshop.Web.Models.AppointmentsViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using static Autoshop.Common.ValidationConstants;

    public class AppointmentViewModel : IValidatableObject
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
        [RegularExpression(@"^\d{10}$", ErrorMessage = PhoneNumberErrorMessage)]
        public string Phone { get; set; }

        public string VehicleInformation { get; set; }

        public string Reason { get; set; }

        public int? SpecialId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        public DateTime DateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string format = "MM/dd/yyyy hh:mm";
            DateTime dateTime;
            bool isValidDate = DateTime.TryParseExact(
                $"{this.Date} {this.Time}",
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateTime);

            if (!isValidDate)
            {
                yield return new ValidationResult("Invalid Date or time", new[] { nameof(Date)});
            }
            else
            {
                this.DateTime = dateTime;
            }
        }
    }
}
