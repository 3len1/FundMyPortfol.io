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
        [Required]
        [StringLength(100)]
        public string PackageName { get; set; }
        public decimal PledgeAmount { get; set; }
    
        public int TimesSelected { get; set; }
        public int? PackageLeft { get; set; }
        [Required]
        public string Description { get; set; }
       
        public long Project { get; set; }

        public Project ProjectNavigation { get; set; }
        public ICollection<BackerBuyPackage> BackerBuyPackage { get; set; }
    }
}
