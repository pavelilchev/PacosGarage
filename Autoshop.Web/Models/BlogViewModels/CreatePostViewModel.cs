namespace Autoshop.Web.Models.BlogViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class CreatePostViewModel
    {
        [Required]
        [MinLength(PostTitleMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(PostTitleMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Title { get; set; }

        [Required]
        [MinLength(PostTextMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(PostTextMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Text { get; set; }        
        

        public int? CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
