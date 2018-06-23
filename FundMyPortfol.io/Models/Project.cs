using FundMyPortfol.io.Enumeration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundMyPortfol.io.Models
{
    public partial class Project
    {
        public enum Category
        {
            [Description("innovation")] INNOVATION,
            [Description("arts")] ARTS,
            [Description("crafts")] CRAFTS,
            [Description("comics")] COMICS,
            [Description("film")] FILM,
            [Description("food")] FOOD,
            [Description("gadget")] GADGET,
            [Description("games")] GAMES,
            [Description("music")] MUSIC,
            [Description("publishing")] PUBLISHING,
            [Description("software")] SOFTWARE
        }

        public Project()
        {
            Package = new HashSet<Package>();
        }

        public long Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public byte[] ProjectImage { get; set; }
        public int Likes { get; set; }
        public DateTime PablishDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal MoneyGoal { get; set; }
        public decimal MoneyReach { get; set; }
        public string Description { get; set; }
        public long ProjectCtrator { get; set; }
        [NotMapped]
        public Category ProjectCategory { get; set; }
        [Column("Category")]
        public string CategoryString
        {
            get { return ProjectCategory.ToString(); }

            private set => ProjectCategory = value.ParseEnum<Category>();
        }

        public User ProjectCtratorNavigation { get; set; }
        public ICollection<Package> Package { get; set; }
    }
}
