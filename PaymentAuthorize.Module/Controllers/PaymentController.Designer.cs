namespace PaymentAuthorize.Module.Controllers
{
    partial class PaymentController
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
            this.saDoPayment = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saDoPayment
            // 
            this.saDoPayment.Caption = "Do Payment";
            this.saDoPayment.ConfirmationMessage = null;
            this.saDoPayment.Id = "saDoPayment";
            this.saDoPayment.TargetObjectType = typeof(PaymentAuthorize.Module.BusinessObjects.TransactionsManager);
            this.saDoPayment.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saDoPayment.ToolTip = null;
            this.saDoPayment.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saDoPayment.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saDoPayment_Execute);
            // 
            // PaymentController
            // 
            this.Actions.Add(this.saDoPayment);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saDoPayment;
    }
}
