using System.Threading.Tasks;
using System;
using System.Net.Http;
using Xamarin.Essentials;
using OpenQA.Selenium;
using OpenQA.Selenium.Opera;

namespace App2.Services
{
    class WebServices
    {
        public static string Parse(string productURL)
        {
            return "null";
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
