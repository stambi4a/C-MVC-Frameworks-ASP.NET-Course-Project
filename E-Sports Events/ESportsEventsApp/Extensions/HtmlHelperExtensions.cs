namespace ESportsEventsApp.Extensions
{
    using System;
    using System.Web.Mvc;

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string imageUrl, string alt, string style, string classAttr, string idAttr)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("class",classAttr);
            builder.MergeAttribute("id", idAttr);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

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

        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string value)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttribute("type", "Submit");
            builder.MergeAttribute("value", value);
            builder.MergeAttribute("class", "btn btn-default");
            //builder.InnerHtml = text;

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
            var builder = new TagBuilder("span") { InnerHtml = date.ToString("dddd, dd MMMM yyyy H:mm tt") };

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString VideoHdIFrame(this HtmlHelper helper, string url)
        {
            var builder = new TagBuilder("iframe");
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("frameborder", "0");
            //builder.MergeAttribute("class", "embed-responsive-item");

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString VideoHd(this HtmlHelper helper, string url)
        {
            var builder = new TagBuilder("div");
            builder.MergeAttribute("class", "youtube-player");
            builder.MergeAttribute("data-id", url);

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Video(this HtmlHelper helper, string url)
        {
            var builder = new TagBuilder("iframe");
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("frameborder", "0");
            builder.MergeAttribute("class", "embed-responsive-item");

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DivForVideoHd(this HtmlHelper helper, string url)
        {
            var builder = new TagBuilder("div");
            builder.MergeAttribute("class", "embed-responsive embed-responsive-16by9");
            //builder.MergeAttribute("class", "embed-responsive");
            var inner = new TagBuilder("iframe");
            inner.MergeAttribute("src", url);
            inner.MergeAttribute("frameborder", "0");
            inner.MergeAttribute("class", "embed-responsive-item");
            builder.InnerHtml = inner.ToString();

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DivForVideo(this HtmlHelper helper, string url)
        {
            var builder = new TagBuilder("div");
            builder.MergeAttribute("class", "embed-responsive embed-responsive-4by3");
            var inner = new TagBuilder("iframe");
            inner.MergeAttribute("src", url);
            inner.MergeAttribute("frameborder", "0");
            inner.MergeAttribute("class", "embed-responsive-item");
            builder.InnerHtml = inner.ToString();

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
    }
}