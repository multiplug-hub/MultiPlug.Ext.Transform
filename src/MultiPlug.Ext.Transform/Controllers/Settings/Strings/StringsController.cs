using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.Strings
{
    [Route("strings")]
    public class StringsController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = Core.Instance.StringComponents,
                Template = "TransformationsStringsView"
            };
        }
    }
}