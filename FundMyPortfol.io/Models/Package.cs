using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundMyPortfol.io.Models
{
    public partial class Package
    {
        public Package()
        {
            BackerBuyPackage = new HashSet<BackerBuyPackage>();
        }

        public long Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }
        [NotEmpty]
        [Required]
        [StringLength(100)]
        public string PackageName { get; set; }
        public decimal PledgeAmount { get; set; }       
        public int TimesSelected { get; set; }
        public int? PackageLeft { get; set; }
        [NotEmpty]
        [Required]
        public string Description { get; set; }
       
        public long Project { get; set; }

        public Project ProjectNavigation { get; set; }
        public ICollection<BackerBuyPackage> BackerBuyPackage { get; set; }

        public class NotEmpty : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (string.IsNullOrEmpty((string)value) && string.IsNullOrWhiteSpace((string)value))
                    return new ValidationResult("Required field must not be empty");
                else
                    return ValidationResult.Success;
            }
        }
    }
}
