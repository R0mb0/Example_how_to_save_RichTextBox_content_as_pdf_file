using iText.Html2pdf;
using RtfPipe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.Kernel.Pdf.Colorspace.PdfDeviceCs;

namespace Example
{
    public partial class Form : System.Windows.Forms.Form
    {
        //----FIELDS----
        private string RTFpath = null;
        private string savePath = null;

        //----BUILDER----
        public Form()
        {
            InitializeComponent();

            //Hide loading gif
            this.loading.Hide();
        }

        //----PRIVATE METHODS----

        //Change element on a string
        private string ChangeString(string original, string find, string replace)
        {
            StringBuilder builder = new StringBuilder(original);
            builder.Replace(find, replace);
            return builder.ToString();
        }

        //----OPEN BUTTON----
        private void BOpen_Click(object sender, EventArgs e)
        {
            //Show loading gif
            this.loading.Show();

            // Create an OpenFileDialog to request a file to open.
            OpenFileDialog openFile1 = new OpenFileDialog();

            // Initialize OpenFileDialog to open pdf files.
            openFile1.DefaultExt = "*.rtf";
            openFile1.Filter = "RTF Files|*.rtf";

            // Determine whether the user selected a file from the OpenFileDialog. 
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile1.FileName.Length > 0)
            {
                this.RTFpath = openFile1.FileName;
            }

            this.TextBox.LoadFile(this.RTFpath);

            //Hide loading gif
            this.loading.Hide();
        }

        private void BSave_Click(object sender, EventArgs e)
        {
            //Show loading gif
            this.loading.Show();

            //Check if RTB is empty
            if (this.TextBox.Rtf.Length != 0)
            {

                if (this.RTFpath != null)
                {
                    //Open dialog in wau to select where save
                    System.Windows.Forms.SaveFileDialog saveFileDialog1;
                    saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                    //Set default extension
                    saveFileDialog1.DefaultExt = "pdf";
                    //Set dafult path where save
                    saveFileDialog1.FileName = ChangeString(this.RTFpath, ".rtf", " ");
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        this.savePath = saveFileDialog1.FileName;
                    }
                }
                else 
                {
                    //Open dialog in wau to select where save
                    System.Windows.Forms.SaveFileDialog saveFileDialog1;
                    saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        this.savePath = saveFileDialog1.FileName;
                    }
                }

                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    HtmlConverter.ConvertToPdf(Rtf.ToHtml(this.TextBox.Rtf.ToString()), new FileStream(this.savePath, FileMode.Create));
                }
                catch 
                {
                    MessageBox.Show("Problems during the saving!");
                }

                if (File.Exists(this.savePath))
                {
                    MessageBox.Show("The file has been saved successfully");
                }
                
            }
            else 
            {
                MessageBox.Show("No document to save");
            }

            //Hide loading gif
            this.loading.Hide();
        }

        //----CLEAR BUTTON----
        private void BClear_Click(object sender, EventArgs e)
        {
            this.TextBox.Clear();
        }
    }
}
