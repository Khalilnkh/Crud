using System;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Models
{
    public class ShopIndexVM
    {
        public List<Product> Products { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public Product Product { get; set; }
    }
}