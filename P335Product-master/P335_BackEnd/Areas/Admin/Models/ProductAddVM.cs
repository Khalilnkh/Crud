using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Areas.Admin.Models
{
    public class ProductAddVM
    {
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }
        [ValidateNever]
        public List<Category>? Categories { get; set; }

        [ValidateNever]
        public int? ProductTypeId { get; set; }
        [ValidateNever]
        public List<ProductType>? ProductTypes { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}