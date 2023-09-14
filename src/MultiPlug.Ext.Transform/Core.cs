using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Transform.Components.String;
using MultiPlug.Ext.Transform.Components.Regex;
using MultiPlug.Ext.Transform.Components.XSLT;
using MultiPlug.Ext.Transform.Models.Components.Regex;
using MultiPlug.Ext.Transform.Models.Components.XSLT;
using MultiPlug.Ext.Transform.Models.Components.String;

namespace MultiPlug.Ext.Transform
{
    public class Core : MultiPlugBase
    {
        private static Core m_Instance = null;

        internal event Action EventsUpdated;

        internal event Action SubscriptionsUpdated;

        internal Subscription[] Subscriptions { get; private set; } = new Subscription[0];
        internal Event[] Events { get; private set; } = new Event[0];

        [DataMember]
        public StringComponent[] StringComponents { get; set; } = new StringComponent[0];

        [DataMember]
        public RegexComponent[] RegexComponents { get; set; } = new RegexComponent[0];

        [DataMember]
        public XSLTComponent[] XSLTComponents { get; set; } = new XSLTComponent[0];

        public static Core Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Core();
                }
                return m_Instance;
            }
        }

        internal void Update(RegexProperties[] regexModel)
        {
            List<RegexComponent> NewRegex = new List<RegexComponent>();

            foreach (var item in regexModel)
            {
                RegexComponent RegexComponent = RegexComponents.FirstOrDefault(f => f.Guid == item.Guid);

                if (RegexComponent != null)
                {
                    RegexComponent.UpdateProperties(item);
                }
                else
                {
                    if (string.IsNullOrEmpty(item.Guid))
                    {
                        item.Guid = Guid.NewGuid().ToString();
                    }

                    RegexComponent = new RegexComponent(item.Guid);
                    RegexComponent.UpdateProperties(item);
                    RegexComponent.SubscriptionsUpdated += OnSubscriptionsUpdated;
                    RegexComponent.EventsUpdated += OnEventsUpdated;
                    NewRegex.Add(RegexComponent);
                }
            }

            if (NewRegex.Any())
            {
                var RegexComponentsList = RegexComponents.ToList();
                RegexComponentsList.AddRange(NewRegex);
                RegexComponents = RegexComponentsList.ToArray();
                OnSubscriptionsUpdated();
                OnEventsUpdated();
            }
        }

        internal void Update(XSLTProperties[] XsltModel)
        {
            List<XSLTComponent> NewXSLTs = new List<XSLTComponent>();

            foreach (var item in XsltModel)
            {
                XSLTComponent XSLTComponent = XSLTComponents.FirstOrDefault(f => f.Guid == item.Guid);

                if (XSLTComponent != null)
                {
                    XSLTComponent.UpdateProperties(item);
                }
                else
                {
                    if( string.IsNullOrEmpty( item.Guid))
                    {
                        item.Guid = Guid.NewGuid().ToString();
                    }

                    XSLTComponent = new XSLTComponent(item.Guid);
                    XSLTComponent.UpdateProperties(item);
                    XSLTComponent.SubscriptionsUpdated += OnSubscriptionsUpdated;
                    XSLTComponent.EventsUpdated += OnEventsUpdated;
                    NewXSLTs.Add(XSLTComponent);
                }
            }

            if(NewXSLTs.Any())
            {
                var XSLTComponentsList = XSLTComponents.ToList();
                XSLTComponentsList.AddRange(NewXSLTs);
                XSLTComponents = XSLTComponentsList.ToArray();
                OnSubscriptionsUpdated();
                OnEventsUpdated();
            }

        }

        internal void Update(StringProperties[] StringModel)
        {
            List<StringComponent> NewStrings = new List<StringComponent>();

            foreach (var item in StringModel)
            {
                StringComponent StringComponent = StringComponents.FirstOrDefault(f => f.Guid == item.Guid);

                if (StringComponent != null)
                {
                    StringComponent.UpdateProperties(item);
                }
                else
                {
                    if (string.IsNullOrEmpty(item.Guid))
                    {
                        item.Guid = Guid.NewGuid().ToString();
                    }

                    StringComponent = new StringComponent(item.Guid);
                    StringComponent.UpdateProperties(item);
                    StringComponent.SubscriptionsUpdated += OnSubscriptionsUpdated;
                    StringComponent.EventsUpdated += OnEventsUpdated;
                    NewStrings.Add(StringComponent);
                }
            }

            if (NewStrings.Any())
            {
                var StringComponentsList = StringComponents.ToList();
                StringComponentsList.AddRange(NewStrings);
                StringComponents = StringComponentsList.ToArray();
                OnSubscriptionsUpdated();
                OnEventsUpdated();
            }
        }

        internal void Remove(StringComponent[] theStringComponents)
        {
            foreach (var StringComponent in theStringComponents)
            {
                StringComponent.SubscriptionsUpdated -= OnSubscriptionsUpdated;
                StringComponent.EventsUpdated -= OnEventsUpdated;

                var StringComponentsList = StringComponents.ToList();
                StringComponentsList.Remove(StringComponent);
                StringComponents = StringComponentsList.ToArray();
            }

            OnSubscriptionsUpdated();
            OnEventsUpdated();
        }

        internal void Remove(RegexComponent[] theRegexComponents)
        {
            foreach (var RegexComponent in theRegexComponents)
            {
                RegexComponent.SubscriptionsUpdated -= OnSubscriptionsUpdated;
                RegexComponent.EventsUpdated -= OnEventsUpdated;

                var RegexComponentsList = RegexComponents.ToList();
                RegexComponentsList.Remove(RegexComponent);
                RegexComponents = RegexComponentsList.ToArray();
            }

            OnSubscriptionsUpdated();
            OnEventsUpdated();
        }

        internal void Remove(XSLTComponent[] theXSLTComponents)
        {
            foreach (var XSLTComponent in theXSLTComponents)
            {
                XSLTComponent.SubscriptionsUpdated -= OnSubscriptionsUpdated;
                XSLTComponent.EventsUpdated -= OnEventsUpdated;

                var XSLTComponentsList = XSLTComponents.ToList();
                XSLTComponentsList.Remove(XSLTComponent);
                XSLTComponents = XSLTComponentsList.ToArray();
            }

            OnSubscriptionsUpdated();
            OnEventsUpdated();
        }

        private void OnEventsUpdated()
        {
            List<Event> EventsList = new List<Event>();
            Array.ForEach(StringComponents, c => EventsList.Add(c.TransformedEvent));
            Array.ForEach(RegexComponents, c => EventsList.Add(c.TransformedEvent));
            Array.ForEach(XSLTComponents, c => EventsList.Add(c.Event));
            Events = EventsList.ToArray();
            EventsUpdated?.Invoke();
        }

        private void OnSubscriptionsUpdated()
        {
            List<Subscription> SubscriptionList = new List<Subscription>();
            Array.ForEach(StringComponents, c => SubscriptionList.AddRange(c.Subscriptions));
            Array.ForEach(RegexComponents, c => SubscriptionList.AddRange(c.Subscriptions));
            Array.ForEach(XSLTComponents, c => SubscriptionList.Add(c.Subscription));
            Subscriptions = SubscriptionList.ToArray();
            SubscriptionsUpdated?.Invoke();
        }
    }
}
