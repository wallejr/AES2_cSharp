using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    public class Category
    {
        public static string getCategorName(Competense category)
        {
            string kategori = null;

            switch(category)
            {
                case Competense.Installation:
                    kategori = Competense.Installation.ToString();
                    break;
                case Competense.Network:
                    kategori = Competense.Network.ToString();
                    break;
                case Competense.Security:
                    kategori = Competense.Security.ToString();
                    break;
                case Competense.Users:
                    kategori = Competense.Users.ToString();
                    break;

            }

            return kategori;

        }

        public void getArbBesk()
        {

        }
    }
}
