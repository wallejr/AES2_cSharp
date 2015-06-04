using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
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
