using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bifrost.Win.Controls
{
    public static class ControlUtil
    {
        public static bool checkLetterKO(string text)
        {
            bool result = false;
            char[] charArr = text.ToCharArray();
            foreach (char c in charArr)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    result = true;
                    return result;
                }
            }

            return result;
        }
    }
}
