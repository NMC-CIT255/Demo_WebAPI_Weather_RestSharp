using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_WebAPI_Weather.ConsoleUtilities
{
    /// <summary>
    /// static class to manage the console game theme
    /// </summary>
    public static class ConsoleTheme
    {
        //
        // welcome/closing screen colors
        //
        public static ConsoleColor WelcomeClosingScreenBackgroundColor = ConsoleColor.DarkRed;
        public static ConsoleColor WelcomeClosingScreenForegroundColor = ConsoleColor.Yellow;

        //
        // application screen colors
        //
        public static ConsoleColor ApplicationBackgroundColor = ConsoleColor.Gray;
        public static ConsoleColor ApplicationForegroundColor = ConsoleColor.DarkBlue;
    }
}
