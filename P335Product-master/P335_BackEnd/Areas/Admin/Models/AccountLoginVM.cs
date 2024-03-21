using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace P335_BackEnd.Areas.Admin.Models
{
    public class AccountLoginVM
    {
        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(4, ErrorMessage = "Password should be at least 2 characters long!")]
        [MaxLength(255, ErrorMessage = "Password should be at most 255 characters long!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [ValidateNever]
        public string ErrorMessage { get; set; }
    }
}

