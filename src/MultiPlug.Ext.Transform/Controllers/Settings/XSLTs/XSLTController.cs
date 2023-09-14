using System;
using System.IO;
using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Exchange;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Transform.Components.XSLT;
using MultiPlug.Ext.Transform.Controllers.Settings;
using MultiPlug.Ext.Transform.Models.Components.XSLT;
using MultiPlug.Ext.Transform.Models.Settings.XSLT;
using MultiPlug.Ext.Transform.Properties;

namespace MultiPlug.Ext.Transform.Views.Controllers.XSLTs
{
    [Route("xslt")]
    public class XSLTController : SettingsApp
    {
        public Response Get(string id)
        {
            XSLTComponent XSLTComponent = null;

            if (id != null)
            {
                XSLTComponent = Core.Instance.XSLTComponents.FirstOrDefault(t => t.Guid == id);
            }

            XSLTProperties model;

            if (XSLTComponent != null)
            {
                model = XSLTComponent;

                if (!model.MemoryLoad.Value)
                {
                    model.Source = File.ReadAllText(model.XSLTPath);
                }
            }
            else
            {
                var guid = Guid.NewGuid().ToString();
                model = new XSLTProperties
                {
                    Guid = guid,
                    Source = Resources.DefaultXSLT,
                    MemoryLoad = false,
                    Subscription = new Subscription(guid, string.Empty),
                    Event = new Event
                    {
                        Guid = guid,
                        Id = guid,
                        Description = "XSLT Result",
                        Subjects = new string[] { "XSLTResult" },
                        Group = "XSLT"
                    },
                    XSLTPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transformations\\" + guid + "\\" + guid + ".xslt"),
                    ForwardEventSubjects = false
                };
            }

            return new Response
            {
                Model = model,
                Template = "TransformationsXsltView"
            };
        }

        public Response Post(PostModel theModel)
        {
            Core.Instance.Update(new XSLTProperties[]
            {
                new XSLTProperties
                {
                    Guid = theModel.Guid,
                    MemoryLoad = theModel.MemoryLoad,
                    Source = theModel.Source,
                    Subscription = new Subscription(theModel.Guid, theModel.SubscriptionId),
                    Event = new Event(theModel.Guid, theModel.EventId, theModel.EventDescription, "XSLT", new string[] { theModel.EventSubject } ),
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
