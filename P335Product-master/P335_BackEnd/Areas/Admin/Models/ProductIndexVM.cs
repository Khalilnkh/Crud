using System;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Areas.Admin.Models
{
    public class ProductIndexVM
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
    }
}