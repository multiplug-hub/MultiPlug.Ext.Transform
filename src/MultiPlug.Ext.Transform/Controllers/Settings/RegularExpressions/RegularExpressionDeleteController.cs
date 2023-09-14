using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.RegularExpressions
{
    [Route("regexs/delete")]
    public class RegularExpressionDeleteController : SettingsApp
    {
        public Response Post(string id)
        {
            if (id != null)
            {
                var REx = Core.Instance.RegexComponents.FirstOrDefault(t => t.Guid == id);

                if(REx != null)
                {
                    Core.Instance.Remove(new[] { REx });
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
