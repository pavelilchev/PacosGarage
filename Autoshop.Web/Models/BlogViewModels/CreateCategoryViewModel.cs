namespace Autoshop.Web.Models.BlogViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class CreateCategoryViewModel
    {
        [Required]
        [MinLength(CategoryMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(CategoryMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Name { get; set; }
    }
}
