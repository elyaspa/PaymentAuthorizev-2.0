using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet_Payments;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PaymentGateway.Module.BusinessObjects;

namespace PaymentAuthorize.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PaymentController : ViewController
    {


        
        public PaymentController()
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


        

        private void saDoPayment_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            TransactionsManager Transaction = (TransactionsManager)View.CurrentObject;
            MessageOptions messageOptionSuccess = new MessageOptions();
            MessageOptions messageOptionFailed = new MessageOptions();
            messageOptionSuccess.Duration = 2000;
            messageOptionSuccess.Message = string.Format("The transaction {0} has been successfull!", ((TransactionsManager)e.CurrentObject).Oid);
            messageOptionSuccess.Type = InformationType.Success;
            messageOptionSuccess.Web.Position = InformationPosition.Right;
            messageOptionSuccess.Win.Caption = "Success";
            messageOptionSuccess.Win.Type = WinMessageType.Alert;

            messageOptionFailed.Duration = 2000;
            messageOptionFailed.Message = string.Format("The transaction {0} can not processed!", ((TransactionsManager)e.CurrentObject).Oid);
            messageOptionFailed.Type = InformationType.Error;
            messageOptionFailed.Web.Position = InformationPosition.Right;
            messageOptionFailed.Win.Caption = "Error";
            messageOptionFailed.Win.Type = WinMessageType.Toast;
            var LoginId = "3apCxP6Hr5e";
            var TransactionKey = "76Wu9bWNR64t4Fd4";
            var Payed = Transaction.AmountToPay;
            var cardInfo = new AuthorizeNet_Payments.CreditCardInfo()
            {
                CardCode = Transaction.CardCode,
                CardNumber = Transaction.CardNumber,
                ExpirationDate = Transaction.ExpirationDate
            };
            if (Transaction.TotalDue>=Transaction.AmountToPay)
            {
                //Transaction Area
                Tuple<ANetApiResponse, createTransactionController> response = null;
                response = CreateChasePayTransaction.Run(LoginId, TransactionKey, cardInfo, Payed, AuthorizeNet_Payments.Environment.SANDBOX);
                if (response.Item2.GetApiResponse() != null)
                {
                    if (response.Item2.GetApiResponse().messages.resultCode == messageTypeEnum.Ok)
                    {
                        if (response.Item2.GetApiResponse().messages != null)
                        {
                            Application.ShowViewStrategy.ShowMessage(messageOptionSuccess);

                            TransactionsHistory transactionsHistory = (TransactionsHistory)View.ObjectSpace.CreateObject(typeof(TransactionsHistory));
                            transactionsHistory.PayedDate = Transaction.PayedDate;
                            transactionsHistory.TotalDue = Transaction.TotalDue - Transaction.AmountToPay;
                            transactionsHistory.CardCode = Transaction.CardCode;
                            transactionsHistory.CardNumber = Transaction.CardNumber;
                            transactionsHistory.TransactionId = response.Item2.GetApiResponse().transactionResponse.transId;
                            transactionsHistory.Type = Transaction.Type;
                            transactionsHistory.Transaction = Transaction;
                            transactionsHistory.ExpirationDate = Transaction.ExpirationDate;
                            transactionsHistory.AmountPayed = Transaction.AmountToPay;
                            Transaction.TransactionHistory.Add(transactionsHistory);
                            Transaction.TotalDue = transactionsHistory.TotalDue;
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
                    Application.ShowViewStrategy.ShowMessage(messageOptionFailed);
                }
                //Transaction Area ends here
            }
            else
            {
                Application.ShowViewStrategy.ShowMessage("Check the amount to pay",InformationType.Error);
            }

        }

       
    }
}
