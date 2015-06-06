/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    /// <summary>
    /// Enum that holds the information that will be displayed in combox for departments.
    /// It could have been a table aswell, but thought that in real enterprise this informationed
    /// could be retrieved from a hr application.
    /// </summary>
    public enum Department
    {
        Finance,
        Logistik,
        Postal,
        Cleaning,
        Productions,
        Development,

    }
}
