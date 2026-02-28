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
            var listSalesByDate = (from sales in salesListing
                                   group sales by sales.orderDate
                                   into listSales
                                   select new
                                   {
                                       date = listSales.Key, 
                                       amount = listSales.Sum(item => item.totalAmount)
                                   }).AsEnumerable();

            ////Obtener numero de dias
            int totalDays = Convert.ToInt32((toDate - fromDate).Days);
            ////Agrupar periodo por dias 
            if (totalDays <= 7)
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by sales.date.ToString("dd-MMM-yyyy") into listSales
                                    select new NetSalesByPeriod
                                    {
                                        period = listSales.Key,
                                        netSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
            ////Agrupar periodo por semanas
            else if (totalDays <= 30)
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                        sales.date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday) 
                                    into listSales
                                    select new NetSalesByPeriod
                                    {
                                        period = "Week "+ listSales.Key.ToString(),
                                        netSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
            ////Agrupar periodo por meses
            else if (totalDays <= 365)
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by sales.date.ToString ("MMM-yyyy") into listSales 
                                    select new NetSalesByPeriod
                                    {
                                        period = listSales.Key,
                                        netSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
            ////Agrupar periodo por años
            else
            {
                netSalesByPeriod = (from sales in listSalesByDate
                                    group sales by sales.date.ToString("yyyy") into listSales
                                    select new NetSalesByPeriod
                                    {
                                        period = listSales.Key,
                                        netSales = listSales.Sum(item => item.amount)
                                    }).ToList();
            }
        } 
    }
}
