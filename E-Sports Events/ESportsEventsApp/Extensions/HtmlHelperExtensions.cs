namespace ESportsEventsApp.Extensions
{
    using System;
    using System.Web.Mvc;

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string imageUrl, string alt, string style)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("style", style);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString SpecialText(this HtmlHelper helper, bool isRed)
        {
            var builder = new TagBuilder("span");
            builder.MergeAttribute("style", isRed?"color:red":"color:green");
            builder.InnerHtml = isRed ? "OUT OF STOCK": "IN STOCK";

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ButtonLink(this HtmlHelper helper, string text, string id)
        {
            var builder = new TagBuilder("a");
            builder.MergeAttribute("href", $"/users/edit/{id}/");
            builder.MergeAttribute("class", "btn btn-primary");
            builder.InnerHtml = text;

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString EmailLink(this HtmlHelper helper, string address)
        {
            var builder = new TagBuilder("a");
            builder.MergeAttribute("href", $"mailto:{address}");
            builder.InnerHtml = address;

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DateFor(this HtmlHelper helper, DateTime date)
        {
            var builder = new TagBuilder("span") { InnerHtml = date.ToString("dd MMMM yyyy") };
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
    }
}