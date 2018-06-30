using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class User:IdentityUser<long>
    {
        public User()
        {
            BackerBuyPackage = new HashSet<BackerBuyPackage>();
            BackerFollowCreatorBackerNavigation = new HashSet<BackerFollowCreator>();
            BackerFollowCreatorProjectCreatorNavigation = new HashSet<BackerFollowCreator>();
            Project = new HashSet<Project>();
        }
        //public override long Id { get; set;}
        //[HiddenInput(DisplayValue = false)]
        //public DateTime CreatedDate { get; set; }
        //public int ProjectCounter { get; set; }
        //public int Followers { get; set; }
        public long UserDetails { get; set; }

        public UserDetails UserDetailsNavigation { get; set; }
        public ICollection<BackerBuyPackage> BackerBuyPackage { get; set; }
        public ICollection<BackerFollowCreator> BackerFollowCreatorBackerNavigation { get; set; }
        public ICollection<BackerFollowCreator> BackerFollowCreatorProjectCreatorNavigation { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
