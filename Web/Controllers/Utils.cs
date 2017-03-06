using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Controllers
{
    public static class Utils
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

		public static IHtmlString AngularActionLink(this HtmlHelper helper, string titleExpression, string actionName, 
				string idExpression) {
			var url = LinkExtensions.ActionLink(helper, titleExpression, actionName, 
					new { id = "[[[__to_replace__]]]" });
			var urlString = HttpUtility.HtmlDecode(url.ToString());
			urlString = urlString.Replace("[[[__to_replace__]]]", idExpression);
			return helper.Raw(urlString);
		}

		public static IHtmlString AngularActionLink(this HtmlHelper helper, string titleExpression, string actionName,
				string idExpression, object htmlAttributes) {
			var url = LinkExtensions.ActionLink(helper, titleExpression, actionName,
					new { id = "[[[__to_replace__]]]" }, htmlAttributes);
			var urlString = HttpUtility.HtmlDecode(url.ToString());
			urlString = urlString.Replace("[[[__to_replace__]]]", idExpression);
			return helper.Raw(urlString);
		}
	}
}
