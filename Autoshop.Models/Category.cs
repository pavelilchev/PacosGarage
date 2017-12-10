namespace Autoshop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CategoryMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(CategoryMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Name { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
