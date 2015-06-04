using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    public enum Status
    {

        [Description("Not Assigned")]
        NotAssigned,
        [Description("Open")]
        Open,
        [Description("Assigned")]
        Assigned,
        [Description("Closed")]
        Closed,
        
    }
}
