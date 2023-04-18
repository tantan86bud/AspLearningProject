using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AspLearningProject.HtmlHelpers
{
    public static class LinkHelper
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper helper, int image_id, string text)
        {
            return new HtmlString(String.Format("<a href=\"image/{0}\">{1}</a>", image_id, text));
        }
    }
}
