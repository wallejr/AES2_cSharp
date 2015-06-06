using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
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
