using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReportGeneration
{
    partial class ReportGeneration
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  UI elements.
        /// </summary>
        private Button uploadCsvFileButton;
        private Button generateLabelReportButton;
        private Button generateDetailReportButton;
        private Button generateSummaryReportButton;
        private Label reportCompletionLabel;
        private Label csvFilesLabel;

        /// <summary>
        /// </summary>

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Text = "The Lunch Basket Report Generation";

            uploadCsvFileButton = new Button();
            uploadCsvFileButton.Location = new Point(10, 10);
            uploadCsvFileButton.Width = 135;
            uploadCsvFileButton.Height = 45;
            uploadCsvFileButton.ForeColor = Color.Black;
            uploadCsvFileButton.Text = "Upload CSV file";
            uploadCsvFileButton.Click += new EventHandler(uploadCsvFileClick);
            Controls.Add(uploadCsvFileButton);

            csvFilesLabel = new Label();
            csvFilesLabel.Location = new Point(10 + uploadCsvFileButton.Location.X + uploadCsvFileButton.Width, 10);
            csvFilesLabel.Width = 200;
            csvFilesLabel.Height = 200;
            csvFilesLabel.ForeColor = Color.Black;
            Controls.Add(csvFilesLabel);

            generateLabelReportButton = new Button();
            generateLabelReportButton.Location = new Point(10, 10 + uploadCsvFileButton.Location.Y + uploadCsvFileButton.Height);
            generateLabelReportButton.Width = 135;
            generateLabelReportButton.Height = 45;
            generateLabelReportButton.ForeColor = Color.Black;
            generateLabelReportButton.Text = "Generate Label Report";
            generateLabelReportButton.Click += new EventHandler(generateLabelReport);
            Controls.Add(generateLabelReportButton);

            generateDetailReportButton = new Button();
            generateDetailReportButton.Location = new Point(10, 10 + generateLabelReportButton.Location.Y + generateLabelReportButton.Height);
            generateDetailReportButton.Width = 135;
            generateDetailReportButton.Height = 45;
            generateDetailReportButton.ForeColor = Color.Black;
            generateDetailReportButton.Text = "Generate Detail Report";
            generateDetailReportButton.Click += new EventHandler(generateDetailReport);
            Controls.Add(generateDetailReportButton);

            generateSummaryReportButton = new Button();
            generateSummaryReportButton.Location = new Point(10, 10 + generateDetailReportButton.Location.Y + generateDetailReportButton.Height);
            generateSummaryReportButton.Width = 135;
            generateSummaryReportButton.Height = 45;
            generateSummaryReportButton.ForeColor = Color.Black;
            generateSummaryReportButton.Text = "Generate Summary Report";
            generateSummaryReportButton.Click += new EventHandler(generateSummaryReport);
            Controls.Add(generateSummaryReportButton);

            reportCompletionLabel = new Label();
            reportCompletionLabel.Location = new Point(10, 10 + generateSummaryReportButton.Location.Y + generateSummaryReportButton.Height);
            reportCompletionLabel.Width = 350;
            reportCompletionLabel.Height = 200;
            reportCompletionLabel.ForeColor = Color.Black;
            Controls.Add(reportCompletionLabel);
        }

        #endregion
    }
}

