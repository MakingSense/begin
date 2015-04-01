using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BeginMobile.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
