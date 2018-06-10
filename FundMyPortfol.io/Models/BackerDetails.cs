using System;
using System.Collections.Generic;

namespace FundMyPortfol.io.Models
{
    public partial class BackerDetails
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public Backer Backer { get; set; }
    }
}
