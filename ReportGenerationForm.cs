using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Extensions;

namespace ReportGeneration
{
    public partial class ReportGenerationForm : Form
    {
        private List<StreamReader> csvFiles = new List<StreamReader>();

        private Button uploadCsvFileButton;
        private Label csvFilesLabel;

        private CheckBox labelCheckBox;
        private CheckBox detailCheckBox;
        private CheckBox summaryCheckBox;
        private Label labelLabel;
        private Label detailLabel;
        private Label summaryLabel;

        private Button generateAllReportsButton;
        private Label reportCompletionLabel;

        const int checkBoxHeight = 15;
        const int checkBoxWidth = 15;

        const int buttonHeight = 45;
        const int buttonWidth = 135;

        public ReportGenerationForm()
        {
            InitializeComponent();

            this.ClientSize = new Size(500, 450);
            this.WindowState = FormWindowState.Maximized;
            this.Text = "The Lunch Basket Report Generation";

            uploadCsvFileButton = new Button();
            uploadCsvFileButton.Location = new Point(10, 10);
            uploadCsvFileButton.Width = buttonWidth;
            uploadCsvFileButton.Height = buttonHeight;
            uploadCsvFileButton.Text = "Upload CSV file";
            uploadCsvFileButton.Click += new EventHandler(uploadCsvFileClick);
            Controls.Add(uploadCsvFileButton);

            csvFilesLabel = new Label();
            csvFilesLabel.Location = new Point(10 + uploadCsvFileButton.Location.X + uploadCsvFileButton.Width, 10);
            csvFilesLabel.Width = 400;
            csvFilesLabel.Height = 180;
            Controls.Add(csvFilesLabel);

            labelCheckBox = new CheckBox();
            labelCheckBox.Location = new Point(10, 10 + uploadCsvFileButton.Location.Y + uploadCsvFileButton.Height);
            labelCheckBox.Width = checkBoxWidth;
            labelCheckBox.Height = checkBoxHeight;
            Controls.Add(labelCheckBox);

            labelLabel = new Label();
            labelLabel.Text = "Label Report";
            labelLabel.Location = new Point(labelCheckBox.Width + labelCheckBox.Location.X, 10 + uploadCsvFileButton.Location.Y + uploadCsvFileButton.Height);
            Controls.Add(labelLabel);

            detailCheckBox = new CheckBox();
            detailCheckBox.Location = new Point(10, 10 + labelCheckBox.Location.Y + labelCheckBox.Height);
            detailCheckBox.Width = checkBoxWidth;
            detailCheckBox.Height = checkBoxHeight;
            Controls.Add(detailCheckBox);

            detailLabel = new Label();
            detailLabel.Text = "Detail Report";
            detailLabel.Location = new Point(detailCheckBox.Width + detailCheckBox.Location.X, 10 + labelCheckBox.Location.Y + labelCheckBox.Height);
            Controls.Add(detailLabel);

            summaryCheckBox = new CheckBox();
            summaryCheckBox.Location = new Point(10, 10 + detailCheckBox.Location.Y + detailCheckBox.Height);
            summaryCheckBox.Width = checkBoxWidth;
            summaryCheckBox.Height = checkBoxHeight;
            Controls.Add(summaryCheckBox);

            summaryLabel = new Label();
            summaryLabel.Text = "Summary Report";
            summaryLabel.Location = new Point(summaryCheckBox.Width + summaryCheckBox.Location.X, 10 + detailCheckBox.Location.Y + detailCheckBox.Height);
            Controls.Add(summaryLabel);

            generateAllReportsButton = new Button();
            generateAllReportsButton.Location = new Point(10, 10 + summaryCheckBox.Location.Y + summaryCheckBox.Height);
            generateAllReportsButton.Width = buttonWidth;
            generateAllReportsButton.Height = buttonHeight;
            generateAllReportsButton.Text = "Generate Reports";
            generateAllReportsButton.Name = "AllReports";
            generateAllReportsButton.Click += new EventHandler(GenerateReportsClick);
            Controls.Add(generateAllReportsButton);


            reportCompletionLabel = new Label();
            reportCompletionLabel.Location = new Point(10, 10 + generateAllReportsButton.Location.Y + generateAllReportsButton.Height);
            reportCompletionLabel.Width = 500;
            reportCompletionLabel.Height = 500;
            Controls.Add(reportCompletionLabel);
        }

        private void uploadCsvFileClick(object sender, EventArgs ev)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                csvFilesLabel.Text = "";

                foreach (string filePath in openFileDialog.FileNames)
                {
                    StreamReader csvFileReader = new StreamReader(filePath);
                    csvFiles.Add(csvFileReader);
                    csvFilesLabel.Text += filePath + Environment.NewLine;
                    reportCompletionLabel.Text = "";
                }
            }
        }

        private void GenerateReportsClick(object sender, EventArgs ev)
        {
            if (csvFiles.Count == 0)
            {
                reportCompletionLabel.Text = "Please select a CSV file";
                return;
            }
            else if (!labelCheckBox.Checked && !detailCheckBox.Checked && !summaryCheckBox.Checked)
            {
                reportCompletionLabel.Text = "Please select a report type to generate";
                return;
            }

            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() != DialogResult.OK)
            {
                reportCompletionLabel.Text = "Please select a valid folder";
                return;
            }

            reportCompletionLabel.Text = "";

            foreach (StreamReader csvFile in csvFiles)
            {

                List<Order> orders = new List<Order>();

                string csvLine;
                csvFile.ReadLine();
                while ((csvLine = csvFile.ReadLine()) != null)
                {
                    csvLine = csvLine.Replace("'", "");
                    List<string> orderCells = csvLine.Split(',').Select(csvCell => csvCell.Trim()).ToList<string>();

                    Order order = new Order(orderCells);
                    orders.Add(order);
                }

                if (orders.Count == 0)
                {
                    reportCompletionLabel.Text = "There were no orders";
                    return;
                }

                string csvFilePath = (csvFile.BaseStream as FileStream).Name;
                string csvFileName = csvFilePath.Substring(csvFilePath.LastIndexOf('\\') + 1);
                csvFileName = csvFileName.Substring(0, csvFileName.LastIndexOf('.'));
                string reportFilePath = folder.SelectedPath + @"\" + csvFileName;

                if (labelCheckBox.Checked)
                {
                    ReportGeneration.GenerateLabelReport(orders, reportFilePath + "_label_report.csv");
                    reportCompletionLabel.Text += "Successfully generated " + reportFilePath + "_label_report.csv" + Environment.NewLine;
                }
                if (detailCheckBox.Checked)
                {
                    ReportGeneration.GenerateDailyReport(orders, reportFilePath + "_detail_report.csv", true);
                    reportCompletionLabel.Text += "Successfully generated " + reportFilePath + "_detail_report.csv" + Environment.NewLine;
                }
                if (summaryCheckBox.Checked) 
                {
                    ReportGeneration.GenerateDailyReport(orders, reportFilePath + "_summary_report.csv", false);
                    reportCompletionLabel.Text += "Successfully generated " + reportFilePath + "_summary_report.csv" + Environment.NewLine;

                }
            }

            csvFiles.Clear();
        }
    }
}
