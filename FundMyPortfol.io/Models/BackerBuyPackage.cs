using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class BackerBuyPackage
    {
        public long BackerId { get; set; }
        public long PackageId { get; set; }

        public Backer Backer { get; set; }
        public Package Package { get; set; }
    }
}
