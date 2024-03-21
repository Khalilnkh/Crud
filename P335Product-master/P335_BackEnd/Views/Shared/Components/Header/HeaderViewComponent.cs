using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P335_BackEnd.Data;
using P335_BackEnd.Models;

namespace P335_BackEnd.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var contactInfo = _dbContext.ContactInfo.FirstOrDefault();

            if (contactInfo is null) return View(new HeaderComponentVM());

            var model = new HeaderComponentVM
            {
                Email = contactInfo.Email,
                PhoneNumber = contactInfo.PhoneNumber
            };

            return View(model);
        }
    }
}

