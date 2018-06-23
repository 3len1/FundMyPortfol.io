using FundMyPortfol.io.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundMyPortfol.io.ViewModels
{
    public class UserView
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProjectCounter { get; set; }
        public int Followers { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] ProfileImage { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
