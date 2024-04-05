using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

internal static class WebServicesHelpers
{

    public static async Task OpenProductOnWebsite(string productURL)
    {
        try
        {
            Uri uri = new Uri(productURL);
            await Browser.OpenAsync(uri);
        }
        catch (System.UriFormatException) { }
    }
}