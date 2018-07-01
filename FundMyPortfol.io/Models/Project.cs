using FundMyPortfol.io.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundMyPortfol.io.Models
{
    public partial class Project
    {
        public enum Category
        {
            [Description("innovation")] INNOVATION = 1,
            [Description("arts")] ARTS = 2,
            [Description("crafts")] CRAFTS = 3,
            [Description("comics")] COMICS = 4,
            [Description("film")] FILM = 5,
            [Description("food")] FOOD = 6,
            [Description("gadget")] GADGET =7,
            [Description("games")] GAMES = 8,
            [Description("music")] MUSIC = 9,
            [Description("publishing")] PUBLISHING = 10,
            [Description("software")] SOFTWARE = 11
        }

        public Project()
        {
            Package = new HashSet<Package>();
        }

        public long Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }
    
        [Required]
        [NotEmpty]
        [StringLength(100)]
        public string Title { get; set; }
        public string ProjectImage { get; set; }
       
        public int Likes { get; set; }
       
        public DateTime PablishDate { get; set; }

        [Required]
        [DatedValid]
        public DateTime ExpireDate { get; set; }

        public class DatedValid : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                value = (DateTime)value;
                if (DateTime.Now.AddYears(2).CompareTo(value) >= 0 && DateTime.Now.CompareTo(value) <= 0)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Date must be within the last two years");
            }
        }

        public class NotEmpty : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (string.IsNullOrEmpty((string)value) && string.IsNullOrWhiteSpace((string)value))
                    return new ValidationResult("Required field must not be empty");
                else
                    return ValidationResult.Success;
            }
        }




        [Required]
        [Range(0, 99999.99)]
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
