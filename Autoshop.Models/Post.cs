namespace Autoshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MinLength(PostTextMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(PostTextMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Title { get; set; }

        [Required]
        [MinLength(PostTextMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(PostTextMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
