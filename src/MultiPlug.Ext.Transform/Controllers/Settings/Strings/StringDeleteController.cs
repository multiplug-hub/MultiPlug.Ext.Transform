using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.Strings
{
    [Route("strings/delete")]
    public class StringDeleteController : SettingsApp
    {
        public Response Post(string id)
        {
            if (id != null)
            {
                var StringComponent = Core.Instance.StringComponents.FirstOrDefault(t => t.Guid == id);

                if (StringComponent != null)
                {
                    Core.Instance.Remove(new[] { StringComponent });
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
