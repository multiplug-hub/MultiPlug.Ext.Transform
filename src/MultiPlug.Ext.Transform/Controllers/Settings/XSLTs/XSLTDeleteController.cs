using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.XSLTs
{
    [Route("xslts/delete")]
    public class XSLTDeleteController : SettingsApp
    {
        public Response Post(string id)
        {
            if (id != null)
            {
                var XsltComponent = Core.Instance.XSLTComponents.FirstOrDefault(t => t.Guid == id);

                if (XsltComponent != null)
                {
                    Core.Instance.Remove(new[] { XsltComponent });
                }
            }

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Redirect,
                Location = Context.Referrer
            };
        }
    }
}
