using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
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
