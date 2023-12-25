using System;
using Xamarin.Essentials;

namespace App2.Services
{
    class WebServices
    {
        public static string Parse(string productURL)
        {
            return "10000 р.";
        }

        public static async System.Threading.Tasks.Task OpenProductOnWebsite (string productURL)
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
