using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;

namespace PaymentAuthorize.Module.BusinessObjects
{
    //[Appearance("VoidAppearanceRule",AppearanceItemType = "ViewItem",TargetItems = "VoidTransaction" ,Criteria = "VoidTransact = 'void'",Context ="ListView",BackColor ="Red",FontStyle = FontStyle.Underline)]
    public class TransactionsHistory : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).




        bool refundTransaction;
        bool cancelTransaction;
        string transactionId;
        private DateTime expirationDate;
        string cardCode;
        string cardNumber;
        DateTime payedDate;
        TransactionsManager transaction;

        public TransactionsHistory(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        [Browsable(false)]
        [Association("PaymentHistory-ListOfPayments")]
        public TransactionsManager Transaction
        {
            get => transaction;
            set => SetPropertyValue(nameof(Transaction), ref transaction, value);
        }


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

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CardCode
        {
            get => cardCode;
            set => SetPropertyValue(nameof(CardCode), ref cardCode, value);
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CardNumber
        {
            get => cardNumber;
            set => SetPropertyValue(nameof(CardNumber), ref cardNumber, value);
        }

        PaymentType type;
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

        decimal totalDue;

        public decimal TotalDue
        {
            get => totalDue;
            set => SetPropertyValue(nameof(TotalDue), ref totalDue, value);
        }
        decimal amountPayed;

        public decimal AmountPayed
        {
            get => amountPayed;
            set => SetPropertyValue(nameof(AmountPayed), ref amountPayed, value);
        }

        [Browsable(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string TransactionId
        {
            get => transactionId;
            set => SetPropertyValue(nameof(TransactionId), ref transactionId, value);
        }


        [Browsable(false)]
        public bool CancelTransaction
        {
            get => cancelTransaction;
            set => SetPropertyValue(nameof(CancelTransaction), ref cancelTransaction, value);
        }
        [Browsable(false)]
        public bool RefundTransaction
        {
            get => refundTransaction;
            set => SetPropertyValue(nameof(RefundTransaction), ref refundTransaction, value);
        }
        PayIn payIn;        
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

    }
}