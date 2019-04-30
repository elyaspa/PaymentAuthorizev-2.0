using System;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet_Payments;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PaymentAuthorize.Module.BusinessObjects;

namespace PaymentAuthorize.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RefundController : ViewController
    {
        public RefundController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            TransactionsHistory transactionsHistory = (TransactionsHistory)View.CurrentObject;
            
                var LoginId = "3apCxP6Hr5e";
                var TransactionKey = "76Wu9bWNR64t4Fd4";
                var TransactionID = transactionsHistory.TransactionId;
                var RefundAmount = transactionsHistory.Type == PaymentType.Partial ? transactionsHistory.AmountPayed : transactionsHistory.TotalDue;
                var cardInfo = new AuthorizeNet_Payments.CreditCardInfo()
                {
                    CardCode = transactionsHistory.CardCode,
                    CardNumber = transactionsHistory.CardNumber,
                    ExpirationDate = transactionsHistory.ExpirationDate
                };

                Tuple<ANetApiResponse, createTransactionController> response = null;
                response = RefundTransaction.Run(LoginId, TransactionKey, RefundAmount, TransactionID, AuthorizeNet_Payments.Environment.SANDBOX, cardInfo);

                if (response.Item2.GetApiResponse() != null)
                {
                    if (response.Item2.GetApiResponse().messages.resultCode == messageTypeEnum.Ok)
                    {
                        if (response.Item2.GetApiResponse().messages != null)
                        {
                            Application.ShowViewStrategy.ShowMessage("Successfull Refund!!!");
                            View.ObjectSpace.CommitChanges();

                        }
                        else
                        {

                            Application.ShowViewStrategy.ShowMessage("Failed Transaction: Error Code: "
                                + response.Item2.GetApiResponse().transactionResponse.errors[0].errorCode
                                + "--" + "Error message: "
                                + response.Item2.GetApiResponse().transactionResponse.errors[0].errorText,
                                InformationType.Error, 4000, InformationPosition.Top);
                        }
                    }
                    else
                    {
                        Application.ShowViewStrategy.ShowMessage("Failed Transaction: Error Code: "
                               + response.Item2.GetApiResponse().transactionResponse.errors[0].errorCode
                               + "--" + "Error message: "
                               + response.Item2.GetApiResponse().transactionResponse.errors[0].errorText,
                               InformationType.Error, 4000, InformationPosition.Top);
                    }
                }
                else
                {
                    Application.ShowViewStrategy.ShowMessage("Error!!!");
                }
           
        }
    }
}
