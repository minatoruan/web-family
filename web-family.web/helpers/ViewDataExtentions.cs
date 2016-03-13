using System.Web.Mvc;

namespace web_family.web.helpers
{
    public static class ViewDataExtentions
    {
        public static bool HasError(this ViewDataDictionary dictionary)
        {
            return dictionary.ContainsKey("status") && dictionary["status"].Equals(0);
        }

        public static string GetMessage(this ViewDataDictionary dictionary)
        {
            if (dictionary.HasError()) return (string)dictionary["message"];
            return string.Empty;
        }
    }
}