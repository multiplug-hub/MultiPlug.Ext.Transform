
namespace MultiPlug.Ext.Transform.Models.Settings.String
{
    public class PostModel
    {
        public string Guid { get; set; }
        public string TransformedEventId { get; set; }
        public string TransformedEventDescription { get; set; }
        public string TransformedEventSubject { get; set; }
        public string[] SubscriptionGuid { get; set; }
        public string[] SubscriptionId { get; set; }

        public string[] StepAction { get; set; }
        public string[] StepArg1 { get; set; }
        public string[] StepArg2 { get; set; }
        public bool ForwardEventSubjects { get; set; }
    }
}
