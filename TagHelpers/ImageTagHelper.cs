using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.IO;
using System.Text;

namespace lab1.taghelpers;

[HtmlTargetElement("img", Attributes = "image-bytes")]
public class ImageTagHelper : TagHelper
{
    [HtmlAttributeName("image-bytes")]
    public byte[] ImageBytes { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ImageBytes != null && ImageBytes.Length > 0)
        {
            // Chuyển đổi byte[] thành chuỗi Base64
            var base64Image = Convert.ToBase64String(ImageBytes);
            var imgSrc = $"data:image/png;base64,{base64Image}";
            output.Attributes.SetAttribute("src", imgSrc);
        }
    }
}