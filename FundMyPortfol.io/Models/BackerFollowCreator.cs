using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class BackerFollowCreator
    {
        public long Backer { get; set; }
        public long ProjectCreator { get; set; }

        public User BackerNavigation { get; set; }
        public User ProjectCreatorNavigation { get; set; }
    }
}
