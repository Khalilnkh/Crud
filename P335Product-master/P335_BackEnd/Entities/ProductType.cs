using System;
namespace P335_BackEnd.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductTypeProduct> ProductTypeProducts { get; set; }
    }
}

