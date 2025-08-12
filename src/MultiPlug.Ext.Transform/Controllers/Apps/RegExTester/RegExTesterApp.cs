using MultiPlug.Base.Http;
using MultiPlug.Extension.Core.Attribute;

namespace MultiPlug.Ext.Transform.Controllers.Apps.RegExTester
{
    [HttpEndpointType(HttpEndpointType.App)]
    [ViewAs(ViewAs.Partial)]
    [Name("RegEx Tester")]
    public class RegExTesterApp : Controller
    {
    }
}
