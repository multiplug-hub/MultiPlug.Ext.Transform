using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Transform.Models.Components.XSLT
{
    public class XSLTProperties : MultiPlugBase
    {
        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public string XSLTPath { get; set; }

        [DataMember]
        public Event Event { get; set; }

        [DataMember]
        public Subscription Subscription { get; set; }

        [DataMember]
        public bool? MemoryLoad { get; set; }

        public string Source { get; set; }

        [DataMember]
        public bool? ForwardEventSubjects { get; set; }
    }
}
