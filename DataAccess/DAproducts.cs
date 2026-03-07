using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class DAproducts : ConnectionSql
    {
        //campos o variables
        private int _product_id;
        private string _product_name;
        private int _model_year;
        private decimal _price;
        private byte[] _imagen;
        private string _textobuscar;

        //Refactorizar las variables privadas para generar las propiedades
        public int Product_id { get => _product_id; set => _product_id = value; }
        public string Product_name { get => _product_name; set => _product_name = value; }
        public int Model_year { get => _model_year; set => _model_year = value; }
        public decimal Price { get => _price; set => _price = value; }
        public byte[] Imagen { get => _imagen; set => _imagen = value; }
        public string Textobuscar { get => _textobuscar; set => _textobuscar = value; }
        //Constructores
        public DAproducts() { }
        public DAproducts(int product_id, string product_name, int model_year, decimal price, byte[] imagen, string textobuscar)
        {
            Product_id = product_id;
            Product_name = product_name;
            Model_year = model_year;
            Price = price;
            Imagen = imagen;
            Textobuscar = textobuscar;
        }
        //Metodo Insertar
        public string Insertar(DAproducts Products)
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
                        command.CommandText = "spinsertar_products";
                        command.CommandType = CommandType.StoredProcedure;
                        
                        SqlParameter Parproduct_id = new SqlParameter();
                        Parproduct_id.ParameterName = "@product_id";
                        Parproduct_id.SqlDbType = SqlDbType.Int;
                        Parproduct_id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(Parproduct_id);

                        SqlParameter Parproduct_name = new SqlParameter();
                        Parproduct_name.ParameterName = "@product_name";
                        Parproduct_name.SqlDbType = SqlDbType.VarChar;
                        Parproduct_name.Size = 200;
                        Parproduct_name.Value = Products.Product_name;
                        command.Parameters.Add(Parproduct_name);

                        SqlParameter Parmodel_year = new SqlParameter();
                        Parmodel_year.ParameterName = "@model_year";
                        Parmodel_year.SqlDbType = SqlDbType.SmallInt;
                        Parmodel_year.Value = Products.Model_year;
                        command.Parameters.Add(Parmodel_year);

                        SqlParameter ParPrice = new SqlParameter();
                        ParPrice.ParameterName = "@price";
                        ParPrice.SqlDbType = SqlDbType.Money;
                        ParPrice.Value = Products.Price;
                        command.Parameters.Add(ParPrice);

                        SqlParameter ParImagen = new SqlParameter();
                        ParImagen.ParameterName = "@imagen";
                        ParImagen.SqlDbType = SqlDbType.Image;
                        ParImagen.Value = Products.Imagen;
                        command.Parameters.Add(ParImagen);

                        //Ejecutamos el comando
                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE GUARDÓ EL REGISTRO";

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

        public string Editar(DAproducts Products)
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
                        command.CommandText = "speditar_products";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parproduct_id = new SqlParameter();
                        Parproduct_id.ParameterName = "@product_id";
                        Parproduct_id.SqlDbType = SqlDbType.Int;
                        Parproduct_id.Value = Products.Product_id;
                        command.Parameters.Add(Parproduct_id);

                        SqlParameter Parproduct_name = new SqlParameter();
                        Parproduct_name.ParameterName = "@product_name";
                        Parproduct_name.SqlDbType = SqlDbType.VarChar;
                        Parproduct_name.Size = 200;
                        Parproduct_name.Value = Products.Product_name;
                        command.Parameters.Add(Parproduct_name);

                        SqlParameter Parmodel_year = new SqlParameter();
                        Parmodel_year.ParameterName = "@model_year";
                        Parmodel_year.SqlDbType = SqlDbType.SmallInt;
                        Parmodel_year.Value = Products.Model_year;
                        command.Parameters.Add(Parmodel_year);

                        SqlParameter ParPrice = new SqlParameter();
                        ParPrice.ParameterName = "@price";
                        ParPrice.SqlDbType = SqlDbType.Money;
                        ParPrice.Value = Products.Price;
                        command.Parameters.Add(ParPrice);

                        SqlParameter ParImagen = new SqlParameter();
                        ParImagen.ParameterName = "@imagen";
                        ParImagen.SqlDbType = SqlDbType.Image;
                        ParImagen.Value = Products.Imagen;
                        command.Parameters.Add(ParImagen);

                        //Ejecutamos el comando
                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE ACTUALIZÓ EL REGISTRO";

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
        public string Eliminar(DAproducts Products)
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
                        command.CommandText = "speliminar_products";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parproduct_id = new SqlParameter();
                        Parproduct_id.ParameterName = "@product_id";
                        Parproduct_id.SqlDbType = SqlDbType.Int;
                        Parproduct_id.Value = Products.Product_id;
                        command.Parameters.Add(Parproduct_id);
                        //Ejecutamos el comando
                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE ELIMINÓ EL REGISTRO";

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
        //Metodo Mostrar
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("products"); 
            using (SqlConnection connection = getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spmostrar_products";
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(dtResultado);

                    }

                    catch (Exception )
                    {
                        dtResultado = null;
                    }
                    finally { if (connection.State == ConnectionState.Open) connection.Close(); }
                }
            }
            return dtResultado;
        }
        //Método Buscar Nombre del producto
        public DataTable BuscarNombre(DAproducts Products)
        {
            DataTable dtResultado = new DataTable("products");
            using (SqlConnection connection = getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spbuscar_products_name";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter ParTextoBuscar = new SqlParameter();
                        ParTextoBuscar.ParameterName = "@texto_buscar";
                        ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                        ParTextoBuscar.Size = 50;
                        ParTextoBuscar.Value = Products.Textobuscar;
                        command.Parameters.Add(ParTextoBuscar);

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
