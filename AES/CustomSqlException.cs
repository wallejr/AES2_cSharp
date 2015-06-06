using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    /// <summary>
    /// A custom selfcreated Exception for Sql exception, inherits from the Exception class
    /// </summary>
    public class CustomSqlException : Exception
    {
        public CustomSqlException()
        {
        }

        public CustomSqlException(string message, System.Data.SqlClient.SqlException innerException)
            : base(message, innerException)
        {
        }

        public CustomSqlException(string message, InvalidCastException innerException)
            : base(message, innerException)
        {
        }
    }
}