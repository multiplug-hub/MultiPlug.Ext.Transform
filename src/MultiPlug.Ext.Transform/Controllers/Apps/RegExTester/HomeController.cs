using System.Text.RegularExpressions;
using DotNetRegex = System.Text.RegularExpressions.Regex;
using MultiPlug.Base.Exchange;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Transform.Controllers.Apps.RegExTester
{
    public class HomeController : RegExTesterApp
    {
        public Response Get(string p, bool ic)
        {
            return new Response
            {
                Subscriptions = new Subscription[0],
                Model = new Models.Apps.RegExTester.Model
                {
                    Pattern = string.IsNullOrEmpty(p) ? string.Empty : p,
                    IgnoreCase = ic,
                    TestString = string.Empty,
                    Matches = new string[0]
                },
                Template = Templates.AppHome
            };
        }

        public Response Post(Models.Apps.RegExTester.Model theModel)
        {
            DotNetRegex DotNetRegex;

            if (theModel.IgnoreCase)
            {
                DotNetRegex = new DotNetRegex(theModel.Pattern, RegexOptions.IgnoreCase);
            }
            else
            {
                DotNetRegex = new DotNetRegex(theModel.Pattern);
            }

            MatchCollection matches = DotNetRegex.Matches(theModel.TestString);

            theModel.Matches = new string[matches.Count];

            int index = 0;
            foreach(var i in matches)
            {
                theModel.Matches[index] = i.ToString();
                index++;
            }

            return new Response
            {
                Model = theModel,
                Template = Templates.AppHome,
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
