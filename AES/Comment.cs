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
    /// Class that will handle the information stored in the Comments object
    /// </summary>
    class Comment
    {
        public string Comments { get; set; }
        public DateTime WrittenTime { get; set; }
        public string WrittenBy { get; set; }
        public int CaseIDFK { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\n{2}\n\n", WrittenBy, WrittenTime.ToString(), Comments);
        }
    }
}
