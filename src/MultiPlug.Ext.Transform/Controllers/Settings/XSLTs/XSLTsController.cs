using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Settings.XSLTs
{
    [Route("xslts")]
    public class XSLTsController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = Core.Instance.XSLTComponents,
                Template = "TransformationsXSLTsView"
            };
        }
    }
}
