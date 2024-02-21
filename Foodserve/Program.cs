using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FoodServe.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FoodServe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    // public class ApiService
    //{
    //    private readonly HttpClient _httpClient;

    //    public ApiService(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;
    //    }

    //    public async Task<List<Food>> GetFoodAsync()
    //    {
    //        var response = await _httpClient.GetStringAsync("https://localhost:44319/api/Food");
    //        return JsonConvert.DeserializeObject<List<Food>>(response);
    //    }
    //}
}
