using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class Project
    {
        public Project()
        {
            Package = new HashSet<Package>();
        }

        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public byte[] ProjectImage { get; set; }
        public int Likes { get; set; }
        public DateTime PablishDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal MoneyGoal { get; set; }
        public decimal MoneyReach { get; set; }
        public string Description { get; set; }
        public long CreatorId { get; set; }

        public ProjectCreator Creator { get; set; }
        public ICollection<Package> Package { get; set; }
    }
}
