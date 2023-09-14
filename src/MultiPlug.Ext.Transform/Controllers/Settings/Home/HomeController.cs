using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.Home
{
    [Route("")]
    public class HomeController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = new Models.Settings.Home
                {
                    StringsCount = Core.Instance.StringComponents.Length.ToString(),
                    RegularExpressionsCount = Core.Instance.RegexComponents.Length.ToString(),
                    XSLTComponentsCount = Core.Instance.XSLTComponents.Length.ToString()
                },
                Template = "TransformationsGetHomeViewContents"
            };
        }
    }
}
