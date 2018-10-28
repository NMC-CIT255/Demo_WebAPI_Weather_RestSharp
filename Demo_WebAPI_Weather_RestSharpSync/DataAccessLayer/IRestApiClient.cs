using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_WebAPI_Weather.DataAccessLayer
{
    interface IRestApiClient
    {
        WeatherData ExecuteRequest(RestClient restClient, IRestRequest request, out ResponseStatusCode restClientErrorCode);
    }
}
