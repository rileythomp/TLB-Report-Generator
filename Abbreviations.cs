using System.Collections.Generic;

namespace ReportGeneration
{
    public sealed class Abbreviations
    {
        private static readonly Abbreviations abbreviations = new Abbreviations();
        // Explicit static constructor to tell C# compiler  
        // not to mark type as beforefieldinit  
        static Abbreviations()
        {
        }
        private Abbreviations()
        {
        }

        public static Dictionary<string, string> Desserts { get; } = new Dictionary<string, string>()
        {
            {"terra cotta chocolate chip cookie", "COOKIE"},
            {"none", ""}
        };

        public static Dictionary<string, string> Meals { get; } = new Dictionary<string, string>()
        {
            {"sweet & sour chicken nuggets w/rice", "S&S CHICK"},
            {"baked ziti",  "ZITI"},
            {"plain pasta w/butter and parmesan cheese",    "PLAIN"},
            {"penne with meat sauce",   "PENNE"},
            {"perogies (cheese & potato) w/bacon",  "PEROGIES"},
            {"beefaroni",   "BEEFARONI"},
            {"soft taco",   "TACO"},
            {"chicken tenders w/rice & plum sauce", "TENDERS"},
            {"chicken burger w/lettuce & mayo", "CHIB"},
            {"chicken burger w/lettuce",    "CHIB"},
            {"chicken parmigiana on a bun", "CPB"},
            {"grilled chicken pocket w/cheese & broccoli",  "GCP"},
            {"ranch chicken wrap w/cheese", "R WRAP"},
            {"chicken caesar wrap", "C WRAP"},
            {"quesadilla w/chicken & peppers",  "QUESA"},
            {"quesadilla w/cheese only",    "CH Q"},
            {"hamburger w/ketchup", "HAMBURG"},
            {"cheeseburger w/ketchup",  "CH BURG"},
            {"none", ""}
       };

        public static Dictionary<string, string> Vegetables { get; }  = new Dictionary<string, string>()
        {
            {"cucumber",    "CUKES"},
            {"red pepper strips",   "RED PEPP"},
            {"none", ""}
        };

        public static Dictionary<string, string> Fruits { get; } = new Dictionary<string, string>()
        {
            {"pineapple",   "PINEAPPLE"},
            {"watermelon",  "W'MELON"},
            {"oranges", "ORANGES"},
            {"grapes",  "GRAPES"},
            {"honeydew",    "H'DEW"},
            {"none", ""}
        };
    }  
}
