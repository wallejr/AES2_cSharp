using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    public class processLeader : Personal
    {
        private string permissionCreate;

        public string PermissionCreate
        {
            get { return permissionCreate; }
            set { permissionCreate = value; }
        }

        public bool permissionCreate()
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
