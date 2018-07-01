using System;
using FundMyPortfol.io.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace FundMyPortfol.io.Models
{
    public partial class BackerBuyPackage
    {
        public enum Payment
        {
            [Description("payPall")] PAYPAL = 1,
            [Description("masterCard")] MASTERCARD = 2,
            [Description("visa")] VISA = 3,
            [Description("vivaWallet")] VIVAWALLET = 4,
            [Description("paySafe")] PAYSAFE = 5
        }

        public long Id { get; set; }
        public long Backer { get; set; }
        public long Package { get; set; }
        [DeliveryDateValid]
        public DateTime? DeliveryDate { get; set; }

        [NotMapped]
        public Payment PaymentMethod { get; set; }
        [Column("PaymentMethod")]
        public string PaymentToString
        {
            get { return PaymentMethod.ToString(); }

            private set => PaymentMethod = value.ParseEnum<Payment>();
        }

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
