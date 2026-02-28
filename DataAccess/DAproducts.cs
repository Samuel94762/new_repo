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
        private string[] _textobuscar;

        //Refactorizar las variables privadas para generar las propiedades
        public int Product_id { get => _product_id; set => _product_id = value; }
        public string Product_name { get => _product_name; set => _product_name = value; }
        public int Model_year { get => _model_year; set => _model_year = value; }
        public decimal Price { get => _price; set => _price = value; }
        public byte[] Imagen { get => _imagen; set => _imagen = value; }
        public string[] Textobuscar { get => _textobuscar; set => _textobuscar = value; }
        //Constructores
        public DAproducts() { }
        public DAproducts(int product_id, string product_name, int model_year, decimal price, byte[] imagen, string[] textobuscar)
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


                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
