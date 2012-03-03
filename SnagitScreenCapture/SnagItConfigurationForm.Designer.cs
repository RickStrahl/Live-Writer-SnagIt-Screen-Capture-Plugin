namespace SnagitScreenCapturePlugin
{
    partial class SnagItConfigurationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnagItConfigurationForm));
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCaptureMode = new System.Windows.Forms.ComboBox();
            this.cmbColorDepth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOutputFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkIncludeCursor = new System.Windows.Forms.CheckBox();
            this.txtDelay = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.LinkLabel();
            this.chkShowPreview = new System.Windows.Forms.CheckBox();
            this.chkDeleteImage = new System.Windows.Forms.CheckBox();
            this.btnGetCapturePath = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCapturePath = new System.Windows.Forms.TextBox();
            this.btnSaveSettings = new System.Windows.Forms.LinkLabel();
            this.picSave = new System.Windows.Forms.PictureBox();
            this.picSnagIt = new System.Windows.Forms.PictureBox();
            this.picWestwind = new System.Windows.Forms.PictureBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtDelay)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSnagIt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWestwind)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(341, 305);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 0;
            this.btnCapture.Text = "&Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(422, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Capture Mode";
            // 
            // cmbCaptureMode
            // 
            this.cmbCaptureMode.FormattingEnabled = true;
            this.cmbCaptureMode.Location = new System.Drawing.Point(159, 34);
            this.cmbCaptureMode.Name = "cmbCaptureMode";
            this.cmbCaptureMode.Size = new System.Drawing.Size(289, 21);
            this.cmbCaptureMode.TabIndex = 8;
            // 
            // cmbColorDepth
            // 
            this.cmbColorDepth.FormattingEnabled = true;
            this.cmbColorDepth.Location = new System.Drawing.Point(158, 78);
            this.cmbColorDepth.Name = "cmbColorDepth";
            this.cmbColorDepth.Size = new System.Drawing.Size(289, 21);
            this.cmbColorDepth.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Color Depth";
            // 
            // cmbOutputFormat
            // 
            this.cmbOutputFormat.FormattingEnabled = true;
            this.cmbOutputFormat.Location = new System.Drawing.Point(158, 122);
            this.cmbOutputFormat.Name = "cmbOutputFormat";
            this.cmbOutputFormat.Size = new System.Drawing.Size(289, 21);
            this.cmbOutputFormat.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output File Format";
            // 
            // chkIncludeCursor
            // 
            this.chkIncludeCursor.AutoSize = true;
            this.chkIncludeCursor.Location = new System.Drawing.Point(159, 152);
            this.chkIncludeCursor.Name = "chkIncludeCursor";
            this.chkIncludeCursor.Size = new System.Drawing.Size(145, 17);
            this.chkIncludeCursor.TabIndex = 0;
            this.chkIncludeCursor.Text = "Include Cursor in Capture";
            this.ToolTip.SetToolTip(this.chkIncludeCursor, "If set causes the mouse cursor to be captured in the screen capture.");
            this.chkIncludeCursor.UseVisualStyleBackColor = true;
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(410, 152);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(38, 20);
            this.txtDelay.TabIndex = 2;
            this.ToolTip.SetToolTip(this.txtDelay, "Determines if there\'s a delay for captures. 0 means no delay.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Capture Delay:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblVersion);
            this.groupBox1.Controls.Add(this.chkShowPreview);
            this.groupBox1.Controls.Add(this.chkDeleteImage);
            this.groupBox1.Controls.Add(this.btnGetCapturePath);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCapturePath);
            this.groupBox1.Controls.Add(this.btnSaveSettings);
            this.groupBox1.Controls.Add(this.picSave);
            this.groupBox1.Controls.Add(this.picSnagIt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbCaptureMode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDelay);
            this.groupBox1.Controls.Add(this.cmbColorDepth);
            this.groupBox1.Controls.Add(this.chkIncludeCursor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbOutputFormat);
            this.groupBox1.Location = new System.Drawing.Point(17, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 290);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(6, 270);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(66, 13);
            this.lblVersion.TabIndex = 15;
            this.lblVersion.TabStop = true;
            this.lblVersion.Text = "Version 0.90";
            this.ToolTip.SetToolTip(this.lblVersion, "Check for new version");
            this.lblVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblVersion_LinkClicked);
            // 
            // chkShowPreview
            // 
            this.chkShowPreview.AutoSize = true;
            this.chkShowPreview.Location = new System.Drawing.Point(159, 248);
            this.chkShowPreview.Name = "chkShowPreview";
            this.chkShowPreview.Size = new System.Drawing.Size(166, 17);
            this.chkShowPreview.TabIndex = 17;
            this.chkShowPreview.Text = "Show SnagIt preview window";
            this.ToolTip.SetToolTip(this.chkShowPreview, "If set causes the mouse cursor to be captured in the screen capture.");
            this.chkShowPreview.UseVisualStyleBackColor = true;
            // 
            // chkDeleteImage
            // 
            this.chkDeleteImage.AutoSize = true;
            this.chkDeleteImage.Location = new System.Drawing.Point(159, 225);
            this.chkDeleteImage.Name = "chkDeleteImage";
            this.chkDeleteImage.Size = new System.Drawing.Size(124, 17);
            this.chkDeleteImage.TabIndex = 16;
            this.chkDeleteImage.Text = "Don\'t save image file";
            this.ToolTip.SetToolTip(this.chkDeleteImage, "If set causes the mouse cursor to be captured in the screen capture.");
            this.chkDeleteImage.UseVisualStyleBackColor = true;
            // 
            // btnGetCapturePath
            // 
            this.btnGetCapturePath.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetCapturePath.Location = new System.Drawing.Point(449, 201);
            this.btnGetCapturePath.Name = "btnGetCapturePath";
            this.btnGetCapturePath.Size = new System.Drawing.Size(20, 19);
            this.btnGetCapturePath.TabIndex = 5;
            this.btnGetCapturePath.Text = "...";
            this.btnGetCapturePath.UseVisualStyleBackColor = true;
            this.btnGetCapturePath.Click += new System.EventHandler(this.btnCapturePath_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Capture Image Directory";
            // 
            // txtCapturePath
            // 
            this.txtCapturePath.Location = new System.Drawing.Point(159, 199);
            this.txtCapturePath.Name = "txtCapturePath";
            this.txtCapturePath.Size = new System.Drawing.Size(288, 20);
            this.txtCapturePath.TabIndex = 4;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.AutoSize = true;
            this.btnSaveSettings.Location = new System.Drawing.Point(397, 269);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(73, 13);
            this.btnSaveSettings.TabIndex = 6;
            this.btnSaveSettings.TabStop = true;
            this.btnSaveSettings.Text = "&Save Settings";
            this.ToolTip.SetToolTip(this.btnSaveSettings, "Save the settings on this page and make them permanent. Otherwise changes are use" +
        "d only for the current capture.");
            this.btnSaveSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSaveSettings_LinkClicked);
            // 
            // picSave
            // 
            this.picSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSave.Image = ((System.Drawing.Image)(resources.GetObject("picSave.Image")));
            this.picSave.Location = new System.Drawing.Point(382, 267);
            this.picSave.Name = "picSave";
            this.picSave.Size = new System.Drawing.Size(16, 16);
            this.picSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSave.TabIndex = 15;
            this.picSave.TabStop = false;
            this.ToolTip.SetToolTip(this.picSave, "Save the settings on this page and make them permanent. Otherwise changes are use" +
        "d only for the current capture.");
            this.picSave.Click += new System.EventHandler(this.picSave_Click);
            // 
            // picSnagIt
            // 
            this.picSnagIt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSnagIt.Image = ((System.Drawing.Image)(resources.GetObject("picSnagIt.Image")));
            this.picSnagIt.Location = new System.Drawing.Point(14, 16);
            this.picSnagIt.Name = "picSnagIt";
            this.picSnagIt.Size = new System.Drawing.Size(135, 136);
            this.picSnagIt.TabIndex = 14;
            this.picSnagIt.TabStop = false;
            this.picSnagIt.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // picWestwind
            // 
            this.picWestwind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picWestwind.Image = ((System.Drawing.Image)(resources.GetObject("picWestwind.Image")));
            this.picWestwind.Location = new System.Drawing.Point(18, 305);
            this.picWestwind.Name = "picWestwind";
            this.picWestwind.Size = new System.Drawing.Size(110, 18);
            this.picWestwind.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWestwind.TabIndex = 14;
            this.picWestwind.TabStop = false;
            this.picWestwind.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // SnagItConfigurationForm
            // 
            this.AcceptButton = this.btnCapture;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(516, 343);
            this.Controls.Add(this.picWestwind);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCapture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SnagItConfigurationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SnagIt Screen Capture";
            this.Load += new System.EventHandler(this.SnagItConfigurationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDelay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSnagIt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWestwind)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCaptureMode;
        private System.Windows.Forms.ComboBox cmbColorDepth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbOutputFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIncludeCursor;
        private System.Windows.Forms.NumericUpDown txtDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picWestwind;
        private System.Windows.Forms.LinkLabel btnSaveSettings;
        private System.Windows.Forms.PictureBox picSave;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.PictureBox picSnagIt;
        private System.Windows.Forms.TextBox txtCapturePath;
        private System.Windows.Forms.Button btnGetCapturePath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDeleteImage;
        private System.Windows.Forms.CheckBox chkShowPreview;
        private System.Windows.Forms.LinkLabel lblVersion;
    }
}