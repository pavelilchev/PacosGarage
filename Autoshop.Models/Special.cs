namespace Autoshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class Special
    {
        public int Id { get; set; }

        [Required]
        [MinLength(SpecialDescriptionMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(SpecialDescriptionMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int DaysValid { get; set; }
    }
}
