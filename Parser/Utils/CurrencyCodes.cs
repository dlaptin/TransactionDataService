using System.Collections.Generic;
using System.Globalization;

namespace ParserService
{
    sealed class CurrencyCodes
    {
        public static HashSet<string> Codes { get; set; }

        static CurrencyCodes()
        {
            Codes = new HashSet<string>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var ci in cultures)
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                Codes.Add(ri.ISOCurrencySymbol);
            }
        }
    }
}
