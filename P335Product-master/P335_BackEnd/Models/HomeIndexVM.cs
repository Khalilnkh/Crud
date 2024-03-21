using System;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Models
{
    public class HomeIndexVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public List<Product> LatestProducts { get; set; }
        public List<Product> ReviewProducts { get; set; }
        public List<Product> TopRatedProducts { get; set; }
    }
}

