namespace Web.Controllers
{
    public class Utils
    {
        public const int MAX_STRING_IN_LIST_LENGTH = 20;
        public const int MIN_STRING_IN_LIST_LENGTH = 5;

        public static string PrepareStringForItemList(string source)
        {
            return PrepareStringForItemList(source, MAX_STRING_IN_LIST_LENGTH);
        }

        public static string PrepareStringForItemList(string source, int length)
        {
            if (length < MIN_STRING_IN_LIST_LENGTH)
            {
                length = MIN_STRING_IN_LIST_LENGTH;
            }

            if (null == source || source.Length < length - 2)
            {
                return source;
            }
            
            return source.Substring(0, length - 3) + "...";
        }
    }
}