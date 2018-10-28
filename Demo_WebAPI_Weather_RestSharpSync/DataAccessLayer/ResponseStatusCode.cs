using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_WebAPI_Weather.DataAccessLayer
{
    public enum ResponseStatusCode
    {
        COMPLETE,
        TRANSPORT_ERROR,
        UNAUTHORIZED,
        BAD_REQUEST
    }
}
