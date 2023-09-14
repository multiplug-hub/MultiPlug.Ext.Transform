using System.Text;
using System.Collections.Generic;
using MultiPlug.Ext.Transform.Models.Settings.String;
using MultiPlug.Ext.Transform.Models.Components.String;

namespace MultiPlug.Ext.Transform.Controllers.Settings.Strings
{
    public static class ActionCreator
    {
        private static Dictionary<string, ActionMeta> Action = new Dictionary<string, ActionMeta>()
        {
            {Actions.ToUpper, new ActionMeta(Actions.ToUpper, "To Upper Case", 0, string.Empty, string.Empty) },
            {Actions.ToLower, new ActionMeta(Actions.ToLower, "To Lower Case", 0, string.Empty, string.Empty) },
            {Actions.Insert, new ActionMeta(Actions.Insert, "Insert", 2, "Start Index", "Value") },
            {Actions.Trim, new ActionMeta(Actions.Trim, "Trim", 0, string.Empty, string.Empty) },
            {Actions.TrimValue, new ActionMeta(Actions.TrimValue, "Trim Value", 1, "Value", string.Empty) },
            {Actions.TrimEnd, new ActionMeta(Actions.TrimEnd, "Trim End", 1, "Value", string.Empty) },
            {Actions.TrimStart, new ActionMeta(Actions.TrimStart, "Trim Start", 1, "Value", string.Empty) },
            {Actions.PadLeft, new ActionMeta(Actions.PadLeft, "Pad Left", 1, "Total Width", string.Empty) },
            {Actions.PadLeftValue, new ActionMeta(Actions.PadLeftValue, "Pad Left with Padding Character", 2, "Total Width", "Padding Character") },
            {Actions.PadRight, new ActionMeta(Actions.PadRight, "Pad Right", 1, "Total Width", string.Empty) },
            {Actions.PadRightValue, new ActionMeta(Actions.PadRightValue, "Pad Right with Padding Character", 2, "Total Width", "Padding Character") },
            {Actions.Remove, new ActionMeta(Actions.Remove, "Remove", 1, "Start Index", string.Empty) },
            {Actions.RemoveLength, new ActionMeta(Actions.RemoveLength, "Remove with Length", 2, "Start Index", "Length") },
            {Actions.Replace, new ActionMeta(Actions.Replace, "Replace", 2, "Old Value", "New Value") },
            {Actions.SubString, new ActionMeta(Actions.SubString, "Substring", 1, "Start Index", string.Empty) },
            {Actions.SubStringLength, new ActionMeta(Actions.SubStringLength, "Substring with Length", 2, "Start Index", "Length") }
        };

        private static string isSelected(string theValue, string theCurrentValue)
        {
            return (theValue == theCurrentValue) ? "selected" : "";
        }

        public static string CreateList(ActionMeta theRecord)
        {
            var sb = new StringBuilder();

            foreach (KeyValuePair<string, ActionMeta> kvp in Action)
            {
                sb.AppendLine("<option value=\"" + kvp.Key +"\" " + isSelected(kvp.Key, theRecord.Action) + ">" + kvp.Value.ActionFullName + "</option>");
            }

            return sb.ToString();
        }

        public static string CreateList()
        {
            var sb = new StringBuilder();

            foreach (KeyValuePair<string, ActionMeta> kvp in Action)
            {
                sb.AppendLine("<option value=\"" + kvp.Key + "\" >" + kvp.Value.ActionFullName + "</option>");
            }

            return sb.ToString();
        }

        private static string Arg1Helper(int theArgCount)
        {
            return theArgCount > 0 ? "text" : "hidden"; 
        }

        private static string Arg2Helper(int theArgCount)
        {
            return theArgCount > 1 ? "text" : "hidden";
        }

        public static string NewStep(string theActionName, string theArg1, string theArg2)
        {
            ActionMeta ActionMeta = Action[theActionName];

            return @"<li>
                        <div class=""row-fluid"">
                            <div class=""span2"">
                                <div class=""row-fluid""><div class=""span12""></div></div>
                                <div class=""row-fluid""><div class=""span12""><button value="""" type=""button"" class=""item btn btn-red delete-step""><i class=""icon-trash icon-large""></i></button></div></div>
                            </div>
                            <div class=""span4"">
                                <div class=""row-fluid""><div class=""span12"">Action</div></div>
                                <div class=""row-fluid""><div class=""span12""><select name=""StepAction"" class=""input-block-level action-list"">
                                " + CreateList(ActionMeta) + @"
                                </select></div></div>
                            </div>
                            <div class=""span3"">
                                <div class=""row-fluid""><div class=""span12"">" + ActionMeta.Arg1Name + @"</div></div>
                                <div class=""row-fluid""><div class=""span12""><input name=""StepArg1"" type=""" + Arg1Helper(ActionMeta.ArgsCount) + @""" value=""" + theArg1 + @""" class=""input-block-level""></div></div>
                            </div>
                            <div class=""span3"">
                                <div class=""row-fluid""><div class=""span12"">" + ActionMeta.Arg2Name + @"</div></div>
                                <div class=""row-fluid""><div class=""span12""><input name=""StepArg2"" type=""" + Arg2Helper(ActionMeta.ArgsCount) + @""" value=""" + theArg2 + @""" class=""input-block-level""></div></div>
                            </div>
                        </div>
                    </li>";

        }
    }
}
