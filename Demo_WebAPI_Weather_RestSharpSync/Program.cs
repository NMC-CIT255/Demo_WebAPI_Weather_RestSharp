using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_WebAPI_Weather.DataAccessLayer;
using Demo_WebAPI_Weather.ConsoleUtilities;
using Demo_WebAPI_Weather.PresentationLayer;
using Demo_WebAPI_Weather.BusinessLayer;

using RestSharp;

namespace Demo_WebAPI_Weather
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            Presenter presenter = new Presenter(businessLogic);
        }





    }
}
