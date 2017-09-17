namespace Helpers
{
    public class ViewModelsMethods
    {
        public static string GetLocation(string first, string second)
        {
            var location = string.Empty;
            if (!first.Equals(' '))
            {
                if (!second.Equals(' '))
                {
                    location = second + ", " + first;
                }
                else
                {
                    location = first;
                }
            }
            else if (!string.IsNullOrEmpty(second))
            {
                location = second;
            }

            return location;
        }
    }
}
