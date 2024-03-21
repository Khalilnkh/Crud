using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using P335_BackEnd.Areas.Admin.Models;
using P335_BackEnd.Data;
using P335_BackEnd.Entities;
using P335_BackEnd.Services;

namespace P335_BackEnd.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly FileService _fileService;

        public ProductController(AppDbContext dbContext, FileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products
                .Include(p => p.ProductImages)
                .ThenInclude(p => p.Image)
                .AsNoTracking().ToList();

            var model = new ProductIndexVM
            {
                Products = products
            };

            return View(model);
        }

        public IActionResult Add()
        {
            var categories = _dbContext.Categories.AsNoTracking().ToList();
            var productTypes = _dbContext.ProductTypes.AsNoTracking().ToList();

            var model = new ProductAddVM
            {
                Categories = categories,
                ProductTypes = productTypes
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ProductAddVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newProduct = new Product();

            newProduct.Name = model.Name;
            newProduct.Price = (decimal)model.Price;

            var foundCategory = _dbContext.Categories.FirstOrDefault(x => x.Id == model.CategoryId);
            if (foundCategory is null) return View(model);

            newProduct.Category = foundCategory;

            if (model.ProductTypeId != null)
            {
                var foundProductType = _dbContext.ProductTypes.FirstOrDefault(x => x.Id == model.ProductTypeId);
                if (foundProductType is null) return View(model);

                newProduct.ProductTypeProducts = new()
                {
                    new ProductTypeProduct
                    {
                        ProductType = foundProductType
                    }
                };
                var imageUrls = _fileService.AddFile(model.Images, Path.Combine("img", "featured"));

                newProduct.ProductImages = imageUrls.Select(url => new ProductImage
                {
                    Image = new Image
                    {
                        Url = url
                    }
                }).ToList();
            }



            _dbContext.Add(newProduct);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.Include(p => p.ProductImages).ThenInclude(pi => pi.Image)
                                             .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            if (product.ProductImages != null)
            {
                foreach (var productImage in product.ProductImages)
                {
                    if (productImage.Image != null)
                    {
                        _fileService.DeleteFile(productImage.Image.Url, Path.Combine("img", "featured"));
                    }
                }
            }

            _dbContext.Remove(product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            Product? product = _dbContext.Products.Include(p => p.ProductTypeProducts)
                .Include(p => p.ProductImages).ThenInclude(p => p.Image)
                .FirstOrDefault(x => x.Id == id);

            List<Category> category = _dbContext.Categories.AsNoTracking().ToList();
            List<ProductType> productTypes = _dbContext.ProductTypes.AsNoTracking().ToList();

            if (product is null) return NotFound();

            List<string> imageUrls = product.ProductImages?.Select(pi => pi.Image.Url).ToList() ?? new List<string>();

            ProductEditVM editedModel = new()
            {
                Name = product.Name,
                ProductTypes = productTypes,
                Categories = category,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ProductTypeId = product.ProductTypeProducts.FirstOrDefault()?.ProductTypeId,
                ImageUrl = imageUrls,
            };
            return View(editedModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditVM editedProduct)
        {
            Product product = _dbContext.Products
                .Include(p => p.ProductTypeProducts)
                .Include(p => p.ProductImages)
                .ThenInclude(pi => pi.Image)
                .FirstOrDefault(p => p.Id == editedProduct.Id);

            if (product is null)
                return NotFound();

            if (editedProduct.ImageUrl != null)
            {
                foreach (var currentImageUrl in product.ProductImages.Select(pi => pi.Image.Url).Except(editedProduct.ImageUrl))
                {
                    _fileService.DeleteFile(currentImageUrl, Path.Combine("img", "featured"));
                }

                product.ProductImages.RemoveAll(pi => !editedProduct.ImageUrl.Contains(pi.Image.Url));
            }

            if (editedProduct.Images != null && editedProduct.Images.Any())
            {
                foreach (var currentImageUrl in product.ProductImages.Select(pi => pi.Image.Url))
                {
                    _fileService.DeleteFile(currentImageUrl, Path.Combine("img", "featured"));
                }

                var newImageUrls = _fileService.AddFile(editedProduct.Images, Path.Combine("img", "featured"));
                product.ProductImages = newImageUrls.Select(imageUrl => new ProductImage
                {
                    Image = new Image
                    {
                        Url = imageUrl
                    }
                }).ToList();
            }


            product.Name = editedProduct.Name;
            product.Price = (decimal)editedProduct.Price;
            product.CategoryId = editedProduct.CategoryId;

            product.ProductTypeProducts = new List<ProductTypeProduct>
            {
                new ProductTypeProduct
                {
                    ProductTypeId = (int)editedProduct.ProductTypeId
                }
            };

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();

            Product product = _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages).ThenInclude(p => p.Image)
                .FirstOrDefault(x => x.Id == id);

            ProductIndexVM model = new()
            {
                Product = product
            };
            return View(model);
        }
    }
}