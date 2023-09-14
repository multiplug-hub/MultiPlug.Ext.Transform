using System.Drawing;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Assets
{
    [Route("images/*")]
    public class ImageController : AssetsEndpoint
    {
        public Response Get(string theName)
        {
            ImageConverter converter = new ImageConverter();
            return new Response { RawBytes = (byte[])converter.ConvertTo(Properties.Resources.transform, typeof(byte[])), MediaType = "image/png" };
        }
    }
}
