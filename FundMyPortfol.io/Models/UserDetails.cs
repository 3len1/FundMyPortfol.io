using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundMyPortfol.io.Models
{
    public partial class UserDetails
    {
        public long Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [StringLength(20)]
        public string Town { get; set; }
        [StringLength(100)]
        public string Street { get; set; }
        [StringLength(5)]
        public string PostalCode { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }

        public User User { get; set; }
    }
}
