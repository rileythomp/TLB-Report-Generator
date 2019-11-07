using Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReportGeneration
{
    class Order
    {
        private string day;
        private string firstName;
        private string lastName;
        private string main;
        private string vegetable;
        private string fruit;
        private string dessert;

        public Order(List<string> order)
        {
            day = order[1];

            GradeTeacher = order[3];
            if (GradeTeacher == "01FDK | Ms Agostino-Hrycaj")
            {
                GradeTeacher = "01FDK | Ms A-H";
            }

            firstName = order[4].FormatAsName();
            lastName = order[5].FormatAsName();

            main = Abbreviations.Meals[order[6].ToLower()];
            vegetable = Abbreviations.Vegetables[order[7].ToLower()];
            fruit = Abbreviations.Fruits[order[8].ToLower()];
            dessert = Abbreviations.Desserts[order[9].ToLower()];
        }

        public string GradeTeacher { get; }

        public bool PlainMain
        {
            get
            {
                return main == "PLAIN";
            }
        }

        public bool BurgerMain
        {
            get
            {
                return String.Equals(main, "CHIB") || String.Equals(main, "CPB") || String.Equals(main, "GCP") || String.Equals(main, "CH Q") || String.Equals(main, "HAMBURG");
            }
        }

        public bool WrapMain
        {
            get
            {
                return String.Equals(main, "R WRAP") || String.Equals(main, "C WRAP");
            }
        }

        public bool HasMeal(string mealType)
        {
            if (String.Equals(mealType, "fruit"))
            {
                return !String.IsNullOrEmpty(fruit);
            }
            else if (String.Equals(mealType, "vegetable")) {
                return !String.IsNullOrEmpty(vegetable);
            }
            else if (String.Equals(mealType, "dessert"))
            {
                return !String.IsNullOrEmpty(dessert);
            }
            else if (String.Equals(mealType, "main"))
            {
                return !String.IsNullOrEmpty(main);
            }

            return false;
        }

        public void WriteSpecificOrder(string mealType, StreamWriter reportCsvFile)
        {
            reportCsvFile.Write($"{day}, TheLunchBasket.ca, {GradeTeacher}, {firstName}, {lastName}, ");

            if (String.Equals(mealType, "fruit"))
            {
                reportCsvFile.Write(fruit);
            } 
            else if (String.Equals(mealType, "vegetable"))
            {
                reportCsvFile.Write(vegetable);
            } 
            else if (String.Equals(mealType, "dessert"))
            {
                reportCsvFile.Write(dessert);
            } 
            else if (String.Equals(mealType, "main"))
            {
                reportCsvFile.Write(main);
            }
        }

        public void WriteDailyDetail(StreamWriter reportCsvFile)
        {
            reportCsvFile.WriteLine($"{GradeTeacher}, {firstName}, {lastName}, {main}, , , , {vegetable}, {fruit}, {dessert}");
        }
    };
}
