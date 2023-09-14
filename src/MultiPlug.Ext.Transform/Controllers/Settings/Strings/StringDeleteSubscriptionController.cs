using System.Linq;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.Strings
{
    public class StringDeleteSubscriptionController : SettingsApp
    {
        public Response Post(string id, string subid)
        {
            if (id != null)
            {
                var StringComponent = Core.Instance.StringComponents.FirstOrDefault(t => t.Guid == id);

                if (StringComponent != null)
                {
                    StringComponent.RemoveSubscription(subid);
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
