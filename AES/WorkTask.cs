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
    /// Case that stores and holds the information of each Worktask
    /// </summary>
    class WorkTask
    {
        public string TaskTodo { get; set; }
        public string CreatedBy { get; set; }
    }
}
