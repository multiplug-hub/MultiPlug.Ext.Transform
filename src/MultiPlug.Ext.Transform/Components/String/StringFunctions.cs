
namespace MultiPlug.Ext.Transform.Components.String
{
    internal static class StringFunctions
    {
        internal static string ToLower(string theCurrentValue, string theArg1, string theArg2)
        {
            return theCurrentValue.ToLower();
        }

        internal static string ToUpper(string theCurrentValue, string theArg1, string theArg2)
        {
            return theCurrentValue.ToUpper();
        }

        internal static string Insert(string theCurrentValue, string theArg1, string theArg2)
        {
            int StartIndex = int.Parse(theArg1);

            return theCurrentValue.Insert(StartIndex, theArg2);
        }

        internal static string Trim(string theCurrentValue, string theArg1, string theArg2)
        {
            return theCurrentValue.Trim();
        }

        internal static string TrimValue(string theCurrentValue, string theArg1, string theArg2)
        {
            return theCurrentValue.Trim(theArg1.ToCharArray());
        }

        internal static string TrimEnd(string theCurrentValue, string theArg1, string theArg2)
        {
            return theCurrentValue.TrimEnd(theArg1.ToCharArray());
        }

        internal static string TrimStart(string theCurrentValue, string theArg1, string theArg2)
        {
            return theCurrentValue.TrimStart(theArg1.ToCharArray());
        }

        internal static string PadLeft(string theCurrentValue, string theArg1, string theArg2)
        {
            int TotalWidth = int.Parse(theArg1);

            return theCurrentValue.PadLeft(TotalWidth);
        }

        internal static string PadLeftValue(string theCurrentValue, string theArg1, string theArg2)
        {
            int TotalWidth = int.Parse(theArg1);

            return theCurrentValue.PadLeft(TotalWidth, theArg2[0]);
        }

        internal static string PadRight(string theCurrentValue, string theArg1, string theArg2)
        {
            int TotalWidth = int.Parse(theArg1);

            return theCurrentValue.PadRight(TotalWidth);
        }

        internal static string PadRightValue(string theCurrentValue, string theArg1, string theArg2)
        {
            int TotalWidth = int.Parse(theArg1);

            return theCurrentValue.PadRight(TotalWidth, theArg2[0]);
        }

        internal static string Remove(string theCurrentValue, string theArg1, string theArg2)
        {
            int StartIndex = int.Parse(theArg1);

            return theCurrentValue.Remove(StartIndex);
        }

        internal static string RemoveLength(string theCurrentValue, string theArg1, string theArg2)
        {
            int StartIndex = int.Parse(theArg1);
            int Length = int.Parse(theArg2);

            return theCurrentValue.Remove(StartIndex, Length);
        }

        internal static string Replace(string theCurrentValue, string theArg1, string theArg2)
        {
            return theCurrentValue.Replace(theArg1, theArg2);
        }

        internal static string SubString(string theCurrentValue, string theArg1, string theArg2)
        {
            int StartIndex = int.Parse(theArg1);

            return theCurrentValue.Substring(StartIndex);
        }

        internal static string SubStringLength(string theCurrentValue, string theArg1, string theArg2)
        {
            int StartIndex = int.Parse(theArg1);
            int Length = int.Parse(theArg2);

            return theCurrentValue.Substring(StartIndex, Length);
        }
    }
}
