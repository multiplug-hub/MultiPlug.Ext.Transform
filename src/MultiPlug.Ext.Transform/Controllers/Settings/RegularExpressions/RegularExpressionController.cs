using System;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Transform.Components.Regex;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Transform.Models.Components.Regex;
using MultiPlug.Ext.Transform.Models.Settings.RegularExpressions;

namespace MultiPlug.Ext.Transform.Controllers.Settings.RegularExpressions
{
    [Route("regex")]
    public class RegularExpressionController : SettingsApp
    {
        public Response Get(string id)
        {
            RegexComponent RegexComponent = null;

            if (id != null)
            {
                RegexComponent = Core.Instance.RegexComponents.FirstOrDefault(t => t.Guid == id);
            }

            RegexProperties model;

            if (RegexComponent != null)
            {
                model = RegexComponent;
            }
            else
            {
                var guid = Guid.NewGuid().ToString();
                model = new RegexProperties
                {
                    Guid = guid,
                    Pattern = "",
                    Subscriptions = new Subscription[0],
                    TransformedEvent = new Event { Guid = guid, Id = guid, Description = "Regex Result" },
                    ResultKeys = new ResultKey[0],
                    ForwardEventSubjects = false,
                    IgnoreCase = true
                };
            }

            return new Response
            {
                Model = model,
                Template = "TransformationsRegularExpressionView"
            };
        }

        public Response Post(PostModel theModel)
        {
            var Subscriptions = new List<Subscription>();

            if (theModel.SubscriptionGuid != null &&
                theModel.SubscriptionId != null &&
                theModel.SubscriptionGuid.Length == theModel.SubscriptionId.Length)
            {
                for (int i = 0; i < theModel.SubscriptionGuid.Length; i++)
                {
                    if (string.IsNullOrEmpty(theModel.SubscriptionId[i]))
                    {
                        continue;
                    }

                    Subscriptions.Add(new Subscription
                    {
                        Guid = theModel.SubscriptionGuid[i],
                        Id = theModel.SubscriptionId[i]
                    });
                }
            }

            bool[] ResultKeyEnabled = null;

            // Checkbox Handling
            if (theModel.ResultKeyEnabled != null)
            {
                ResultKeyEnabled = new bool[theModel.ResultKeyEnabled.Length];

                int index = 0;

                for (int i = 0; i < theModel.ResultKeyEnabled.Length; i++)
                {
                    ResultKeyEnabled[index] = theModel.ResultKeyEnabled[i];
                    index++;

                    if (theModel.ResultKeyEnabled[i])
                    {
                        i++;
                    }
                }
            }

            ResultKey[] ResultKey;

            if (theModel.ResultKeyIndex != null)
            {
                ResultKey = new ResultKey[theModel.ResultKeyIndex.Length];

                for(int i = 0; i < theModel.ResultKeyIndex.Length; i++)
                {
                    ResultKey[i] = new ResultKey
                    {
                        Index = theModel.ResultKeyIndex[i],
                        KeyValue = theModel.ResultKeyKeyValue[i],
                        Enabled = ResultKeyEnabled[i]
                    };
                }
            }
            else
            {
                ResultKey = new ResultKey[0];
            }


            Core.Instance.Update(new RegexProperties[]
            {
                new RegexProperties
                {
                    Guid = theModel.Guid,
                    Pattern = theModel.Pattern,
                    TransformedEvent = new Event
                    {
                        Guid = theModel.Guid,
                        Id = theModel.TransformedEventId,
                        Description = theModel.TransformedEventDescription,
                        Group = "RegEx"
                    },
                    ResultKeys = ResultKey,
                    Subscriptions = Subscriptions.ToArray(),
                    ForwardEventSubjects = theModel.ForwardEventSubjects,
                    IgnoreCase = theModel.IgnoreCase
                }
            });

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = new Uri(Context.Referrer, "?id=" + theModel.Guid)
            };
        }
    }
}
