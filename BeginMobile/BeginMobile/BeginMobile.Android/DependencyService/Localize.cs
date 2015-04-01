using System.Globalization;
using BeginMobile.Android.DependencyService;
using BeginMobile.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace BeginMobile.Android.DependencyService
{
     public class Localize: ILocalize
    {
         public CultureInfo GetCurrentCultureInfo()
         {
             var androidLocale = Java.Util.Locale.Default;
             var netLanguage = androidLocale.ToString().Replace("_", "-");
             return new CultureInfo(netLanguage);
         }
    }
}