using System;
using Microsoft.AspNetCore.Mvc;
using P335_BackEnd.Entities;

namespace P335_BackEnd.Views.Shared.Components.SearchResult
{
    public class SearchResultViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<Product> model)
        {
            return View(model);
        }
    }
}