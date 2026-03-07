using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Net.NetworkInformation;

namespace DataAccess
{
    public class DAorders : ConnectionSql
    {
        //Variables
        private int _order_id;
        private int _customer_id;
        private int _order_date;

        private string _textobuscar;

        public int Order_id { get => _order_id; set => _order_id = value; }
        public int Customer_id { get => _customer_id; set => _customer_id = value; }
        public int Order_date { get => _order_date; set => _order_date = value; }
        public string Textobuscar { get => _textobuscar; set => _textobuscar = value; }

        //Constructores
        public DAorders() { }
        public DAorders(int order_id, int customer_id, int order_date, string textobuscar)
        {
            Order_id = order_id;
            Customer_id = customer_id;
            Order_date = order_date;
            Textobuscar = textobuscar;
        }
        //Métodos 
        public string Insertar(DAorders Orders, List<DAorder_items> Detalle)
        {
            string rpta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = ConnectionSql.cn;
                sqlCon.Open();
                //Establecer la transacción 
                SqlTransaction sqlTra = sqlCon.BeginTransaction();
                //Establecer el comando 
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTra;
                sqlCmd.CommandText = "spinsertar_orders";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParOrder_id = new SqlParameter();
                ParOrder_id.ParameterName = "@order_id";
                ParOrder_id.SqlDbType = SqlDbType.Int;
                ParOrder_id.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(ParOrder_id);

                SqlParameter ParCustomer_id = new SqlParameter();
                ParCustomer_id.ParameterName = "@customer_id";
                ParCustomer_id.SqlDbType = SqlDbType.Int;
                ParCustomer_id.Value = Orders.Customer_id;
                sqlCmd.Parameters.Add(ParCustomer_id);

                SqlParameter ParOrder_date = new SqlParameter();
                ParOrder_date.ParameterName = "@order_date";
                ParOrder_date.SqlDbType = SqlDbType.DateTime;
                ParOrder_date.Value = Orders.Order_date;
                sqlCmd.Parameters.Add(ParOrder_date);
                //Ejecutamos el comando
                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE INGRESÓ EL REGISTRO";
                if (rpta.Equals("OK"))
                {
                    //Obtener el order_id de la order
                    Order_id = Convert.ToInt32(sqlCmd.Parameters["@order_id"].Value);
                    foreach (DAorder_items det in Detalle)
                    {
                        det.Order_id = Order_id;
                        //Llamar al método insertar de la clase DAorder_items
                        rpta = det.Insertar(det, ref sqlCon, ref sqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
                        }
                    }
                }
                if (rpta.Equals("OK")) { sqlTra.Commit(); }  
                else {  sqlTra.Rollback(); } 
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally { if (sqlCon.State == ConnectionState.Open) sqlCon.Close(); }
            return rpta;
        }
        //Método Anular una orden
        public string Eliminar(DAorders Orders)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spanular_order";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parorder_id = new SqlParameter();
                        Parorder_id.ParameterName = "@order_id";
                        Parorder_id.SqlDbType = SqlDbType.Int;
                        Parorder_id.Value = Orders.Order_id;
                        command.Parameters.Add(Parorder_id);
                        //Ejecutamos el comando
                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE ANULÓ EL REGISTRO";

                    }
                    catch (Exception ex)
                    {
                        rpta = ex.Message;
                    }
                    finally { if (connection.State == ConnectionState.Open) connection.Close(); }
                }
            }
            return rpta;
        }
        //Mostrar Orders
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("orders");
            using (SqlConnection connection = getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spmostrar_order";
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(dtResultado);

                    }

                    catch (Exception)
                    {
                        dtResultado = null;
                    }
                    finally { if (connection.State == ConnectionState.Open) connection.Close(); }
                }
            }
            return dtResultado;
        }

        public DataTable BuscarFechas(string TextoBuscar1, string TextoBuscar2)
        {
            DataTable dtResultado = new DataTable("orders");
            using (SqlConnection connection = getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spbuscar_order_fecha";
                        command.CommandType = CommandType.StoredProcedure;



                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(dtResultado);

                    }

                    catch (Exception)
                    {
                        dtResultado = null;
                    }
                    finally { if (connection.State == ConnectionState.Open) connection.Close(); }
                }
            }
            return dtResultado;
        }
    }
}
