using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDao : ConnectionSql
    {   
        public DataTable getSalesOrder(DateTime fromDate, DateTime toDate) 
        {
            using (var connection = getConnection())
            {
                connection.Open();
                using (var command = new SqlCommand()) 
                { 
                    command.Connection = connection;
                    command.CommandText = @"getSalesOrder";
                    command.Parameters.AddWithValue("@fromDate", fromDate);
                    command.Parameters.AddWithValue("@toDate", toDate);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    var table = new DataTable();
                    table.Load(reader);
                    reader.Dispose();
                    return table;
                }
            };
        }
    }
}
