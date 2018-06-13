using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class Package
    {
        public Package()
        {
            BackerBuyPackage = new HashSet<BackerBuyPackage>();
        }

        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PackageName { get; set; }
        public decimal PledgeAmount { get; set; }
        public int TimesSelected { get; set; }
        public int? PackageLeft { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Description { get; set; }
        public long Project { get; set; }

        public Project ProjectNavigation { get; set; }
        public ICollection<BackerBuyPackage> BackerBuyPackage { get; set; }
    }
}
