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

namespace LogBlog_WinForm_v0._2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Show the file select dialog and assign the result.
                DialogResult result = openFileDialog.ShowDialog();
                string fileAddress = openFileDialog.FileName;

                //Ensure fileNameBox is empty
                this.fileNameBox.Clear();
                
                //Show user the path and filename
                UpdateFileNameBox(fileAddress);
            }
            catch (Exception ex)
            {
                UpdateOutput("EXCEPTION - " + ex.Message + Environment.NewLine);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void UpdateOutput (String text)
        {
           this.outputBox.AppendText(text);
        }

        public void UpdateFileNameBox(String text)
        {
            this.fileNameBox.AppendText(text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            try
            {
                string fileAddress = fileNameBox.Text;
                string fileName = Path.GetFileName(fileAddress);
                string filePath = Path.GetDirectoryName(fileAddress) + "\\";

                //Get how many lines are required and assign
                int lines = int.Parse(numberBox.Text);

                // Initiate the cut of the file
                var firstLines = File.ReadLines(fileAddress).Take(lines).ToList();
                UpdateOutput("Succesfully taken " + lines + " lines from " + fileName + Environment.NewLine);

                // Build the new file path
                string newFileAddress = filePath + lines + "_" + fileName;

                //Write the lines to the new file
                TextWriter tw = new StreamWriter(newFileAddress);

                foreach (String s in firstLines)
                    tw.WriteLine(s);

                tw.Close();
                UpdateOutput("Succesfully output " + lines + " lines to " + newFileAddress + Environment.NewLine + Environment.NewLine);
            }
            catch (Exception ex)
            {
                UpdateOutput("EXCEPTION - " + ex.Message + Environment.NewLine);
            }
        }
    }
}
