using Microsoft.AspNetCore.Razor.TagHelpers;

namespace JWTMvcClient.TagHelpers
{
  //<text-center text="Merhaba"></text-center>
  // <p style="text-align:center;padding:'5px'">
  // Deneme
  // </p>
  [HtmlTargetElement("text-center", Attributes = "text")]
  public class TextCenterTagHelper : TagHelper
  {

    public string Text { get; set; }


    // HTML çıktı üreteceğiz
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      //<p style="text-align:center">${Content}</p>
      output.TagName = "p";
      output.Attributes.Add("style","text-align:center;padding:5px");
      output.Content.SetContent(Text);
      base.Process(context, output);
    }

  }
}
