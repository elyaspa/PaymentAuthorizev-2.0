namespace PaymentAuthorize.Module.Controllers
{
    partial class RefundController
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
            this.saRefund = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saRefund
            // 
            this.saRefund.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.saRefund.Caption = "Refund Transaction";
            this.saRefund.ConfirmationMessage = "You are about to make a Refund!!";
            this.saRefund.Id = "saRefund";
            this.saRefund.TargetObjectType = typeof(PaymentAuthorize.Module.BusinessObjects.TransactionsHistory);
            this.saRefund.TargetViewId = "";
            this.saRefund.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.saRefund.ToolTip = null;
            this.saRefund.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.saRefund.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // RefundController
            // 
            this.Actions.Add(this.saRefund);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saRefund;
    }
}
