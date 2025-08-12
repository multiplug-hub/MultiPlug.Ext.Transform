
namespace MultiPlug.Ext.Transform.Models.Apps.RegExTester
{
    public class Model
    {
        public bool IgnoreCase { get; internal set; }
        public string Pattern { get; internal set; }
        public string TestString { get; internal set; }
        public string[] Matches { get; internal set; }
    }
}
