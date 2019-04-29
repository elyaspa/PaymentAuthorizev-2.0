﻿using System;
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
using DevExpress.ExpressApp.ConditionalAppearance;
using PaymentGateway.Module.BusinessObjects;
using System.Drawing;

namespace PaymentAuthorize.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VoidPaymentController : ViewController
    {
        public VoidPaymentController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        //private AppearanceController appearanceController;
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
            //if (appearanceController != null)
            //{
            //    appearanceController.AppearanceApplied += new EventHandler<ApplyAppearanceEventArgs>(appearanceController_AppearanceApplied);
            //            }
        }

        private void saVoidPayment_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            TransactionsHistory transactionsHistory = (TransactionsHistory)View.CurrentObject;
            var LoginId = "3apCxP6Hr5e";
            var TransactionKey = "76Wu9bWNR64t4Fd4";
            var TransactionID = transactionsHistory.TransactionId;

            Tuple<ANetApiResponse, createTransactionController> response = null;
            response = VoidTransaction.Run(LoginId, TransactionKey, TransactionID,AuthorizeNet_Payments.Environment.SANDBOX);

            if (response.Item2.GetApiResponse() != null)
            {
                if (response.Item2.GetApiResponse().messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.Item2.GetApiResponse().messages != null)
                    {
                        Application.ShowViewStrategy.ShowMessage("Exito!!!");
                        transactionsHistory.VoidTransact = "void";
                        //appearanceController = Frame.GetController<AppearanceController>();
                        //if (appearanceController != null)
                        //{
                        //    appearanceController.AppearanceApplied += new EventHandler<ApplyAppearanceEventArgs>(appearanceController_AppearanceApplied);
                        //}
                        ////Frame.GetController<AppearanceController>().AppearanceApplied += VoidPaymentController_AppearanceApplied1;
                        ////Frame.GetController<AppearanceController>().CustomApplyAppearance += VoidPaymentController_CustomApplyAppearance;


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

       

        //private void VoidPaymentController_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
        //{
        //    if ((View is ListView) && (e.ItemType == AppearanceItemType.ViewItem.ToString()))
        //    {
        //        if (View.SelectedObjects.Contains(e.ContextObjects[0]))
        //        {
        //            IAppearanceFormat format = e.Item as IAppearanceFormat;
        //            if (format != null)
        //            {
        //                format.BackColor = Color.Red;
        //                format.FontStyle = FontStyle.Underline;
        //            }
        //        }
        //    }
        //}

        //private void appearanceController_AppearanceApplied(object sender, ApplyAppearanceEventArgs e)
        //{
        //    if ((View is ListView) && (e.ItemType == AppearanceItemType.ViewItem.ToString()) && (e.ItemName == "VoidTransact") && (e.ContextObjects.Length > 0))
        //    {
        //        if (View.SelectedObjects.Contains(e.ContextObjects[0]))
        //        {
        //            IAppearanceFormat format = e.Item as IAppearanceFormat;
        //            if (format != null)
        //            {
        //                format.BackColor = Color.Red;
        //                format.FontStyle = FontStyle.Underline;
        //            }
        //        View.ObjectSpace.CommitChanges();
        //        }
        //    }
        //}

       

    }
}
