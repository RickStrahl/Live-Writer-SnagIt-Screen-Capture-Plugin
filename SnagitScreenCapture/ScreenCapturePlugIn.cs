using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace SnagitScreenCapturePlugin
{
    [WriterPlugin( "7E113E74-A693-4bcd-8CF8-4C732654699C", 
        "SnagIt Screen Capture",        
		 ImagePath = "Images.snagit.png",
		 PublisherUrl = "http://www.west-wind.com/tools/SnagitLiveWriterPlugin.aspx",
		 Description = "Embeds a screen capture image from SnagIt.")]

	[InsertableContentSource( "SnagIt Screen Capture" )]
	public class SnagitScreenCapturePlugin : ContentSource
	{
		public SnagitScreenCapturePlugin()
		{
		}

		public override DialogResult CreateContent(IWin32Window dialogOwner, ref string newContent)
		{		
            DialogResult dr = DialogResult.OK;

            // *** Result Output file captured
            string OutputFile = null;
            
            try
            {
                SnagItAutomation SnagIt = SnagItAutomation.Create();
                SnagIt.ActiveForm = Form.ActiveForm;

                SnagItConfigurationForm ConfigForm = new SnagItConfigurationForm(SnagIt);
                if (ConfigForm.ShowDialog() == DialogResult.Cancel)
                    return DialogResult.Cancel;

                OutputFile = SnagIt.CaptureImageToFile();

                SnagIt.SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to capture image:\r\n\r\n" + ex.Message,
                                "SnagIt Capture Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return DialogResult.Cancel;
            }

            // *** Just embed the image
            if (!string.IsNullOrEmpty(OutputFile))                    
                newContent = @"<img src='file:///" + OutputFile + "'>\r\n";

            return dr;
		}
        

	}
}
