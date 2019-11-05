using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ReportGeneration
{
    public partial class ReportGeneration : Form
    {
        private List<StreamReader> csvFileReaders = new List<StreamReader>();
        public ReportGeneration()
        {
            InitializeComponent();
        }

        private void uploadCsvFileClick(object sender, EventArgs ev)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    StreamReader csvFileReader = new StreamReader(filePath);
                    csvFileReaders.Add(csvFileReader);
                    csvFilesLabel.Text += filePath + Environment.NewLine;
                }
            }
        }

        private void generateLabelReport(object sender, EventArgs ev)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() != DialogResult.OK)
            {
                reportCompletionLabel.Text = "Please select a valid folder";
                return;
            }

            foreach (StreamReader csvFileReader in csvFileReaders)
            {
                string csvFilePath = (csvFileReader.BaseStream as FileStream).Name;
                string csvFileName = csvFilePath.Substring(csvFilePath.LastIndexOf('\\') + 1);
                string reportFilePath = folder.SelectedPath + @"\" + csvFileName;

                StreamWriter file = new StreamWriter(reportFilePath);

                for (int i = 0; i < 10; ++i)
                {
                    var line = csvFileReader.ReadLine();
                    file.WriteLine(line);
                }

                file.Close();

                reportCompletionLabel.Text += "Successfully generated " + reportFilePath + Environment.NewLine;
            }
        }

        private void generateDetailReport(object sender, EventArgs ev)
        {

        }

        private void generateSummaryReport(object sender, EventArgs ev)
        {

        }

    }
}
