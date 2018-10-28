using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_WebAPI_Weather.ConsoleUtilities;
using Demo_WebAPI_Weather.DataAccessLayer;
using Demo_WebAPI_Weather.BusinessLayer;
using Demo_WebAPI_Weather.Models;
using RestSharp;

namespace Demo_WebAPI_Weather.PresentationLayer
{
    class Presenter
    {
        BusinessLogic _businessLogic;

        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="businessLogic">business logic object</param>
        public Presenter(BusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
            RunApplicationLoop();
        }

        /// <summary>
        /// application loop
        /// </summary>
        private void RunApplicationLoop()
        {
            DisplayWelcomeScreen();
            DisplayMainMenu();
            DisplayClosingScreen();
        }

        /// <summary>
        /// display main menu
        /// </summary>
        private void DisplayMainMenu()
        {
            bool runApp = true;
            WeatherData _weatherData = null;
            LocationInformation _locationInformation = null;

            InitializeApplicationWindow();

            do
            {
                DisplayHeader("\t\tMain Menu");
                Console.WriteLine("");
                Console.WriteLine("\tA. Get Weather Data by Longitude and Latitude");
                Console.WriteLine("\tB. Get Weather Data by Zip Code");
                Console.WriteLine("\tC. Display Weather Data Short Format");
                Console.WriteLine("\tQ. Quit");
                Console.WriteLine();
                Console.Write("\tEnter Menu Choice:");
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.KeyChar.ToString().ToLower())
                {
                    case "a":
                        _weatherData = DisplayGetWeatherByLonLat(out _locationInformation);
                        break;
                    case "b":
                        _weatherData = DisplayGetWeatherByZipCode(out _locationInformation);
                        break;
                    case "c":
                        DisplayWeatherDataShortFormat(_weatherData, _locationInformation);
                        break;
                    case "q":
                        runApp = false;
                        break;
                    default:
                        Console.WriteLine("Please make a selection A-C or Q.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (runApp);

        }

        /// <summary>
        /// display get the weather data by longitude and latitude
        /// </summary>
        /// <returns>weather data</returns>
        private WeatherData DisplayGetWeatherByLonLat(out LocationInformation locationInformation)
        {
            WeatherData weatherData;
            double lon, lat;

            locationInformation = new LocationInformation();

            DisplayHeader("Weather by Longitude and latitude");

            //
            // get longitude and latitude from user
            //
            do
            {
                Console.Write("\tEnter Longitude:");
            } while (!double.TryParse(Console.ReadLine(), out lon));
            do
            {
                Console.Write("\tEnter latitude:");
            } while (!double.TryParse(Console.ReadLine(), out lat));

            //
            // acquire weather data from Open Weather Map
            //
            weatherData = _businessLogic.GetWeatherByLonLat(new LocationCoordinates() { Longitude = lon, Latitude = lat }, out ResponseStatusCode responseStatusCode);

            if (responseStatusCode == ResponseStatusCode.COMPLETE)
            {
                //
                // update LocationInformation object
                //
                locationInformation.LocationCoordinates = new LocationCoordinates() { Longitude = lon, Latitude = lat };
                locationInformation.Name = weatherData.Name;
                locationInformation.ZipCode = 0;

                Console.WriteLine($"\tWeather data for Longitude:{lon:0.##} and Latitude:{lat:0.##} acquired.");
            }
            else
            {
                Console.WriteLine(DisplayResponseStatusErrorMessage(responseStatusCode));
            }


            DisplayContinuePrompt();

            return weatherData;
        }

        /// <summary>
        /// display get the weather data by zip code
        /// </summary>
        /// <returns>weather data</returns>
        private WeatherData DisplayGetWeatherByZipCode(out LocationInformation locationInformation)
        {
            WeatherData weatherData;
            int zipCode;

            locationInformation = new LocationInformation();

            DisplayHeader("Weather by Zip Code");

            //
            // get zip code from user
            //
            do
            {
                Console.Write("\tEnter Zip Code:");
            } while (!int.TryParse(Console.ReadLine(), out zipCode));

            //
            // acquire weather data from Open Weather Map
            //
            weatherData = _businessLogic.GetWeatherByZipCode(zipCode, out ResponseStatusCode responseStatusCode);

            if (responseStatusCode == ResponseStatusCode.COMPLETE)
            {
                //
                // update LocationInformation object
                //
                locationInformation.ZipCode = zipCode;
                locationInformation.Name = weatherData.Name;
                locationInformation.LocationCoordinates = new LocationCoordinates() { Longitude = weatherData.Coord.Lon, Latitude = weatherData.Coord.Lat };

                Console.WriteLine($"\tWeather data for Zip Code:{zipCode} acquired.");
            }
            else
            {
                Console.WriteLine(DisplayResponseStatusErrorMessage(responseStatusCode));
            }

            DisplayContinuePrompt();

            return weatherData;
        }

        /// <summary>
        /// display current weather in short format
        /// </summary>
        /// <param name="weatherData">weather data</param>
        /// <param name="locationInformation">location information</param>
        /// <param name="locationDesignationMethod"></param>
        private void DisplayWeatherDataShortFormat(WeatherData weatherData, LocationInformation locationInformation)
        {
            DisplayHeader("Current Weather Data");

            Console.WriteLine($"\tWeather Data for {locationInformation.Name}");
            if (locationInformation.ZipCode != 0) Console.WriteLine("\tZip Code:" + locationInformation.ZipCode);
            Console.WriteLine($"\tLongitude: {locationInformation.LocationCoordinates.Longitude:0.##}");
            Console.WriteLine($"\tLatitude: {locationInformation.LocationCoordinates.Latitude:0.##}");
            Console.WriteLine(DisplayLatitudeLongitude(weatherData.Coord.Lon, weatherData.Coord.Lat));
            Console.WriteLine();

            Console.WriteLine($"\tTemperature: {DisplayFahrenheit(weatherData.Main.Temp)}");
            Console.WriteLine($"\tHumidity: {weatherData.Main.Humidity:0.}%");
            Console.WriteLine($"\tWind: {DisplayMilesPerHour(weatherData.Wind.Speed)} {DisplayCardinalDirection(weatherData.Wind.Deg)}");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display user error message base on the response status code
        /// </summary>
        /// <param name="responseStatusCode">response status code</param>
        /// <returns>error message</returns>
        static string DisplayResponseStatusErrorMessage(ResponseStatusCode responseStatusCode)
        {
            string errorMessage = "\t*************************************************************************************\n";

            switch (responseStatusCode)
            {
                case ResponseStatusCode.TRANSPORT_ERROR:
                    errorMessage += "\t\tAn error occurred in the request: network is down, failed DNS lookup, etc \n";
                    break;
                case ResponseStatusCode.UNAUTHORIZED:
                    errorMessage += "\t\tAn error occurred in the request: key is unauthorized \n";
                    break;
                case ResponseStatusCode.BAD_REQUEST:
                    errorMessage += "\t\tAn error occurred in the request: the request was malformed\n";
                    break;
            }

            errorMessage += "\t*************************************************************************************\n";

            return errorMessage;
        }

        /// <summary>
        /// Display the Closing Screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            InitializeWelcomeClosingWindow();

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tDemo Provided by NMC CIT Department");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display the Welcome Screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            InitializeWelcomeClosingWindow();

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tWeather Web API Demo");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        #region HEDPER METHODS

        /// <summary>
        /// initialize application screen configuration
        /// </summary>
        static void InitializeApplicationWindow()
        {
            Console.ForegroundColor = ConsoleTheme.ApplicationForegroundColor;
            Console.BackgroundColor = ConsoleTheme.ApplicationBackgroundColor;
        }

        /// <summary>
        /// initialize welcome and closing screen configuration
        /// </summary>
        static void InitializeWelcomeClosingWindow()
        {
            Console.ForegroundColor = ConsoleTheme.WelcomeClosingScreenForegroundColor;
            Console.BackgroundColor = ConsoleTheme.WelcomeClosingScreenBackgroundColor;
        }

        /// <summary>
        /// display a screen header
        /// </summary>
        /// <param name="headerText">header content</param>
        static void DisplayHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        /// <summary>
        /// display a continue prompt w/ ReadKey()
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// convert Kalvin to Fahrenheit
        /// </summary>
        /// <param name="degreesKalvin"></param>
        /// <returns>degrees Fahrenheit</returns>
        static string DisplayFahrenheit(double degreesKalvin)
        {
            return ((degreesKalvin - 273.15) * 1.8 + 32) + "\u00B0F";
        }

        /// <summary>
        /// convert meter/second to miles/hour
        /// </summary>
        /// <param name="speedMetersPerSecond"></param>
        /// <returns>miles per hour</returns>
        static string DisplayMilesPerHour(double speedMetersPerSecond)
        {
            return speedMetersPerSecond * (3600 / 1609) + "mph";
        }

        /// <summary>
        /// convert directions in degrees to cardinal directions
        /// </summary>
        /// <param name="degrees">directions in degrees</param>
        /// <returns>cardinal directions</returns>
        static string DisplayCardinalDirection(double degrees)
        {
            string[] caridnalDirections = { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };
            return $"{degrees:0}\u00B0" + caridnalDirections[(int)Math.Round(((double)degrees % 360) / 45)];
        }

        /// <summary>
        /// display longitude and latitude with cardinal directions
        /// </summary>
        /// <param name="locationCoordinates"></param>
        /// <returns>string with formated coordinates</returns>
        static string DisplayLatitudeLongitude(double longitude, double latitude)
        {
            string latitudeLongitude;

            latitudeLongitude =
                "\tLongitude: " + Math.Abs(longitude) + (longitude >= 0 ? "E" : "W") + Environment.NewLine +
                "\tLatitude: " + Math.Abs(latitude) + (latitude >= 0 ? "N" : "S") + Environment.NewLine;

            return latitudeLongitude;
        }

        #endregion
    }
}
