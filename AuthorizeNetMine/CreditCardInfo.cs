using System;

namespace AuthorizeNet_Payments
{
    public class CreditCardInfo
    {
        public string CardNumber { get; set; }

        public string CardCode { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string ExpirationDateToString { get => ExpirationDate != null ? ExpirationDate.ToString("MMyy") : string.Empty; }
    }
}
