using System;
namespace P335_BackEnd.Entities
{
    public class CategoryImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }

        //navigation properties
        public Category Category { get; set; }
    }
}
