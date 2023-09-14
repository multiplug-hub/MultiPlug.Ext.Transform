
using System;
using System.Runtime.Serialization;

namespace MultiPlug.Ext.Transform.Models.Components.String
{
    public class Step
    {
        [DataMember]
        public string Action { get; set; }
        /// <summary>
        /// Used to speed Lookup
        /// </summary>
        public Func<string,string,string,string> ActionFunction { get; set; }
        [DataMember]
        public string Arg1 { get; set; }
        [DataMember]
        public string Arg2 { get; set; }
    }
}
