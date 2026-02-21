using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SalesReport
    {
        //Atributos y Propiedades
        public DateTime  reportDate {  get; private set; }

        public DateTime startDate { get; private set; }

        public DateTime endDate { get; private set; }

        public List<SalesListing> salesListing { get; private set; }

        public List<NetSalesByPeriod> netSalesByPeriod { get; private set; }

        public double totalNetSales { get; private set; }

        //Métodos 
        public void createSalesOrderReport(DateTime fromDate, DateTime toDate)
        {
            //Implementar fechas

            reportDate = DateTime.Now;
            startDate = fromDate;
            endDate = toDate;

            //Crear Listado de Ventas
            var orderDao = new OrderDao();
            var result = orderDao.getSalesOrder(fromDate, toDate);

            salesListing = new List<SalesListing>();

            foreach (System.Data.DataRow rows in result.Rows)
            {
                var salesModel = new SalesListing()
                {
                    orderId = Convert.ToInt32(rows[0]),
                    orderDate = Convert.ToDateTime(rows[1]),
                    customer = Convert.ToString(rows[2]),
                    products = Convert.ToString(rows[3]),
                    totalAmount = Convert.ToDouble(rows[4])
                };
                salesListing.Add(salesModel);
                //Calcular Total de ventas netas
                totalNetSales += Convert.ToDouble(rows[4]);
            }
            //Crear ventas netas por periodo
            //Crear lista temporal de ventas netas por fecha

        } 


    }
}
