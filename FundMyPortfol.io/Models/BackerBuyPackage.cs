using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class BackerBuyPackage
    {
        public long Backer { get; set; }
        public long Package { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public User BackerNavigation { get; set; }
        public Package PackageNavigation { get; set; }
    }
}
