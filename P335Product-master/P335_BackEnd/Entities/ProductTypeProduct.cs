using System;
namespace P335_BackEnd.Entities
{
    public class ProductTypeProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }

        public Product Product { get; set; }
        public ProductType ProductType { get; set; }
    }
}

