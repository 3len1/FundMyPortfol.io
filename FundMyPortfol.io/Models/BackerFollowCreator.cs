using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class BackerFollowCreator
    {
        public long BackerId { get; set; }
        public long ProjectCreatorId { get; set; }

        public Backer Backer { get; set; }
        public ProjectCreator ProjectCreator { get; set; }
    }
}
