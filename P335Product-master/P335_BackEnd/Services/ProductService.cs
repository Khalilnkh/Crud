using System;
using Microsoft.EntityFrameworkCore;
using P335_BackEnd.Data;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Services
{
    public class ProductService
    {
        private AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetFeaturedProducts(int? categoryId)
        {
            return _dbContext.ProductTypeProducts
                   .Include(x => x.ProductType)
                   .Include(x => x.Product).ThenInclude(x => x.ProductImages)
                   .Where(x => x.ProductType.Name == "Featured" && (categoryId == null ? true : x.Product.CategoryId == categoryId))
                   .Select(x => x.Product)
                   .ToList();
        }

        public List<Product> GetLatestProducts()
        {
            return _dbContext.ProductTypeProducts
                    .Include(x => x.ProductType)
                    .Include(x => x.Product)
                    .Where(x => x.ProductType.Name == "Latest")
                    .Select(x => x.Product)
                    .ToList();
        }


        public List<Product> GetTopRatedProducts()
        {
            return _dbContext.ProductTypeProducts
                    .Include(x => x.ProductType)
                    .Include(x => x.Product)
                    .Where(x => x.ProductType.Name == "Top Rated")
                    .Select(x => x.Product)
                    .ToList();
        }

        public List<Product> GetReviewProducts()
        {
            return _dbContext.ProductTypeProducts
                    .Include(x => x.ProductType)
                    .Include(x => x.Product)
                    .Where(x => x.ProductType.Name == "Review")
                    .Select(x => x.Product)
                    .ToList();
        }

    }
}

