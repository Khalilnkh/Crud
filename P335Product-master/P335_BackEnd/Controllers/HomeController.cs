using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P335_BackEnd.Data;
using P335_BackEnd.Entities;
using P335_BackEnd.Models;
using P335_BackEnd.Services;

namespace P335_BackEnd.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly ProductService _productService;

    public HomeController(AppDbContext dbContext, ProductService productService)
    {
        _dbContext = dbContext;
        _productService = productService;
    }

    public IActionResult Index(int? categoryId)
    {

        var categories = _dbContext.Categories
            .Include(x=>x.CategoryImage)
            .ToList();

        var featured = _productService.GetFeaturedProducts(categoryId);

        var latest = _productService.GetLatestProducts();

        var topRated = _productService.GetTopRatedProducts();

        var review = _productService.GetReviewProducts();

        var model = new HomeIndexVM
        {
            Categories = categories,
            FeaturedProducts = featured,
            LatestProducts = latest,
            ReviewProducts = review,
            TopRatedProducts = topRated
        };
        
        return View(model);
    }
}