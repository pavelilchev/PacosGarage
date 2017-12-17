namespace Autoshop.Web.Areas.Administration.Models.Specials
{
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class SpecialFormViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(SpecialDescriptionMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(SpecialDescriptionMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Description { get; set; }

        [Display(Name = "Days Valid")]
        [Range(0, int.MaxValue, ErrorMessage = RangeErrorMessage)]
        public int DaysValid { get; set; }
    }
}
