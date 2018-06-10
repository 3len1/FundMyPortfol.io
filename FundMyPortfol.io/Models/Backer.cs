using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class Backer
    {
        public Backer()
        {
            BackerBuyPackage = new HashSet<BackerBuyPackage>();
            BackerFollowCreator = new HashSet<BackerFollowCreator>();
        }

        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long BackerDetailsId { get; set; }

        public BackerDetails BackerDetails { get; set; }
        public ICollection<BackerBuyPackage> BackerBuyPackage { get; set; }
        public ICollection<BackerFollowCreator> BackerFollowCreator { get; set; }
    }
}
