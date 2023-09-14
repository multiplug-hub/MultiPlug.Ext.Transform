
namespace MultiPlug.Ext.Transform.Models.Settings.XSLT
{
    public class PostModel
    {
        public string Guid { get; set; }
        public string EventId { get; set; }
        public string EventDescription { get; set; }
        public string EventSubject { get; set; }
        public bool MemoryLoad { get; set; }
        public string Source { get; set; }
        public string SubscriptionGuid { get; set; }
        public string SubscriptionId { get; set; }
        public bool ForwardEventSubjects { get; set; }
    }
}
