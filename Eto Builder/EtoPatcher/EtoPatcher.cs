using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Eto_Builder.EtoPatcher
{
    public static class EtoPatcher
    {
        public static string PatchCode(string code)
        {
            var patchedCode = code;
            patchedCode = PatchTypeName(patchedCode);
            string[] stringSeparators = new string[] { "\r\n" };
            string[] rawCodeLines = patchedCode.Split(stringSeparators, StringSplitOptions.None);
            List<string> patchedCodeLines = new List<string>();
            foreach (string line in rawCodeLines)
            {
                string patchedLine = line;
                patchedLine = PatchCodeLine(patchedLine);
                if (!String.IsNullOrWhiteSpace(patchedLine))
                {
                    patchedCodeLines.Add(patchedLine);
                }
            }
            patchedCode = StitchLines(patchedCodeLines.ToArray());
            return patchedCode;
        }
        public static string PatchTypeName(string code)
        {
            var patchedCode = code;
            patchedCode = patchedCode.Replace("System.Windows.Forms", "Eto.Forms");
            patchedCode = patchedCode.Replace("System.Drawing", "Eto.Drawing");
            return patchedCode;
        }
        public static string PatchCodeLine(string code)
        {
            var patchedCode = code;
            patchedCode = PatchProperties(patchedCode);
            patchedCode = PatchAppStart(patchedCode);
            return patchedCode;
        }
        public static string PatchProperties(string code)
        {
            var patchedCode = code;
            patchedCode = patchedCode.ReplaceWildcard("*.Name*", "");
            return patchedCode;
        }
        public static string PatchAppStart(string code)
        {
            var patchedCode = code;
            patchedCode = Regex.Replace(patchedCode, @"(?:(?!Eto\.Forms\.Application\.Run\()(?:.|\n))*Eto\.Forms\.Application\.Run\(", "            new Eto.Forms.Application(Eto.Platform.Detect).Run(");
            patchedCode = Regex.Replace(patchedCode, @"ResumeLayout\((?:(?!\))(?:.|\n))*\)", "ResumeLayout()");
            return patchedCode;
        }
        public static string StitchLines(string[] lines)
        {
            return string.Join("\r\n", lines);
        }
        public static string ReplaceWildcard(this string s, string pattern, string replacement)
        {
            var rpattern = WildcardToRegex(pattern);
            return Regex.Replace(s, rpattern, replacement);
        }
        public static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                              .Replace(@"\*", ".*")
                              .Replace(@"\?", ".")
                       + "$";
        }
    }
}
