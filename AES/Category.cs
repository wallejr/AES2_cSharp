/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    /// <summary>
    /// Class that will store and handle information about categories object
    /// </summary>
    public class Category : ListManager<Category>
    {

        private string cat;

        public string Cat
        {
            get { return cat; }
            set { cat = value; }
        }

        public override string ToString()
        {
            return Cat;
        }
       
    }
}
