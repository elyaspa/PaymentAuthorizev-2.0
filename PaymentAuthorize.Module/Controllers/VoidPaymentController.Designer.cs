namespace PaymentAuthorize.Module.Controllers
{
    partial class VoidPaymentController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.saVoidPayment = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saVoidPayment
            // 
            this.saVoidPayment.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.saVoidPayment.Caption = "Void Payment";
            this.saVoidPayment.ConfirmationMessage = "You are about to Cancel the Transaction!!!";
            this.saVoidPayment.Id = "saVoidPayment";
            this.saVoidPayment.ImageName = "PaymentUnpaid";
            this.saVoidPayment.TargetObjectType = typeof(PaymentAuthorize.Module.BusinessObjects.TransactionsHistory);
            this.saVoidPayment.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.saVoidPayment.ToolTip = null;
            this.saVoidPayment.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.saVoidPayment.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saVoidPayment_Execute);
            // 
            // VoidPaymentController
            // 
            this.Actions.Add(this.saVoidPayment);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saVoidPayment;
    }
}
