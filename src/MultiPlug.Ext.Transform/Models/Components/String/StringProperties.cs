using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Transform.Models.Components.String
{
    public class StringProperties : MultiPlugBase
    {
        [DataMember]
        public string Guid { get; set; }

        [DataMember]
        public Event TransformedEvent { get; set; }

        [DataMember]
        public Subscription[] Subscriptions { get; set; }

        [DataMember]
        public Step[] Steps { get; set; }

        [DataMember]
        public bool? ForwardEventSubjects { get; set; }
    }
}
