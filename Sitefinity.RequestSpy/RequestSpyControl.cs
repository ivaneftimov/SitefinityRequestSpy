using System.IO;
using System.Text;
using System.Web.UI;

namespace Sitefinity.RequestSpy
{
    public class RequestSpyControl : Control
    {
        /// <inheritdoc />
        public override void RenderControl(HtmlTextWriter writer)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            var htmlStream = typeof(RequestSpyModule).Assembly.GetManifestResourceStream("Sitefinity.RequestSpy.requestSpy.html");
            using (StreamReader reader = new StreamReader(htmlStream))
            {
                htmlBuilder.Append(reader.ReadToEnd());
            }

            htmlBuilder.Append("<script>requestSpyApp.constant('SERVICE_URL', '" + RequestSpyHttpModule.ServiceUrl + "')</script>");
            string html = htmlBuilder.ToString();
            
            writer.Write(html);
        }
    }
}