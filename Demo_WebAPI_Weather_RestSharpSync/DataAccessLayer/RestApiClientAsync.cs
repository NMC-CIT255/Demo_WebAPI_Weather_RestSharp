using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace Demo_WebAPI_Weather.DataAccessLayer
{
    class RestApiClientAsync
    {
        public async Task<WeatherData> ExecuteRequest(RestClient restClient, IRestRequest request)
        {
            WeatherData weatherData = null;

            //var client = new RestClient();
            //client.BaseUrl = new Uri("http://api.openweathermap.org/data/2.5/");

            //var request = new RestRequest("weather", Method.GET);
            //request.AddParameter("appid", "864d252afc928abff4010abe732617a1");
            //request.AddParameter("lon", "-86");
            //request.AddParameter("lat", "45");

            //
            // execute the request synchronously
            //
            //IRestResponse response = client.Execute<WeatherData>(request);
            //var content = response.Content; // raw content as string
            //weatherData = JsonConvert.DeserializeObject<WeatherData>(content);

            //var asyncHandler = restClient.ExecuteAsync<WeatherData>(request, r =>
            //{
            //    if (r.ResponseStatus == ResponseStatus.Completed)
            //    {
            //        weatherData = r.Data;
            //    }
            //});

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<WeatherData> response = (RestResponse<WeatherData>)client.Execute<WeatherData>(request);
            //weatherData = response.Data;

            // easy async support
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});

            var asyncHandle = restClient.ExecuteAsync(request, response =>
            {
                Console.WriteLine(response.Content);
            });

            //IRestResponse response = client.ExecuteAsync(request, ;


            // async with deserialization
            //var asyncHandle = client.ExecuteAsync<WeatherData>(request, response => {
            //   Console.WriteLine(response.Data.Name);
            //});

            // abort the request on demand
            //asyncHandle.Abort();


            return weatherData;
        }
    }
}
