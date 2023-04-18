using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspLearningProject.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "image-id")]
    public class ATagHelper : TagHelper
    {
        [HtmlAttributeName("image-id")]
        public int Id { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           output.Attributes.Add("href", $"image/{Id}");
        }
    }
}