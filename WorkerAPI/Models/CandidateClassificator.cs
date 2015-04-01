using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkerAPI.Models
{
    public class CandidateClassificator
    {
        private static int[] acceptanceRange = new int[] { 7, 8, 9, 10 };

        private static bool IsQualified(int value)
        {
            if (acceptanceRange.Contains(value))
            {
                return true;
            }

            return false;
        }

        public static IDictionary<string, string> GetMessages(Candidate value)
        {
            var result = new Dictionary<string, string>();

            var frontEnd = IsQualified(value.Html) && IsQualified(value.Css) &&IsQualified(value.JavaScript);
            var backEnd = IsQualified(value.Python) && IsQualified(value.Django);
            var mobile = IsQualified(value.iOS) || IsQualified(value.Android);

            var defaultText = "Obrigado por se candidatar, assim que tivermos uma vaga disponível para {0} entraremos em contato.";
            
            if (frontEnd)
            {
                result.Add("frontEnd", string.Format(defaultText, "programador Front-End"));
            }

            if (backEnd)
            {
                result.Add("backEnd", string.Format(defaultText, "programador Back-End"));
            }

            if (mobile)
            {
                result.Add("mobile", string.Format(defaultText, "para programador Mobile"));
            }

            if (!frontEnd && !backEnd && !mobile)
            {
                result.Add("general", string.Format(defaultText, "programador"));
            }

            return result;
        }
    }
}