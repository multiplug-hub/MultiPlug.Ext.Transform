using System;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Transform.Models.Components.XSLT;
using MultiPlug.Ext.Transform.Properties;

namespace MultiPlug.Ext.Transform.Components.XSLT
{
    public class XSLTComponent : XSLTProperties
    {
        public event Action EventsUpdated;
        public event Action SubscriptionsUpdated;

        public XSLTComponent(string theGuid)
        {
            Guid = theGuid;
            Source = string.Empty;
            MemoryLoad = false;
            ForwardEventSubjects = false;
            Subscription = new Subscription(theGuid, string.Empty);
            Subscription.Event += OnEvent;
            Event = new Event
            {
                Guid = theGuid,
                Id = theGuid,
                Description = "XSLT Result",
                Subjects = new string[] { "XSLTResult" },
                Group = "XSLT"
            };
            XSLTPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transformations\\" + theGuid + "\\" + theGuid + ".xslt");

            string dir = Path.GetDirectoryName(XSLTPath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(XSLTPath, Resources.DefaultXSLT);
        }

        internal void UpdateProperties(XSLTProperties theNewProperties)
        {
            bool EvUpdated = false;
            bool SubsUpdated = false;

            if( theNewProperties.MemoryLoad != null && MemoryLoad != theNewProperties.MemoryLoad)
            {
                MemoryLoad = theNewProperties.MemoryLoad;

                if(! MemoryLoad.Value)
                {
                    Source = string.Empty;
                }
            }

            if(theNewProperties.XSLTPath != null && XSLTPath != theNewProperties.XSLTPath)
            {
                XSLTPath = theNewProperties.XSLTPath;

                string dir = Path.GetDirectoryName(XSLTPath);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }

            if (XSLTPath != "" && theNewProperties.Source != null)
            {
                File.WriteAllText(XSLTPath, theNewProperties.Source);
            }

            if (MemoryLoad.Value)
            {
                if (string.IsNullOrEmpty(theNewProperties.Source))
                {
                    try
                    {
                        Source = File.ReadAllText(XSLTPath);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                }
                else
                {
                    Source = theNewProperties.Source;
                }
            }

            if (theNewProperties.Subscription != null)
            {
                if (Subscription.Merge(Subscription, theNewProperties.Subscription)) { SubsUpdated = true; }
            }

            if(theNewProperties.Event != null)
            {
                if (Event.Merge(Event, theNewProperties.Event)) { EvUpdated = true; }
            }

            if(theNewProperties.ForwardEventSubjects != null)
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

        private void OnEvent(SubscriptionEvent theSubscriptionEvent )
        {
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.AppendChild(xmldoc.CreateProcessingInstruction("xml", "version='1.0'"));
            var RootElement = xmldoc.CreateElement("root");
            xmldoc.AppendChild(RootElement);
            XmlElement IndexElement = xmldoc.CreateElement("index");
            XmlElement ArrayElement = xmldoc.CreateElement("array");
            XmlElement SubjectsElement = xmldoc.CreateElement("subjects");

            for (int i = 0; i  < theSubscriptionEvent.PayloadSubjects.Length; i++)
            {
                XmlElement Element = xmldoc.CreateElement("value");
                Element.InnerText = theSubscriptionEvent.PayloadSubjects[i].Value;
                ArrayElement.AppendChild(Element);

                Element = xmldoc.CreateElement("item" + i.ToString());
                Element.InnerText = theSubscriptionEvent.PayloadSubjects[i].Value;
                IndexElement.AppendChild(Element);

                Element = xmldoc.CreateElement(theSubscriptionEvent.PayloadSubjects[i].Subject);
                Element.InnerText = theSubscriptionEvent.PayloadSubjects[i].Value;
                SubjectsElement.AppendChild(Element);
            }

            RootElement.AppendChild(ArrayElement);
            RootElement.AppendChild(IndexElement);
            RootElement.AppendChild(SubjectsElement);

            XslCompiledTransform xslt = new XslCompiledTransform();

            if (MemoryLoad.Value)
            {
                if(Source == string.Empty)
                {
                    return;
                }

                using (StringReader sr = new StringReader(Source))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        try
                        {
                            xslt.Load(xr);
                        }
                        catch(ArgumentNullException)
                        {
                            return;
                        }
                        catch( XsltException )
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    xslt.Load(XSLTPath);
                }
                catch(XsltException)
                {
                    return;
                }
                catch( FileNotFoundException)
                {
                    return;
                }
            }

            PayloadSubject[] PayloadSubjects;

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xslt.Transform(xmldoc, null, xmlTextWriter);
                }

                PayloadSubjects = new PayloadSubject[] { new PayloadSubject(Event.Subjects[0], stringWriter.ToString() ) };

                if(ForwardEventSubjects.Value)
                {
                    PayloadSubjects = PayloadSubjects.Concat(theSubscriptionEvent.Payload.Subjects).ToArray();
                }
            }

            Event.Invoke(new Payload(Event.Id, PayloadSubjects));
        }
    }
}
