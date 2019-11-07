namespace Extensions
{
    public static class Extensions
    {
        public static string EmptyIfZero(this int num)
        {
            return (int.Equals(0, num) ? "" : num.ToString());
        }
    }
}
