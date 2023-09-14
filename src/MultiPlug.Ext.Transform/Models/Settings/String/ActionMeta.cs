
namespace MultiPlug.Ext.Transform.Models.Settings.String
{
    public class ActionMeta
    {
        public ActionMeta(string theAction, string theActionFullName, int theArgsCount, string theArg1Name, string theArg2Name)
        {
            Action = theAction;
            ActionFullName = theActionFullName;
            ArgsCount = theArgsCount;
            Arg1Name = theArg1Name;
            Arg2Name = theArg2Name;
        }
        public string Action { get; private set; }
        public string ActionFullName { get; private set; }
        public int ArgsCount { get; private set; }
        public string Arg1Name { get; private set; }
        public string Arg2Name { get; private set; }
    }
}
