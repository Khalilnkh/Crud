using System;
namespace P335_BackEnd.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryImageId { get; set; }

        // navigation properties
        public CategoryImage CategoryImage { get; set; }
        public List<Product> Products { get; set; }
    }
}

