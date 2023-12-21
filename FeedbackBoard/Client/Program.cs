using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using System.Net.Http.Json;

namespace FeedbackBoard.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

              _ = builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



            _ = builder.Services.AddScoped(sp => new BlobService("DefaultEndpointsProtocol=https;AccountName=feedbackboard;AccountKey=LcFUyAYRk3c6BufPFY4psSyO6S1JsEpQ5y0ZWaURb5ZnufAI5QFPJpL2cTQ9+wc1KtfWtJJuAueI+AStP4olFQ==;EndpointSuffix=core.windows.net"));









            //var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            //builder.Services.AddScoped(_ => httpClient);

            //// Load configuration from appsettings.json
            //var appSettings = await httpClient.GetFromJsonAsync<AppSettings>("appsettings.json");
            //if (appSettings != null)
            //{
            //    builder.Services.AddScoped(sp => new BlobService(appSettings.AzureBlobStorageConnectionString));
            //}



            await builder.Build().RunAsync();
        }
    }

    //public class AppSettings
    //{
    //    public string AzureBlobStorageConnectionString { get; set; }
    //}
}


//DefaultEndpointsProtocol=https;AccountName=feedbackboard;AccountKey=LcFUyAYRk3c6BufPFY4psSyO6S1JsEpQ5y0ZWaURb5ZnufAI5QFPJpL2cTQ9+wc1KtfWtJJuAueI+AStP4olFQ==;EndpointSuffix=core.windows.net