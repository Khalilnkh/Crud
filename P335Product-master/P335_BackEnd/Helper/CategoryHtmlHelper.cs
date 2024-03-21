using System;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace P335_BackEnd.Helper
{
    public static class CategoryHtmlHelper
    {
        public static string IsCategoryActive(this IHtmlHelper htmlHelper, int? categoryId=null)
        {
            int.TryParse(htmlHelper.ViewContext.HttpContext.Request.Query["categoryId"].ToString(),
                out int _categoryId);

            if (categoryId == null && _categoryId == 0) return "active";
            
            return categoryId == _categoryId ? "active" : "";
        }
    }
}

