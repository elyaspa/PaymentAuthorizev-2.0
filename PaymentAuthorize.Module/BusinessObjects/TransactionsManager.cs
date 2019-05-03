using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
//using AuthorizeNet_Payments;
//using AuthorizeNet_Payments.Api.Controllers;
//using AuthorizeNet_Payments.Api.Contracts.V1;
//using AuthorizeNet_Payments.Api.Controllers.Bases;
using System;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Persistent.Validation;

namespace PaymentAuthorize.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("CardNumber")]
    public class TransactionsManager : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public TransactionsManager(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           // PayedDate = System.DateTime.UtcNow;
            Type = PaymentType.Full;
            PayIn = PayIn.Card;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        DateTime expirationDate;
        string cardCode;
        string cardNumber;
        DateTime payedDate;
        [Association("PaymentHistory-ListOfPayments")]
        public XPCollection<TransactionsHistory> TransactionHistory
        {
            get
            {
                return GetCollection<TransactionsHistory>(nameof(TransactionHistory));
            }
        }

        [ModelDefault("AllowEdit", "False")]
        public DateTime PayedDate
        {
            get => payedDate;
            set => SetPropertyValue(nameof(PayedDate), ref payedDate, value);
        }

        public DateTime ExpirationDate
        {
            get => expirationDate;
            set => SetPropertyValue(nameof(ExpirationDate), ref expirationDate, value);
        }
        [RuleRequiredField("Card Code can't be empty",DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CardCode
        {
            get => cardCode;
            set => SetPropertyValue(nameof(CardCode), ref cardCode, value);
        }
        [RuleRequiredField("Card Number can't be empty", DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CardNumber
        {
            get => cardNumber;
            set => SetPropertyValue(nameof(CardNumber), ref cardNumber, value);
        }

        PaymentType type;
        [ImmediatePostData]
        public PaymentType Type
        {
            get
            {
                return type;
            }
            set
            {
                SetPropertyValue(nameof(Type), ref type, value);
            }
        }

        PayIn payIn;
        [ImmediatePostData]
        public PayIn PayIn
        {
            get
            {
                return payIn;
            }
            set
            {
                SetPropertyValue(nameof(PayIn), ref payIn, value);
            }
        }

        decimal totalDue;
        [RuleRequiredField("Total Due can't be empty", DefaultContexts.Save)]
        public decimal TotalDue
        {
            get => totalDue;
            set => SetPropertyValue(nameof(TotalDue), ref totalDue, value);
        }
        decimal amountToPay;
        [RuleRequiredField("Amount to Pay can't be empty", DefaultContexts.Save)]
        public decimal AmountToPay
        {
            get => amountToPay;
            set => SetPropertyValue(nameof(AmountToPay), ref amountToPay, value);
        }
       
    }
    public enum PaymentType
    {
        Full = 1,
        Partial = 2,
    }

    public enum PayIn
    {
        Card = 1,
        Cash = 2,
    }

}