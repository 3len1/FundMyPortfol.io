using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class ProjectCreator
    {
        public ProjectCreator()
        {
            BackerFollowCreator = new HashSet<BackerFollowCreator>();
            Project = new HashSet<Project>();
        }

        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BrandName { get; set; }
        public string Link { get; set; }
        public byte[] ProfileImage { get; set; }
        public int ProjectCounter { get; set; }
        public int Followers { get; set; }
        public DateTime? BirthDay { get; set; }
        public string About { get; set; }
        public long CreatorDetailsId { get; set; }

        public CreatorDetails CreatorDetails { get; set; }
        public ICollection<BackerFollowCreator> BackerFollowCreator { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
