
namespace MultiPlug.Ext.Transform.Models.Settings.RegularExpressions
{
    public class PostModel
    {
        public string Guid { get; set; }
        public string Pattern { get; set; }
        public string TransformedEventId { get; set; }
        public string TransformedEventDescription { get; set; }
        public string[] SubscriptionGuid { get; set; }
        public string[] SubscriptionId { get; set; }
        public int[] ResultKeyIndex { get; set; }
        public string[] ResultKeyKeyValue { get; set; }
        public bool[] ResultKeyEnabled { get; set; }
        public bool ForwardEventSubjects { get; set; }
        public bool IgnoreCase { get; set; }
    }
}
