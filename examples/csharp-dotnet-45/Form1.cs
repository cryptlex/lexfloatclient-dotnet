using System;
using System.Windows.Forms;
using Cryptlex;

namespace FloatSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LicenceRenewCallback(uint status)
        {
            switch (status)
            {
                case LexFloatClient.StatusCodes.LF_OK:
                    this.statusLabel.Text = "The license lease has renewed successfully.";
                    break;
                case LexFloatClient.StatusCodes.LF_E_LICENSE_NOT_FOUND:
                    this.statusLabel.Text = "The license expired before it could be renewed.";
                    break;
                case LexFloatClient.StatusCodes.LF_E_LICENSE_EXPIRED_INET:
                    this.statusLabel.Text = "The license expired due to network connection failure.";
                    break;
                default:
                    this.statusLabel.Text = "The license renew failed due to other reason. Error code: " + status.ToString();
                    break;
            }
        }

        private void leaseBtn_Click(object sender, EventArgs e)
        {
            if (LexFloatClient.HasFloatingLicense() == LexFloatClient.StatusCodes.LF_OK)
            {
                return;
            }
            int status;
            status = LexFloatClient.SetHostProductId("PASTE_YOUR_PRODUCT_ID");
            if (status != LexFloatClient.StatusCodes.LF_OK)
            {
                this.statusLabel.Text = "Error setting product id: " + status.ToString();
                return;
            }
            status = LexFloatClient.SetHostUrl("http://localhost:8090");
            if (status != LexFloatClient.StatusCodes.LF_OK)
            {
                this.statusLabel.Text = "Error setting host url: " + status.ToString();
                return;
            }
            status = LexFloatClient.SetFloatingLicenseCallback(LicenceRenewCallback);
            if (status != LexFloatClient.StatusCodes.LF_OK)
            {
                this.statusLabel.Text = "Error setting callback function: " + status.ToString();
                return;
            }
            status = LexFloatClient.RequestFloatingLicense();
            if (status != LexFloatClient.StatusCodes.LF_OK)
            {
                this.statusLabel.Text = "Error requesting license: " + status.ToString();
                return;
            }
            this.statusLabel.Text = "License leased successfully!";
        }

        private void dropBtn_Click(object sender, EventArgs e)
        {
            if (LexFloatClient.HasFloatingLicense() != LexFloatClient.StatusCodes.LF_OK)
            {
                return;
            }
            int status;
            status = LexFloatClient.DropFloatingLicense();
            if (status != LexFloatClient.StatusCodes.LF_OK)
            {
                this.statusLabel.Text = "Error dropping license: " + status.ToString();
                return;
            }
            this.statusLabel.Text = "License dropped successfully!";
        }
    }
}
