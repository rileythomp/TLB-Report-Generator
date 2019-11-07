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
        private Button generateAllReportsButton;
        private Label reportCompletionLabel;
        private Label csvFilesLabel;
        private CheckBox labelCheckBox;
        private CheckBox detailCheckBox;
        private CheckBox summaryCheckBox;
        private Label labelLabel;
        private Label detailLabel;
        private Label summaryLabel;

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
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.WindowState = FormWindowState.Maximized;
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
            csvFilesLabel.Width = 400;
            csvFilesLabel.Height = 180;
            csvFilesLabel.ForeColor = Color.Black;
            Controls.Add(csvFilesLabel);

            labelCheckBox = new CheckBox();
            labelCheckBox.Location = new Point(10, 10 + uploadCsvFileButton.Location.Y + uploadCsvFileButton.Height);
            labelCheckBox.Width = 15;
            labelCheckBox.Height = 15;
            Controls.Add(labelCheckBox);

            labelLabel = new Label();
            labelLabel.Text = "Label Report";
            labelLabel.Location = new Point(labelCheckBox.Width + labelCheckBox.Location.X, 10 + uploadCsvFileButton.Location.Y + uploadCsvFileButton.Height);
            Controls.Add(labelLabel);

            detailCheckBox = new CheckBox();
            detailCheckBox.Location = new Point(10, 10 + labelCheckBox.Location.Y + labelCheckBox.Height);
            detailCheckBox.Width = 15;
            detailCheckBox.Height = 15;
            Controls.Add(detailCheckBox);

            detailLabel = new Label();
            detailLabel.Text = "Detail Report";
            detailLabel.Location = new Point(detailCheckBox.Width + detailCheckBox.Location.X, 10 + labelCheckBox.Location.Y + labelCheckBox.Height);
            Controls.Add(detailLabel);

            summaryCheckBox = new CheckBox();
            summaryCheckBox.Location = new Point(10, 10 + detailCheckBox.Location.Y + detailCheckBox.Height);
            summaryCheckBox.Width = 15;
            summaryCheckBox.Height = 15;
            Controls.Add(summaryCheckBox);

            summaryLabel = new Label();
            summaryLabel.Text = "Summary Report";
            summaryLabel.Location = new Point(summaryCheckBox.Width + summaryCheckBox.Location.X, 10 + detailCheckBox.Location.Y + detailCheckBox.Height);
            Controls.Add(summaryLabel);

            generateAllReportsButton = new Button();
            generateAllReportsButton.Location = new Point(10, 10 + summaryCheckBox.Location.Y + summaryCheckBox.Height);
            generateAllReportsButton.Width = 135;
            generateAllReportsButton.Height = 45;
            generateAllReportsButton.ForeColor = Color.Black;
            generateAllReportsButton.Text = "Generate Reports";
            generateAllReportsButton.Name = "AllReports";
            generateAllReportsButton.Click += new EventHandler(GenerateReportsClick);
            Controls.Add(generateAllReportsButton);


            reportCompletionLabel = new Label();
            reportCompletionLabel.Location = new Point(10, 10 + generateAllReportsButton.Location.Y + generateAllReportsButton.Height);
            reportCompletionLabel.Width = 500;
            reportCompletionLabel.Height = 500;
            reportCompletionLabel.ForeColor = Color.Black;
            Controls.Add(reportCompletionLabel);
        }

        #endregion
    }
}

