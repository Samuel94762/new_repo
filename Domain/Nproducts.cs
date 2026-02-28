using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccess;

namespace Domain
{
    public class Nproducts
    {
        //Método Insertar que llama al método insertar de la clase DAProducts
        public static string Insertar(string product_name, int model_year, decimal price, byte[] imagen)
        {
            DAproducts Obj = new DAproducts();
            Obj.Product_name = product_name;
            Obj.Model_year = model_year;
            Obj.Price = price;
            Obj.Imagen = imagen;
            return Obj.Insertar(Obj);
        }
        public static string Editar(int product_id, string product_name, int model_year, decimal price, byte[] imagen)
        {
            DAproducts Obj = new DAproducts();
            Obj.Product_id = product_id;
            Obj.Product_name = product_name;
            Obj.Model_year = model_year;
            Obj.Price = price;
            Obj.Imagen = imagen;
            return Obj.Editar(Obj);
        }
        public static string Eliminar(int product_id)
        {
            DAproducts Obj = new DAproducts();
            Obj.Product_id = product_id;
            return Obj.Eliminar(Obj);
        }

        public static DataTable Mostrar()
        {
            DAproducts Obj = new DAproducts();
            return Obj.Mostrar();
        }

        public static DataTable BuscarNombre(string textobuscar)
        {
            DAproducts Obj = new DAproducts();
            Obj.Textobuscar = textobuscar;
            return Obj.BuscarNombre(Obj);

        }

    }
}
