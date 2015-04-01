using System.Globalization;
using BeginMobile.Interfaces;
using BeginMobile.iOS.DependencyService;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace BeginMobile.iOS.DependencyService
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = pref.Replace("_", "-");
            }

            return new CultureInfo(netLanguage);
        }
    }
}