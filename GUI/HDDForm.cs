using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Timers;
using System.Threading;
using System.Management;
using System.Windows.Forms;

namespace OpenHardwareMonitor.GUI
{
    public partial class HDDForm : Form
    {
        public static Label Loadinglabel = new Label();
        public static ProgressBar LoadingProgress = new ProgressBar();
        public static ComboBox hddCombo = new ComboBox();

        public static string RequestedHardwareName;
        public static RichTextBox InformationBox;
        
        public static Label Description = new Label();

        string HDDdata;

        public static int SelectedItem = 0;

        public static System.Timers.Timer wait;

        public HDDForm()
        {
            InitializeComponent();
            loadingLabel = Loadinglabel;
            LoadingProgress = loadingBar;

            hddCombo = hddComboBox;
            Description = descriptionLabel;
            InformationBox = InfoBox;
        }

        public void addData(string data)
        {
                HDDdata=data;
        }

        public void SelectRequestedHardDisk(string hardwareName)
        {
            RequestedHardwareName = hardwareName;
            wait = new System.Timers.Timer(1000);
            wait.Elapsed += TimerElapsed;
            wait.Enabled = true;
  
        }

        private static void TimerElapsed (Object source, ElapsedEventArgs e) {
            hddCombo.SelectedItem = RequestedHardwareName.ToString();
            wait.Enabled = false;
            }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void captionLabel_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OpenHardwareMonitor.Hardware.HDD.HarddriveInformation.clearList();
            this.Close();
        }

        private void bindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void descriptionLabel_Click(object sender, EventArgs e)
        {

        }

        private void HDDForm_Load(object sender, EventArgs e)
        {
            Loadinglabel.Text = "Loading";
            InfoBox.Text = "Please select a harddrive.";
            HDDdata = "";
             // Get all the disk drives
            ManagementObjectSearcher mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            // Loop through each object (disk) retrieved by WMI
            foreach (ManagementObject moDisk in mosDisks.Get())
            {
                // Add the HDD to the list (use the Model field as the item's caption)
                hddCombo.Items.Add(moDisk["Model"].ToString());
            }

        }

        private void HDDForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenHardwareMonitor.Hardware.HDD.HarddriveInformation.clearList();
            

        }
            
        private void hddComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InfoBox.Text = "";
            InfoBox.Visible = false;
                       
                    
                    OpenHardwareMonitor.Hardware.HDD.HarddriveInformationProvider.GetInformation(hddCombo.SelectedItem.ToString());
                    addData(OpenHardwareMonitor.Hardware.HDD.HarddriveInformationProvider.data);
                    descriptionLabel.Text = OpenHardwareMonitor.Hardware.HDD.HarddriveInformationProvider.description;
                    InfoBox.Visible = true;
                InfoBox.Text = HDDdata;
            
                

           
           
        }

    }
}
