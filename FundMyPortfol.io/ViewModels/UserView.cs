using FundMyPortfol.io.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FundMyPortfol.io.ViewModels
{
    public class UserView
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ProjectCounter { get; set; }
        public int? Followers { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string Country { get; set; }
        [StringLength(20)]
        public string Town { get; set; }
        [StringLength(100)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
        public ICollection<Project> Project { get; set; }
        public IFormFile Media { get; set; }
    }
}
