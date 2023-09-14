using System;
using System.Linq;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Transform.Models.Components.String;

namespace MultiPlug.Ext.Transform.Components.String
{
    public class StringComponent : StringProperties
    {
        public event Action EventsUpdated;
        public event Action SubscriptionsUpdated;

        public StringComponent( string theGuid )
        {
            Guid = theGuid;
            TransformedEvent = new Event
            {
                Guid = theGuid,
                Id = theGuid,
                Description = "String Transformation Result",
                Group = "String",
                Subjects = new string[] { "StringResult"}
            };
            Subscriptions = new Subscription[0];
            Steps = new Step[0];
            ForwardEventSubjects = false;
        }

        internal void UpdateProperties(StringProperties theNewProperties)
        {
            bool EvUpdated = false;
            bool SubsUpdated = false;

            if (theNewProperties.TransformedEvent != null)
            {
                if (Event.Merge(TransformedEvent, theNewProperties.TransformedEvent))
                {
                    EvUpdated = true;
                }
            }

            if (theNewProperties.Subscriptions != null)
            {
                var list = Subscriptions.ToList();

                foreach (var UpdatedSubscription in theNewProperties.Subscriptions)
                {
                    if (string.IsNullOrEmpty(UpdatedSubscription.Guid))
                    {
                        if (!string.IsNullOrEmpty(UpdatedSubscription.Id))
                        {
                            UpdatedSubscription.Guid = System.Guid.NewGuid().ToString();
                            UpdatedSubscription.Event += OnEvent;
                            list.Add(UpdatedSubscription);
                            SubsUpdated = true;
                        }
                    }
                    else
                    {
                        var ExistingSubSearch = list.FirstOrDefault(s => s.Guid == UpdatedSubscription.Guid);

                        if (ExistingSubSearch == null)
                        {
                            if (!string.IsNullOrEmpty(UpdatedSubscription.Id))
                            {
                                UpdatedSubscription.Event += OnEvent;
                                list.Add(UpdatedSubscription);
                                SubsUpdated = true;
                            }
                        }
                        else
                        {
                            if (Subscription.Merge(ExistingSubSearch, UpdatedSubscription)) { SubsUpdated = true; }
                        }
                    }
                }

                Subscriptions = list.ToArray();
            }

            if (theNewProperties.Steps != null)
            {
                foreach (var item in theNewProperties.Steps)
                {
                    switch (item.Action)
                    {
                        case Actions.ToUpper:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.ToUpper);
                            break;
                        case Actions.ToLower:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.ToLower);
                            break;
                        case Actions.Insert:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.Insert);
                            break;
                        case Actions.Trim:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.Trim);
                            break;
                        case Actions.TrimValue:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.TrimValue);
                            break;
                        case Actions.TrimEnd:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.TrimEnd);
                            break;
                        case Actions.TrimStart:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.TrimStart);
                            break;
                        case Actions.PadLeft:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.PadLeft);
                            break;
                        case Actions.PadLeftValue:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.PadLeftValue);
                            break;
                        case Actions.PadRight:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.PadRight);
                            break;
                        case Actions.PadRightValue:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.PadRightValue);
                            break;
                        case Actions.Remove:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.Remove);
                            break;
                        case Actions.RemoveLength:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.RemoveLength);
                            break;
                        case Actions.Replace:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.Replace);
                            break;
                        case Actions.SubString:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.SubString);
                            break;
                        case Actions.SubStringLength:
                            item.ActionFunction = new Func<string, string, string, string>(StringFunctions.SubStringLength);
                            break;
                    }
                }

                Steps = theNewProperties.Steps;
            }

            if (theNewProperties.ForwardEventSubjects != null)
            {
                ForwardEventSubjects = theNewProperties.ForwardEventSubjects;
            }

            if (EvUpdated)
            {
                EventsUpdated?.Invoke();
            }

            if (SubsUpdated)
            {
                SubscriptionsUpdated?.Invoke();
            }
        }

        internal void RemoveSubscription(string theSubscriptionGuid)
        {
            var SubSearch = Subscriptions.FirstOrDefault(sub => sub.Guid == theSubscriptionGuid);

            if (SubSearch != null)
            {
                var SubsList = Subscriptions.ToList();
                SubsList.Remove(SubSearch);
                Subscriptions = SubsList.ToArray();
                SubscriptionsUpdated?.Invoke();
            }
        }

        private void OnEvent(SubscriptionEvent theSubscriptionEvent)
        {
            foreach (var item in theSubscriptionEvent.PayloadSubjects)
            {
                string Result = item.Value;

                foreach(var Step in Steps)
                {
                    try
                    {
                        Result = Step.ActionFunction(Result, Step.Arg1, Step.Arg2);
                    }
                    catch
                    {
                        // TODO Add Logging
                        return;
                    }
                }

                var PayloadSubjects = new PayloadSubject[] { new PayloadSubject(TransformedEvent.Subjects[0], Result) };

                if (ForwardEventSubjects.Value)
                {
                    PayloadSubjects = PayloadSubjects.Concat(theSubscriptionEvent.Payload.Subjects).ToArray();
                }

                TransformedEvent.Invoke(new Payload(TransformedEvent.Id, PayloadSubjects));
            }
        }
    }
}
