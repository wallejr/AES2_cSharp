/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    /// <summary>
    /// Enum that display selections in a selectable combobox.
    /// These information will be stored in the case as a String
    /// </summary>
    public enum Status
    {

        [Description("Open")]
        Open,
        [Description("Assigned")]
        Assigned,
        [Description("Closed")]
        Closed,
        
    }
}
