using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_WebAPI_Weather.Models
{
    public class LocationInformation
    {
        public LocationCoordinates LocationCoordinates { get; set; }
        public int ZipCode { get; set; }
        public string Name { get; set; }
    }
}
