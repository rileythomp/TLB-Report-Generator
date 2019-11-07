using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Extensions;

namespace ReportGeneration
{
    public partial class ReportGeneration : Form
    {
        private List<StreamReader> csvFiles = new List<StreamReader>();
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
                    csvFiles.Add(csvFileReader);
                    csvFilesLabel.Text += filePath + Environment.NewLine;
                }
            }
        }

        private void GenerateLabelReportClick(object sender, EventArgs ev)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() != DialogResult.OK)
            {
                reportCompletionLabel.Text = "Please select a valid folder";
                return;
            }

            foreach (StreamReader csvFile in csvFiles)
            {

                List<Order> orders = new List<Order>();
                // create the orders first
                string csvLine;
                csvFile.ReadLine();
                while ((csvLine = csvFile.ReadLine()) != null)
                {
                    csvLine = csvLine.Replace("'", "");
                    List<string> orderCells= csvLine.Split(',').Select(csvCell => csvCell.Trim()).ToList<string>();

                    Order order = new Order(orderCells);
                    orders.Add(order);
                }

                string csvFilePath = (csvFile.BaseStream as FileStream).Name;
                string csvFileName = csvFilePath.Substring(csvFilePath.LastIndexOf('\\') + 1);
                csvFileName = csvFileName.Substring(0, csvFileName.LastIndexOf('.'));
                string reportFilePath = folder.SelectedPath + @"\" + csvFileName + "_label_report.csv";

                StreamWriter reportCsvFile = new StreamWriter(reportFilePath);

                reportCsvFile.WriteLine("Day, School, Teacher/Grade, First Name, Last Name, Main");

                List<string> mealTypes = new List<string>(){"main", "vegetable", "fruit", "dessert" };

                foreach (var mealType in mealTypes)
                {
                    foreach (var order in orders)
                    {
                        if (order.HasMeal(mealType))
                        {
                            order.WriteSpecificOrder(mealType, reportCsvFile);
                            reportCsvFile.WriteLine();
                        }
                    }
                    reportCsvFile.WriteLine();
                }

                reportCsvFile.Close();

                reportCompletionLabel.Text += "Successfully generated " + reportFilePath + Environment.NewLine;
            }

            csvFiles.Clear();
        }

        private void GenerateDetailReportClick(object sender, EventArgs ev)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() != DialogResult.OK)
            {
                reportCompletionLabel.Text = "Please select a valid folder";
                return;
            }

            foreach (StreamReader csvFile in csvFiles)
            {

                List<Order> orders = new List<Order>();
                // create the orders first
                string csvLine;
                csvFile.ReadLine();
                while ((csvLine = csvFile.ReadLine()) != null)
                {
                    csvLine = csvLine.Replace("'", "");
                    List<string> orderCells = csvLine.Split(',').Select(csvCell => csvCell.Trim()).ToList<string>();

                    Order order = new Order(orderCells);
                    orders.Add(order);
                }

                string csvFilePath = (csvFile.BaseStream as FileStream).Name;
                string csvFileName = csvFilePath.Substring(csvFilePath.LastIndexOf('\\') + 1);
                csvFileName = csvFileName.Substring(0, csvFileName.LastIndexOf('.'));
                string reportFilePath = folder.SelectedPath + @"\" + csvFileName + "_daily_detail_report.csv";

                StreamWriter reportCsvFile = new StreamWriter(reportFilePath);

                string curGradeTeacher = orders[0].GradeTeacher;

                int fruits = 0;
                int vegetables = 0;
                int desserts = 0;
                int mains = 0;
                int wraps = 0;
                int plains = 0;
                int burgers = 0;

                for (int i = 0; i < orders.Count; ++i)
                {
                    Order order = orders[i];

                    if (i == 0 || !String.Equals(orders[i - 1].GradeTeacher, curGradeTeacher))
                    {
                        reportCsvFile.WriteLine("Teacher/Grade, First Name, Last Name, Main, Wrap, Burger, Plain, Vegetable, Fruit, Dessert");
                    }

                    if (order.HasMeal("fruit"))
                    {
                        fruits += 1;
                    }
                    else if (order.HasMeal("vegetable"))
                    {
                        vegetables += 1;
                    }
                    else if (order.HasMeal("dessert"))
                    {
                        desserts += 1;
                    }

                    if (order.PlainMain)
                    {
                        plains += 1;
                    }
                    else if (order.BurgerMain)
                    {
                        burgers += 1;
                    }
                    else if (order.WrapMain)
                    {
                        wraps += 1;
                    }
                    else
                    {
                        mains += 1;
                    }

                    order.WriteDailyDetail(reportCsvFile);

                    if ((i < orders.Count - 1 && orders[i + 1].GradeTeacher != curGradeTeacher) || i == orders.Count - 1)
                    {
                        int meals = mains + wraps + plains + burgers;

                        reportCsvFile.WriteLine($"Total, , {meals.EmptyIfZero()}, {mains.EmptyIfZero()}, {wraps.EmptyIfZero()}, {burgers.EmptyIfZero()}, {plains.EmptyIfZero()}, {vegetables.EmptyIfZero()}, {fruits.EmptyIfZero()}, {desserts.EmptyIfZero()}");
                        reportCsvFile.WriteLine();

                        if (i != orders.Count - 1)
                        {
                            curGradeTeacher = orders[i + 1].GradeTeacher;
                        }

                        vegetables = 0;
                        fruits = 0;
                        desserts = 0;
                        mains = 0;
                        wraps = 0;
                        plains = 0;
                        burgers = 0;
                    }
                }

                reportCsvFile.Close();

                reportCompletionLabel.Text += "Successfully generated " + reportFilePath + Environment.NewLine;
            }

            csvFiles.Clear();
        }

        private void GenerateSummaryReportClick(object sender, EventArgs ev)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() != DialogResult.OK)
            {
                reportCompletionLabel.Text = "Please select a valid folder";
                return;
            }

            foreach (StreamReader csvFile in csvFiles)
            {

                List<Order> orders = new List<Order>();
                // create the orders first
                string csvLine;
                csvFile.ReadLine();
                while ((csvLine = csvFile.ReadLine()) != null)
                {
                    csvLine = csvLine.Replace("'", "");
                    List<string> orderCells = csvLine.Split(',').Select(csvCell => csvCell.Trim()).ToList<string>();

                    Order order = new Order(orderCells);
                    orders.Add(order);
                }

                string csvFilePath = (csvFile.BaseStream as FileStream).Name;
                string csvFileName = csvFilePath.Substring(csvFilePath.LastIndexOf('\\') + 1);
                csvFileName = csvFileName.Substring(0, csvFileName.LastIndexOf('.'));
                string reportFilePath = folder.SelectedPath + @"\" + csvFileName + "_daily_summary_report.csv";

                StreamWriter reportCsvFile = new StreamWriter(reportFilePath);

                string curGradeTeacher = orders[0].GradeTeacher;

                int fruits = 0;
                int vegetables = 0;
                int desserts = 0;
                int mains = 0;
                int wraps = 0;
                int plains = 0;
                int burgers = 0;

                reportCsvFile.WriteLine("Teacher/Grade, Total, Main, Wrap, Burger, Plain, Vegetable, Fruit, Dessert");

                for (int i = 0; i < orders.Count; ++i)
                {
                    Order order = orders[i];

                    if (order.HasMeal("fruit"))
                    {
                        fruits += 1;
                    }
                    else if (order.HasMeal("vegetable"))
                    {
                        vegetables += 1;
                    }
                    else if (order.HasMeal("dessert"))
                    {
                        desserts += 1;
                    }

                    if (order.PlainMain)
                    {
                        plains += 1;
                    }
                    else if (order.BurgerMain)
                    {
                        burgers += 1;
                    }
                    else if (order.WrapMain)
                    {
                        wraps += 1;
                    }
                    else
                    {
                        mains += 1;
                    }

                    if ((i < orders.Count - 1 && orders[i + 1].GradeTeacher != curGradeTeacher) || i == orders.Count - 1)
                    {
                        int meals = mains + wraps + plains + burgers;

                        reportCsvFile.WriteLine($"{curGradeTeacher}, {meals.EmptyIfZero()}, {mains.EmptyIfZero()}, {wraps.EmptyIfZero()}, {burgers.EmptyIfZero()}, {plains.EmptyIfZero()}, {vegetables.EmptyIfZero()}, {fruits.EmptyIfZero()}, {desserts.EmptyIfZero()}");

                        if (i != orders.Count - 1)
                        {
                            curGradeTeacher = orders[i + 1].GradeTeacher;
                        }

                        vegetables = 0;
                        fruits = 0;
                        desserts = 0;
                        mains = 0;
                        wraps = 0;
                        plains = 0;
                        burgers = 0;
                    }
                }

                reportCsvFile.Close();

                reportCompletionLabel.Text += "Successfully generated " + reportFilePath + Environment.NewLine;
            }

            csvFiles.Clear();
        }

        private void GenerateAllReportsClick(object sender, EventArgs ev)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() != DialogResult.OK)
            {
                reportCompletionLabel.Text = "Please select a valid folder";
                return;
            }

            foreach (StreamReader csvFile in csvFiles)
            {

                List<Order> orders = new List<Order>();
                // create the orders first
                string csvLine;
                csvFile.ReadLine();
                while ((csvLine = csvFile.ReadLine()) != null)
                {
                    csvLine = csvLine.Replace("'", "");
                    List<string> orderCells = csvLine.Split(',').Select(csvCell => csvCell.Trim()).ToList<string>();

                    Order order = new Order(orderCells);
                    orders.Add(order);
                }

                string csvFilePath = (csvFile.BaseStream as FileStream).Name;
                string csvFileName = csvFilePath.Substring(csvFilePath.LastIndexOf('\\') + 1);
                csvFileName = csvFileName.Substring(0, csvFileName.LastIndexOf('.'));

                // label
                string labelFilePath = folder.SelectedPath + @"\" + csvFileName + "_label_report.csv";

                StreamWriter labelCsvFile = new StreamWriter(labelFilePath);

                labelCsvFile.WriteLine("Day, School, Teacher/Grade, First Name, Last Name, Main");

                List<string> mealTypes = new List<string>() { "main", "vegetable", "fruit", "dessert" };

                foreach (var mealType in mealTypes)
                {
                    foreach (var order in orders)
                    {
                        if (order.HasMeal(mealType))
                        {
                            order.WriteSpecificOrder(mealType, labelCsvFile);
                            labelCsvFile.WriteLine();
                        }
                    }
                    labelCsvFile.WriteLine();
                }

                labelCsvFile.Close();

                reportCompletionLabel.Text += "Successfully generated " + labelFilePath + Environment.NewLine;

                // end label

                // detail
                string detailFilePath = folder.SelectedPath + @"\" + csvFileName + "_daily_detail_report.csv";

                StreamWriter detailCsvFile = new StreamWriter(detailFilePath);

                string curGradeTeacher = orders[0].GradeTeacher;

                int fruits = 0;
                int vegetables = 0;
                int desserts = 0;
                int mains = 0;
                int wraps = 0;
                int plains = 0;
                int burgers = 0;

                for (int i = 0; i < orders.Count; ++i)
                {
                    Order order = orders[i];

                    if (i == 0 || !String.Equals(orders[i - 1].GradeTeacher, curGradeTeacher))
                    {
                        detailCsvFile.WriteLine("Teacher/Grade, First Name, Last Name, Main, Wrap, Burger, Plain, Vegetable, Fruit, Dessert");
                    }

                    if (order.HasMeal("fruit"))
                    {
                        fruits += 1;
                    }
                    else if (order.HasMeal("vegetable"))
                    {
                        vegetables += 1;
                    }
                    else if (order.HasMeal("dessert"))
                    {
                        desserts += 1;
                    }

                    if (order.PlainMain)
                    {
                        plains += 1;
                    }
                    else if (order.BurgerMain)
                    {
                        burgers += 1;
                    }
                    else if (order.WrapMain)
                    {
                        wraps += 1;
                    }
                    else
                    {
                        mains += 1;
                    }

                    order.WriteDailyDetail(detailCsvFile);

                    if ((i < orders.Count - 1 && orders[i + 1].GradeTeacher != curGradeTeacher) || i == orders.Count - 1)
                    {
                        int meals = mains + wraps + plains + burgers;

                        detailCsvFile.WriteLine($"Total, , {meals.EmptyIfZero()}, {mains.EmptyIfZero()}, {wraps.EmptyIfZero()}, {burgers.EmptyIfZero()}, {plains.EmptyIfZero()}, {vegetables.EmptyIfZero()}, {fruits.EmptyIfZero()}, {desserts.EmptyIfZero()}");
                        detailCsvFile.WriteLine();

                        if (i != orders.Count - 1)
                        {
                            curGradeTeacher = orders[i + 1].GradeTeacher;
                        }

                        vegetables = 0;
                        fruits = 0;
                        desserts = 0;
                        mains = 0;
                        wraps = 0;
                        plains = 0;
                        burgers = 0;
                    }
                }

                detailCsvFile.Close();

                reportCompletionLabel.Text += "Successfully generated " + detailFilePath + Environment.NewLine;
                // end detail

                // summary
                string summaryFilePath = folder.SelectedPath + @"\" + csvFileName + "_daily_summary_report.csv";

                StreamWriter summaryCsvFile = new StreamWriter(summaryFilePath);

                curGradeTeacher = orders[0].GradeTeacher;

                fruits = 0;
                vegetables = 0;
                desserts = 0;
                mains = 0;
                wraps = 0;
                plains = 0;
                burgers = 0;

                summaryCsvFile.WriteLine("Teacher/Grade, Total, Main, Wrap, Burger, Plain, Vegetable, Fruit, Dessert");

                for (int i = 0; i < orders.Count; ++i)
                {
                    Order order = orders[i];

                    if (order.HasMeal("fruit"))
                    {
                        fruits += 1;
                    }
                    else if (order.HasMeal("vegetable"))
                    {
                        vegetables += 1;
                    }
                    else if (order.HasMeal("dessert"))
                    {
                        desserts += 1;
                    }

                    if (order.PlainMain)
                    {
                        plains += 1;
                    }
                    else if (order.BurgerMain)
                    {
                        burgers += 1;
                    }
                    else if (order.WrapMain)
                    {
                        wraps += 1;
                    }
                    else
                    {
                        mains += 1;
                    }

                    if ((i < orders.Count - 1 && orders[i + 1].GradeTeacher != curGradeTeacher) || i == orders.Count - 1)
                    {
                        int meals = mains + wraps + plains + burgers;

                        summaryCsvFile.WriteLine($"{curGradeTeacher}, {meals.EmptyIfZero()}, {mains.EmptyIfZero()}, {wraps.EmptyIfZero()}, {burgers.EmptyIfZero()}, {plains.EmptyIfZero()}, {vegetables.EmptyIfZero()}, {fruits.EmptyIfZero()}, {desserts.EmptyIfZero()}");

                        if (i != orders.Count - 1)
                        {
                            curGradeTeacher = orders[i + 1].GradeTeacher;
                        }

                        vegetables = 0;
                        fruits = 0;
                        desserts = 0;
                        mains = 0;
                        wraps = 0;
                        plains = 0;
                        burgers = 0;
                    }
                }

                summaryCsvFile.Close();

                reportCompletionLabel.Text += "Successfully generated " + summaryFilePath + Environment.NewLine;
                // end summary
            }

            csvFiles.Clear();
        }
    }
}
