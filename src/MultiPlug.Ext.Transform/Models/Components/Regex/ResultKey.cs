using MultiPlug.Base;
using System.Runtime.Serialization;

namespace MultiPlug.Ext.Transform.Models.Components.Regex
{
    public class ResultKey : MultiPlugBase
    {
        [DataMember]
        public string KeyValue { get; set; }
        [DataMember]
        public bool Enabled { get; set; }
        [DataMember]
        public int Index { get; set; }
    }
}
