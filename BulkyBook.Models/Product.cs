using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Display(Name ="List Price")]
        [Required]
        [Range(1,1000)]
        public double ListPrice { get; set; }

        [Display(Name ="Price for 1-50")]
        [Required]
        [Range(1,1000)]
        public double Price { get; set; }

        [Display(Name ="Price for 50+")]
        [Required]
        [Range(1,1000)]
        public double Price50 { get; set; }

        [Display(Name = "Price for 100+")]
        [Required]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ValidateNever]
        [Display(Name ="Product Image")]  
        public string? ImageUrl { get; set; }
    }
}
