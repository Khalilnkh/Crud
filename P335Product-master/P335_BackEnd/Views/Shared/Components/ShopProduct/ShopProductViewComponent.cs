using System;
using Microsoft.AspNetCore.Mvc;
using P335_BackEnd.Entities;
using P335_BackEnd.Models;

namespace P335_BackEnd.Views.Shared.Components.ShopProduct
{
    public class ShopProductViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ShopIndexVM model)
        {
            return View(model);
        }
    }
}