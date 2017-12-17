namespace Autoshop.Web.Models.AppointmentsViewModels
{
    using Autoshop.Services.Models.Specials;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using static Autoshop.Common.ValidationConstants;

    public class AppointmentViewModel : IValidatableObject
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

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = PhoneNumberErrorMessage)]
        public string PhoneNumber { get; set; }

        public string VehicleInformation { get; set; }

        public string Reason { get; set; }

        public int? SpecialId { get; set; }

        public SpecialListingServiceModel Special { get; set; }

        public List<SelectListItem> Specials { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        public DateTime DateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string format = "MM/dd/yyyy HH:mm";
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
