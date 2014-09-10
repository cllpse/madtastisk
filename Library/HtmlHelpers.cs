using System;
using System.Web.Mvc;

namespace Library
{
    public static class HtmlHelpers
    {
        public static String Literal(this HtmlHelper helper, String text, Boolean bold)
        {
            return (bold ? "<strong>" : "") + MvcHtmlString.Create(text).ToHtmlString() + (bold ? "</strong>" : "");
        }


        public static String SubmitButton(this HtmlHelper helper, String id, String value)
        {
            return "<input type=\"submit\" id=\"" + id + "\" value=\"" + value + "\" />";
        }
    }
}