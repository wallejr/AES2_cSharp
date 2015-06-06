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
    /// Class that will store and handle information about ProcessLeaders, thos who have permission to
    /// create a case. Inherits from class Personal
    /// </summary>
    public class processLeader : Personal
    {
        private string permissionCreate;
        private string compentence;

        public string Compentence
        {
            get { return compentence; }
            set { compentence = value; }
        }

        public string PermissionCreate
        {
            get { return permissionCreate; }
            set { permissionCreate = value; }
        }

        public bool permissionValidation()
        {
            if(PermissionCreate == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
