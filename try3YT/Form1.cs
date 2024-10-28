using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO.Compression;
using System.Diagnostics;
//using UpdateFunction;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Deployment.Application;
using System.Net.NetworkInformation;
using System.Security.Policy;

namespace UpdateService
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            // Start the update check

        }
        private void btnCheckForUpdates(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    ApplicationDeployment deployment = ApplicationDeployment.CurrentDeployment;
                    UpdateCheckInfo updateInfo = deployment.CheckForDetailedUpdate();
                    if (updateInfo.UpdateAvailable)
                    {
                        DialogResult result = MessageBox.Show("An update is available. Would you like to install it?", "Update Available", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            deployment.Update();
                            MessageBox.Show("The application has been updated and will now restart.", "Update Complete");
                            Application.Restart();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No updates are available.", "Check for Updates");
                    }
                }
                catch (DeploymentDownloadException ex)
                {
                    MessageBox.Show("The update could not be downloaded. Please check your network connection and try again.", "Update Error");
                }
                catch (InvalidDeploymentException ex)
                {
                    MessageBox.Show("The ClickOnce deployment is not valid. Please contact the application vendor.", "Deployment Error");
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show("This application cannot check for updates. It may not be a ClickOnce application.", "Update Check Error");
                }
            }
            else
            {
                MessageBox.Show("This application is not deployed using ClickOnce.", "Update Check Error");
            }
        }
    }
}