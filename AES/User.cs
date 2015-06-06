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
    /// Class that holds information about the users. But i decided not to implement it this way, since
    /// its better to be able write each requester as it appers.
    /// In an enterprise there would be a HR system that holds each user in the company that you can connect to
    /// that database and get all users in the company
    /// </summary>
    class User : Personal
    {
        private string compName;

        public string CompName
        {
            get { return compName; }
            set { compName = value; }
        }

        public void getInfo()
        {
            
        }
    }
}
