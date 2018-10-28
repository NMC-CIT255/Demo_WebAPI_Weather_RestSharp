using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net;

namespace Demo_WebAPI_Weather.DataAccessLayer
{
    public class RestApiClientSync : IRestApiClient
    {
        /// <summary>
        /// execute a rest client request
        /// </summary>
        /// <param name="restClient">rest api client</param>
        /// <param name="request">rest api request</param>
        /// <returns>weather data object</returns>
        /// <out>response status</out>
        public WeatherData ExecuteRequest(RestClient restClient, IRestRequest request, out ResponseStatusCode responseStatusCode)
        {
            WeatherData weatherData = null;

            responseStatusCode = ResponseStatusCode.COMPLETE;

            var response = restClient.Execute(request);
            var content = response.Content;
            weatherData = JsonConvert.DeserializeObject<WeatherData>(content);

            //
            // convert RestSharp error status to application's response status code
            //
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                responseStatusCode = ResponseStatusCode.TRANSPORT_ERROR;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                responseStatusCode = ResponseStatusCode.UNAUTHORIZED;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                responseStatusCode = ResponseStatusCode.BAD_REQUEST;
            }

            return weatherData;
        }
    }
}
