﻿using System.ComponentModel.DataAnnotations;

namespace FiorelloBackend.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}