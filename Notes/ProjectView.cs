using FundMyPortfol.io.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundMyPortfol.io.ViewModels
{
    public class ProjectView
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string ProjectImage { get; set; }
        public int Likes { get; set; }
        public DateTime PablishDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal MoneyGoal { get; set; }
        public decimal MoneyReach { get; set; }
        public string Description { get; set; }
        public long ProjectCtrator { get; set; }
        public Project.Category ProjectCategory { get; set; }
        public IFormFile Media { get; set; }

        public User ProjectCtratorNavigation { get; set; }
        public ICollection<Package> Package { get; set; }
    }
}
