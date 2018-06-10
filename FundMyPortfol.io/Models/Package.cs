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
        public decimal MoneyCost { get; set; }
        public int SoldCounter { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }

        public Project Project { get; set; }
        public ICollection<BackerBuyPackage> BackerBuyPackage { get; set; }
    }
}
