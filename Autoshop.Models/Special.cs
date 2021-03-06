﻿namespace Autoshop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Autoshop.Common.ValidationConstants;

    public class Special
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(SpecialDescriptionMinLength, ErrorMessage = MinLengthErrorMessgae)]
        [MaxLength(SpecialDescriptionMaxLength, ErrorMessage = MaxLengthErrorMessgae)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        [Range(0, int.MaxValue)]
        public int DaysValid { get; set; }
    }
}
