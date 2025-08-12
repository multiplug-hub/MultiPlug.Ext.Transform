using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotNetRegex = System.Text.RegularExpressions.Regex;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Transform.Models.Components.Regex;

namespace MultiPlug.Ext.Transform.Components.Regex
{
    public class RegexComponent : RegexProperties
    {
        public event Action EventsUpdated;
        public event Action SubscriptionsUpdated;

        public RegexComponent( string theGuid )
        {

            Guid = theGuid;
            TransformedEvent = new Event
            {
                Guid = theGuid,
                Id = theGuid,
                Description = "",
                Group = "RegEx"
            };

            Pattern = string.Empty;
            Subscriptions = new Subscription[0];
            ResultKeys = new ResultKey[0];
            ForwardEventSubjects = false;
            IgnoreCase = true;
            MatchesEqualResultKeys = true;
        }

        internal void UpdateProperties(RegexProperties theNewProperties)
        {
            bool EvUpdated = false;
            bool SubsUpdated = false;

            if(theNewProperties.Pattern != null && theNewProperties.Pattern != Pattern )
            {
                Pattern = theNewProperties.Pattern;
            }

            if(theNewProperties.TransformedEvent != null)
            {
                if( Event.Merge(TransformedEvent, theNewProperties.TransformedEvent, false ) )
                {
                    EvUpdated = true;
                }
            }

            if(theNewProperties.ResultKeys != null)
            {
                ResultKeys = theNewProperties.ResultKeys.OrderBy(r => r.Index).ToArray();

                var EnabledResultKey = ResultKeys.Where(ResultKey => ResultKey.Enabled).Select(ResultKey => ResultKey.KeyValue).ToArray();

                if( ! EnabledResultKey.SequenceEqual(TransformedEvent.Subjects))
                {
                    TransformedEvent.Subjects = EnabledResultKey;
                    EvUpdated = true;
                }
            }

            if(theNewProperties.Subscriptions != null)
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

            if (theNewProperties.ForwardEventSubjects != null)
            {
                ForwardEventSubjects = theNewProperties.ForwardEventSubjects;
            }

            if(theNewProperties.IgnoreCase != null)
            {
                IgnoreCase = theNewProperties.IgnoreCase;
            }

            if(theNewProperties.MatchesEqualResultKeys != null)
            {
                MatchesEqualResultKeys = theNewProperties.MatchesEqualResultKeys;
            }

            if (EvUpdated )
            {
                EventsUpdated?.Invoke();
            }

            if ( SubsUpdated )
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
            foreach( var item in theSubscriptionEvent.PayloadSubjects)
            {
                DotNetRegex DotNetRegex; 
                    
                if(IgnoreCase.Value)
                {
                    DotNetRegex = new DotNetRegex(Pattern, RegexOptions.IgnoreCase);
                }   
                else
                {
                    DotNetRegex = new DotNetRegex(Pattern);
                }
                    
                MatchCollection matches = DotNetRegex.Matches(item.Value);

                var PayloadSubjects = new List<PayloadSubject>();

                if(MatchesEqualResultKeys.Value)
                {
                    if(matches.Count != ResultKeys.Length)
                    {
                        return;
                    }
                }

                for( int i = 0; i < ResultKeys.Length; i++)
                {
                    if( ! ResultKeys[i].Enabled )
                    {
                        continue;
                    }

                    string Value;

                    if( i < matches.Count)
                    {
                        Value = matches[i].ToString();
                    }
                    else
                    {
                        Value = string.Empty;
                    }

                    PayloadSubjects.Add(new PayloadSubject(ResultKeys[i].KeyValue, Value));
                }

                if (ForwardEventSubjects.Value)
                {
                    PayloadSubjects.AddRange( theSubscriptionEvent.Payload.Subjects);
                }

                TransformedEvent.Invoke(new Payload(TransformedEvent.Id, PayloadSubjects.ToArray() ));
            }
        }
    }
}
