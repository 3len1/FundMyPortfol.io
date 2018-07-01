using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class UserDetails
    {
        public long Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }

        public User User { get; set; }
    }
}
