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
    /// Subclass of the Personal that will store and handle the the information specific for it-support personal
    /// </summary>
    class ItSupport : Personal
    {
        private string kompetens;

        public string Kompetens
        {
            get { return kompetens; }
            set { kompetens = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FName, LName);
        }
    }
}
