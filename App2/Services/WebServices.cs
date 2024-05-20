using System.Threading.Tasks;
using System;
using System.Net.Http;
using Xamarin.Essentials;

namespace App2.Services
{
    class WebServices
    {
        public static string Parse(string productURL)
        {
            string price = string.Empty;
            try
            {
                using (var hdl = new HttpClientHandler() { AllowAutoRedirect = false, AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        using (var resp = clnt.GetAsync(productURL).Result)
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                var html = resp.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(html);
                                    price = doc.DocumentNode.SelectSingleNode(@"/html/body/div[1]/div/div/div[5]/div/div/div[1]/div/div[2]/div[4]/span[1]").InnerText;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return price;
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
