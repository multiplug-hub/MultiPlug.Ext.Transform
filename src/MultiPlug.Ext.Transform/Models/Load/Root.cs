using System.Runtime.Serialization;
using MultiPlug.Ext.Transform.Models.Components.Regex;
using MultiPlug.Ext.Transform.Models.Components.XSLT;
using MultiPlug.Ext.Transform.Models.Components.String;

namespace MultiPlug.Ext.Transform.Models.Load
{
    public class Root
    {
        [DataMember]
        public StringProperties[] StringComponents { get; set; }
        [DataMember]
        public RegexProperties[] RegexComponents { get; set; }
        [DataMember]
        public XSLTProperties[] XSLTComponents { get; set; }
    }
}
