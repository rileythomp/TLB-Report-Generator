namespace Extensions
{
    public static class Extensions
    {
        public static string EmptyIfZero(this int num)
        {
            return (int.Equals(0, num) ? "" : num.ToString());
        }

        public static string FormatAsName(this string name)
        {
            string formattedName = "";
            formattedName += char.ToUpper(name[0]);
            for (int i = 1; i < name.Length; ++i)
            {
                formattedName += char.ToLower(name[i]);
            }
            return formattedName;
        }
    }
}
