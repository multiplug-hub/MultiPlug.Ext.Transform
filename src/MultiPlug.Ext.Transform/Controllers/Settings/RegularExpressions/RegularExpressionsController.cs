using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.RegularExpressions
{
    [Route("regexs")]
    public class RegularExpressionsController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = Core.Instance.RegexComponents,
                Template = "TransformationsRegularExpressionsView"
            };
        }
    }
}
