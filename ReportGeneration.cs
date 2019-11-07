using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Extensions;

namespace ReportGeneration
{
    class ReportGeneration
    {
        private static readonly ReportGeneration reportGenerator = new ReportGeneration();

        // Explicit static constructor to tell C# compiler  
        // not to mark type as beforefieldinit  
        static ReportGeneration()
        {
        }
        private ReportGeneration()
        {
        }

        internal static void GenerateLabelReport(List<Order> orders, string reportFilePath)
        {
            StreamWriter reportCsvFile = new StreamWriter(reportFilePath);

            reportCsvFile.WriteLine("Day, School, Teacher/Grade, First Name, Last Name, Main");

            List<string> mealTypes = new List<string>() { "main", "vegetable", "fruit", "dessert" };

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
        }

        internal static void GenerateDailyReport(List<Order> orders, string reportFilePath, bool detail)
        {
            StreamWriter reportCsvFile = new StreamWriter(reportFilePath);

            string curGradeTeacher = orders[0].GradeTeacher;

            int fruits = 0;
            int vegetables = 0;
            int desserts = 0;
            int mains = 0;
            int wraps = 0;
            int plains = 0;
            int burgers = 0;

            int totalFruits = 0;
            int totalVegetables = 0;
            int totalDesserts = 0;
            int totalMains = 0;
            int totalWraps = 0;
            int totalPlains = 0;
            int totalBurgers = 0;

            if (!detail)
            {
                reportCsvFile.WriteLine($"{orders[0].School}, {orders[0].Day}");
                reportCsvFile.WriteLine("Teacher/Grade, Total, Main, Wrap, Burger, Plain, Vegetable, Fruit, Dessert");
            }

            for (int i = 0; i < orders.Count; ++i)
            {
                Order order = orders[i];

                if ((i == 0 || !String.Equals(orders[i - 1].GradeTeacher, curGradeTeacher)) && detail)
                {
                    reportCsvFile.WriteLine("Teacher/Grade, First Name, Last Name, Main, Wrap, Burger, Plain, Vegetable, Fruit, Dessert");
                }

                if (order.HasMeal("fruit"))
                {
                    fruits += 1;
                }
                if (order.HasMeal("vegetable"))
                {
                    vegetables += 1;
                }
                if (order.HasMeal("dessert"))
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

                if (detail)
                {
                    order.WriteDailyDetail(reportCsvFile);
                }

                if ((i < orders.Count - 1 && orders[i + 1].GradeTeacher != curGradeTeacher) || i == orders.Count - 1)
                {
                    int meals = mains + wraps + plains + burgers;

                    if (detail)
                    {
                        reportCsvFile.WriteLine($"Total, , {meals.EmptyIfZero()}, {mains.EmptyIfZero()}, {wraps.EmptyIfZero()}, {burgers.EmptyIfZero()}, {plains.EmptyIfZero()}, {vegetables.EmptyIfZero()}, {fruits.EmptyIfZero()}, {desserts.EmptyIfZero()}");
                        reportCsvFile.WriteLine();
                    }
                    else
                    {
                        reportCsvFile.WriteLine($"{curGradeTeacher}, {meals.EmptyIfZero()}, {mains.EmptyIfZero()}, {wraps.EmptyIfZero()}, {burgers.EmptyIfZero()}, {plains.EmptyIfZero()}, {vegetables.EmptyIfZero()}, {fruits.EmptyIfZero()}, {desserts.EmptyIfZero()}");
                    }


                    if (i != orders.Count - 1)
                    {
                        curGradeTeacher = orders[i + 1].GradeTeacher;
                    }

                    totalMains += mains;
                    totalWraps += wraps;
                    totalPlains += plains;
                    totalBurgers += burgers;
                    totalVegetables += vegetables;
                    totalFruits += fruits;
                    totalDesserts += desserts;

                    vegetables = 0;
                    fruits = 0;
                    desserts = 0;
                    mains = 0;
                    wraps = 0;
                    plains = 0;
                    burgers = 0;
                }
            }

            if (!detail)
            {
                int totalMeals = totalMains + totalWraps + totalPlains + totalBurgers;
                reportCsvFile.WriteLine($"Total, {totalMeals.EmptyIfZero()}, {totalMains.EmptyIfZero()}, {totalWraps.EmptyIfZero()}, {totalBurgers.EmptyIfZero()}, {totalPlains.EmptyIfZero()}, {totalVegetables.EmptyIfZero()}, {totalFruits.EmptyIfZero()}, {totalDesserts.EmptyIfZero()}");
            }

            reportCsvFile.Close();
        }
    }
}
