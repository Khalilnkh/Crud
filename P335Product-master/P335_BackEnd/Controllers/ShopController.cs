using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P335_BackEnd.Data;
using P335_BackEnd.Entities;
using P335_BackEnd.Models;

namespace P335_BackEnd.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IActionResult> Index(int page, string order = "desc")
        {
            if (page <= 0) page = 1;

            int productsPerPage = 3;
            var productCount = await _dbContext.Products.CountAsync();

            int totalPageCount = (int)Math.Ceiling(((decimal)productCount / productsPerPage));

            var productsQuery = order switch
            {
                "desc" => _dbContext.Products.OrderByDescending(x => x.Id),
                "asc" => _dbContext.Products.OrderBy(x => x.Id),
                _ => _dbContext.Products.OrderByDescending(x => x.Id)
            };

            var pagedProducts = await productsQuery
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .Include(p => p.ProductImages).ThenInclude(pi => pi.Image)
                .ToListAsync();

            ViewBag.Order = order;

            var model = new ShopIndexVM
            {
                Products = pagedProducts,
                TotalPageCount = totalPageCount,
                CurrentPage = page
            };

            return View(model);
        }

        //public IActionResult Filter(int page, string order = "desc")
        //{
        //    return RedirectToAction("Index", new { page, order });
        //}

        public async Task<IActionResult> Filter(int page, string order = "desc")
        {
            if (page <= 0) page = 1;

            int productsPerPage = 3;
            var productCount = await _dbContext.Products.CountAsync();

            int totalPageCount = (int)Math.Ceiling(((decimal)productCount / productsPerPage));

            var products = order switch
            {
                "desc" => _dbContext.Products.OrderByDescending(x => x.Id),
                "asc" => _dbContext.Products.OrderBy(x => x.Id),
                _ => _dbContext.Products.OrderByDescending(x => x.Id)
            };

            var model = new ShopIndexVM
            {
                Products = await products
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage)
                .ToListAsync(),
                TotalPageCount = totalPageCount,
                CurrentPage = page
            };

            return ViewComponent("ShopProduct", model);
        }

        public IActionResult Detail(int? id)
        {
            if (id is null) return NotFound();

            Product? product = _dbContext.Products
                .Include(p => p.ProductImages).ThenInclude(pi => pi.Image)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            ShopIndexVM model = new()
            {
                Product = product,
            };
            return View(model);

        }

        public IActionResult Search(string? input)
        {
            var products = input == null ? new List<Product>()
                : _dbContext.Products
                .Where(x => x.Name.ToLower()
                .StartsWith(input.ToLower()))
                .ToList();

            return ViewComponent("SearchResult", products);
        }

    }
}