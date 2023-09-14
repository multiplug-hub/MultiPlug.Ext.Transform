using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext.Transform.Controllers.Settings
{
    [Name("Transformations")]
    [ViewAs(ViewAs.Partial)]
    [HttpEndpointType(HttpEndpointType.Settings)]
    public class SettingsApp : Controller
    {
    }
}
