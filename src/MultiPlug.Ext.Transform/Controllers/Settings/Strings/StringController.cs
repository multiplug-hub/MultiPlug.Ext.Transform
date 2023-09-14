using System;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Exchange;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Transform.Models.Components.String;
using MultiPlug.Ext.Transform.Models.Settings.String;

namespace MultiPlug.Ext.Transform.Controllers.Settings.Strings
{
    [Route("string")]
    public class StringController : SettingsApp
    {
        public Response Get(string id)
        {
            StringProperties StringComponent = null;

            if (id != null)
            {
                StringComponent = Core.Instance.StringComponents.FirstOrDefault(t => t.Guid == id);
            }

            if (StringComponent == null)
            {
                string Guid = System.Guid.NewGuid().ToString();
                StringComponent = new StringProperties
                {
                    Guid = Guid,
                    Subscriptions = new Subscription[0],
                    TransformedEvent = new Event
                    {
                        Guid = Guid,
                        Id = Guid,
                        Description = "String Transformation Result",
                        Subjects = new string[] { "StringResult"},
                        Group = "String"
                    },
                    Steps = new Step[0],
                    ForwardEventSubjects = false
                };
            }

            return new Response
            {
                Model = StringComponent,
                Template = "TransformationsStringView"
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

            var Steps = new List<Step>();

            if (theModel.StepAction != null &&
                theModel.StepArg1 != null &&
                theModel.StepArg2 != null &&
                theModel.StepAction.Length == theModel.StepArg1.Length &&
                theModel.StepAction.Length == theModel.StepArg2.Length )
            {
                for (int i = 0; i < theModel.StepAction.Length; i++)
                {
                    Steps.Add(new Step
                    {
                        Action = theModel.StepAction[i],
                        Arg1 = theModel.StepArg1[i],
                        Arg2 = theModel.StepArg2[i]
                    });
                }
            }

            Core.Instance.Update(new StringProperties[]
            {
                new StringProperties
                {
                    Guid = theModel.Guid,
                    TransformedEvent = new Event
                    {
                        Guid = theModel.Guid,
                        Id = theModel.TransformedEventId,
                        Description = theModel.TransformedEventDescription,
                        Subjects = new string[] { theModel.TransformedEventSubject },
                        Group = "String"
                    },
                    Subscriptions = Subscriptions.ToArray(),
                    Steps = Steps.ToArray(),
                    ForwardEventSubjects = theModel.ForwardEventSubjects
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
