using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class ConnectionSql
    {
        public static string cn = "Server=(local); DataBase=BikeStore_Samuel; integrated security=true";
        private readonly string _connectionString ;

        public ConnectionSql()
        {
            _connectionString = cn;
        }


        protected SqlConnection getConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
