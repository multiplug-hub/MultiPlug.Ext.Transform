using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Transform.Models.Components.Regex
{
    public class RegexProperties : MultiPlugBase
    {
        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public Event TransformedEvent { get; set; }

        [DataMember]
        public Subscription[] Subscriptions { get; set; }

        [DataMember]
        public string Pattern { get; set; }

        [DataMember]
        public ResultKey[] ResultKeys { get; set; }

        [DataMember]
        public bool? ForwardEventSubjects { get; set; }
        [DataMember]
        public bool? IgnoreCase { get; set; }
    }
}
