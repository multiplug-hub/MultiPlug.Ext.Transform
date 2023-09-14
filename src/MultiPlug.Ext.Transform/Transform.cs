using System.Collections.Generic;

using MultiPlug.Base.Exchange;
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;

using MultiPlug.Ext.Transform.Models.Load;
using MultiPlug.Ext.Transform.Properties;

namespace MultiPlug.Ext.Transform
{
    public class Transform : MultiPlugExtension
    {
        private List<Root> m_LoadQ = new List<Root>();

        public Transform()
        {
            Core.Instance.EventsUpdated += () => { MultiPlugActions.Extension.Updates.Events(); };
            Core.Instance.SubscriptionsUpdated += () => { MultiPlugActions.Extension.Updates.Subscriptions(); };
        }

        public override Event[] Events
        {
            get
            {
                return Core.Instance.Events;
            }
        }

        public override Subscription[] Subscriptions
        {
            get
            {
                return Core.Instance.Subscriptions;
            }
        }

        public override RazorTemplate[] RazorTemplates
        {
            get
            {
                return new RazorTemplate[]
                {
                    new RazorTemplate("TransformationsNavigation", Resources.Navigation),
                    new RazorTemplate("TransformationsGetHomeViewContents", Resources.Home),
                    new RazorTemplate("TransformationsStringView", Resources.String),
                    new RazorTemplate("TransformationsStringsView", Resources.Strings),
                    new RazorTemplate("TransformationsRegularExpressionView", Resources.RegularExpression),
                    new RazorTemplate("TransformationsRegularExpressionsView", Resources.RegularExpressions),
                    new RazorTemplate("TransformationsXsltView", Resources.XSLT),
                    new RazorTemplate("TransformationsXSLTsView", Resources.XSLTs),
                    new RazorTemplate("TransformationsAbout", Resources.About)
                };
            }
        }

        public override void Initialise()
        {
            Root[] Load = m_LoadQ.ToArray();
            m_LoadQ.Clear();

            foreach (Root LoadModel in Load)
            {
                if(LoadModel.StringComponents != null)
                {
                    Core.Instance.Update(LoadModel.StringComponents);
                }

                if (LoadModel.RegexComponents != null)
                {
                    Core.Instance.Update(LoadModel.RegexComponents);
                }

                if (LoadModel.XSLTComponents != null)
                {
                    Core.Instance.Update(LoadModel.XSLTComponents);
                }
            }
        }



        public void Load(Root theModel)
        {
            m_LoadQ.Add(theModel);
        }

        public override object Save()
        {
            return Core.Instance;
        }
    }
}
