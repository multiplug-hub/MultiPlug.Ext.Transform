using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.RegularExpressions
{
    [Route("regex/delete")]
    public class RegularExpressionDeleteSubscriptionController : SettingsApp
    {
        public Response Post(string id, string subid)
        {
            if (id != null)
            {
                var RegexComponent = Core.Instance.RegexComponents.FirstOrDefault(t => t.Guid == id);

                if (RegexComponent != null)
                {
                    RegexComponent.RemoveSubscription(subid);
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
