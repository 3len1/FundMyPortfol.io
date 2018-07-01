using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundMyPortfol.io.Models
{
    public partial class BackerBuyPackage
    {
        public long Id { get; set; }
        public long Backer { get; set; }
        public long Package { get; set; }
        [DeliveryDateValid]
        public DateTime? DeliveryDate { get; set; }

        public User BackerNavigation { get; set; }
        public Package PackageNavigation { get; set; }

        public class DeliveryDateValid : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                value = (DateTime)value;
                if (DateTime.Now.AddYears(1).CompareTo(value) >= 0 && DateTime.Now.CompareTo(value) <= 0)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Date must in the next year");
            }
        }
    }
}
