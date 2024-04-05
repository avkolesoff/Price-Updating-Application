using System;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace App2.Services
{
    class WebServices
    {
        public static string Parse(string productURL)
        {
            return "000";
        }

        public static async Task OpenProductOnWebsite (string productURL)
        {
            try
            {
                Uri uri = new Uri(productURL);
                await Browser.OpenAsync(uri);
            }
            catch (System.UriFormatException) { }
        }
    }
}
