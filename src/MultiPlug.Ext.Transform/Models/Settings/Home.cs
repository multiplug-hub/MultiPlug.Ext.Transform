using BritishSystems.MultiPlug.Extension.Version1;
using MultiPlug.Base;

namespace MultiPlug.Ext.Transform.Models.Settings
{
    public class Home : MultiPlugBase
    {
        public string StringsCount { get; set; }
        public string RegularExpressionsCount { get; set; }
        public string XSLTComponentsCount { get; set; }
    }
}
