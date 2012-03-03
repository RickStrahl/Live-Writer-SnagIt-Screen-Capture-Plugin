using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Westwind.Tools;
using System.IO;
using System.Reflection;

namespace SnagitScreenCapturePlugin
{
    public partial class SnagItConfigurationForm : Form
    {
        
        public SnagItAutomation SnagIt
        {
            get { return _SnagIt; }
            set { _SnagIt = value; }
        }
        private SnagItAutomation _SnagIt =  null;


        public SnagItConfigurationForm(SnagItAutomation snagIt)
        {
            this.SnagIt = snagIt;
            InitializeComponent();
        }

        private void SnagItConfigurationForm_Load(object sender, EventArgs e)
        {
            BindSnagItObject();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.lblVersion.Text = "Version " + version.Major.ToString() + "." +  version.Minor.ToString();
        }

        private void BindSnagItObject()
        {
            Array Modes = Enum.GetValues(typeof(CaptureModes));
            foreach (CaptureModes Mode in Modes)
            {
                string Text = ((int)Mode).ToString() + " - " + Mode.ToString();
                int Index = this.cmbCaptureMode.Items.Add(new ComboListItem(Text, Mode));
                if (Mode == this.SnagIt.CaptureMode)
                    this.cmbCaptureMode.SelectedIndex = Index;
            }

            Modes = Enum.GetValues(typeof(CaptureFormats));
            foreach (CaptureFormats Mode in Modes)
            {
                int Index = this.cmbOutputFormat.Items.Add(new ComboListItem(Mode.ToString(), Mode));
                if (Mode == this.SnagIt.OutputFileCaptureFormat)
                    this.cmbOutputFormat.SelectedIndex = Index;
            }

            this.cmbColorDepth.Items.Add(new ComboListItem("1 Bit (Black & White)", 1));
            this.cmbColorDepth.Items.Add(new ComboListItem("8 Bit (256 Colors)", 8));
            this.cmbColorDepth.Items.Add(new ComboListItem("16 Bit (High Color)", 16));
            this.cmbColorDepth.Items.Add(new ComboListItem("24 Bit (True Color)", 24));
            this.cmbColorDepth.Items.Add(new ComboListItem("32 Bit (True Color)", 32));

            for (int x = 0; x < this.cmbColorDepth.Items.Count; x++)
            {
                ComboListItem item = this.cmbColorDepth.Items[x] as ComboListItem;
                if ((int)item.Value == this.SnagIt.ColorDepth)
                {
                    this.cmbColorDepth.SelectedIndex = x;
                    break;
                }
            }

            this.txtDelay.Value = (decimal)this.SnagIt.DelayInSeconds;
            this.chkIncludeCursor.Checked = this.SnagIt.IncludeCursor;
            this.chkDeleteImage.Checked = this.SnagIt.DeleteImageFromDisk;
            this.chkShowPreview.Checked = this.SnagIt.ShowPreviewWindow;
            this.txtCapturePath.Text = this.SnagIt.CapturePath;
        }

        private void UnbindSnagItObject()
        {
            CaptureModes CaptureMode = (CaptureModes) ((ComboListItem) this.cmbCaptureMode.SelectedItem).Value;
            this.SnagIt.CaptureMode = CaptureMode;

            CaptureFormats CaptureFormat = (CaptureFormats) ((ComboListItem) this.cmbOutputFormat.SelectedItem).Value;
            this.SnagIt.OutputFileCaptureFormat = CaptureFormat;
                        
            this.SnagIt.ColorDepth = (int) ((ComboListItem)this.cmbColorDepth.SelectedItem).Value;

            int Delay = 0;
            int.TryParse(this.txtDelay.Text,out Delay);
            this.SnagIt.DelayInSeconds = Delay;

            this.SnagIt.IncludeCursor  = this.chkIncludeCursor.Checked;            
            this.SnagIt.DeleteImageFromDisk = this.chkDeleteImage.Checked;
            this.SnagIt.ShowPreviewWindow = this.chkShowPreview.Checked;

            this.SnagIt.CapturePath = this.txtCapturePath.Text;
            if (string.IsNullOrEmpty(this.SnagIt.CapturePath) || !Directory.Exists(this.SnagIt.CapturePath))
                this.SnagIt.CapturePath = Path.GetTempPath();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.UnbindSnagItObject();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.UnbindSnagItObject();
                this.SnagIt.SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save settings:\r\n\r\n" + ex.Message, "SnagIt Screen Capture", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.picSave_Click(this, EventArgs.Empty);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Utils.GoUrl("http://www.west-wind.com/weblog/");
        }

        private void btnCapturePath_Click(object sender, EventArgs e)
        {
            string Dir = Directory.GetCurrentDirectory();
            
            FolderBrowserDialog dd = new FolderBrowserDialog();
            dd.Description = "Pick your image capture path";

            dd.SelectedPath = this.txtCapturePath.Text;
            dd.ShowNewFolderButton = true;            

            if (dd.ShowDialog() == DialogResult.Cancel)
                return;

            Directory.SetCurrentDirectory(Dir);

            this.txtCapturePath.Text = dd.SelectedPath;        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                Utils.GoUrl("http://www.techsmith.com/snagIt");
        }

        private void lblVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.GoUrl("http://www.west-wind.com/tools/snagItLiveWriterPlugin.asp");
        } 
    }

    public class ComboListItem
    {
        public ComboListItem(object key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public object Key = null;
        public object Value = null;

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}