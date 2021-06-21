using IsisTravelCore.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsisTravelCore.Services
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(Order order)
        {
            var discAmount1 = order.Discount1 * order.Price1 / 100;
            var discAmount2 = order.Discount2 * order.Price2 / 100;
            var discAmount3 = order.Discount3 * order.Price3 / 100;
            var discAmount4 = order.Discount4 * order.Price4 / 100;
            var totalDisc = discAmount1 + discAmount2 + discAmount3 + discAmount4;
            var total1 = (order.Price1 * order.Quantity1) - discAmount1;
            var total2 = (order.Price2 * order.Quantity2) - discAmount2;
            var total3 = (order.Price3 * order.Quantity3) - discAmount3;
            var total4 = (order.Price4 * order.Quantity4) - discAmount4;
            var total = total1 + total2 + total3 + total4;
            var sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html><html><head><title></title></head><body><div width='90%' style='border: 2px dotted rgb(0,144,146)'>
                        <table width='100%' style='text-align: left'><thead><tr><th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                        <th><img src='http://alasdeisis.com/OrderImages/HeaderLeft.PNG' /></th><th><img src='http://alasdeisis.com/OrderImages/HeaderRight.png' /></th></tr><tr><th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                        <th><p>C/ PADILLA 57 2º IZQ</p><p>28006 - MADRID</p><p> B - 83549451 </p><p>Tlf.:91- 402 35 78 Fax: 91- 309 28 39 </p></th></tr></thead>    </table>
                        <hr style='color: rgb(0,144,146)'><table width='100%' style='text-align: left'><thead><tr><th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th><th><p>");

            sb.AppendFormat(@"<p> Nombre : {0}</p><p> Dirección :{1}</p><p>Población :{2}</p><p>C.P. :{3}</p><p>País :{4}</p><p> NIE :{5}</p><p>Teléfono:{6} </p></th></p>",
                order.Name, order.Address, order.Population, order.CP, order.Country, order.NIE, order.Phone);

            sb.AppendFormat(@"<th><p>Nº FACTURA :{0}</p><p>Fecha : {1}</p><p> EX 01/19 {2}</p><p>Fecha de salida: {3}</p></th></p></tr></thead></table><hr>",
                DateTime.Now.Year + "/" + order.OrderNumber, order.CreatedDate, order.Country, order.OrderDate);

            sb.AppendFormat(@"<table border='1' style='border:#3b3b3b' width='100%'><thead><tr><th>CONCEPTO</th><th>UNID</th><th>IMPORTE</th><th>DTO.%</th><th>DTO.€</th><th>TOTAL</th>
                              </tr></thead><tbody><tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
                              order.ItemName1, order.Quantity1, order.Price1, order.Discount1, discAmount1, total1);
            sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
                order.ItemName2, order.Quantity2, order.Price2, order.Discount2, discAmount2, total2);
            sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
                order.ItemName3, order.Quantity3, order.Price3, order.Discount3, discAmount3, total3);
            sb.AppendFormat(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
                order.ItemName4, order.Quantity4, order.Price4, order.Discount4, discAmount4, total4);
            sb.AppendFormat(@"<tr><td colspan='3'>{0}</td><td colspan='3'>TOTAL SERVICIOS {1}</td></tr>",
              " ", total+ totalDisc);
            sb.AppendFormat(@"<tr><td colspan='6'>FACTURA EXENTO IVA {0}</td></tr>",
              totalDisc);
            sb.AppendFormat(@"<tr><td colspan='6'><h2>TOTAL FACTURA : </h2>{0}</td></tr>",
               total);
            sb.AppendFormat(@"<tr><td colspan='6'><h2>Forma de pago  : </h2>{0}</td></tr>",
             " " );
            sb.AppendFormat(@"<tr><td colspan='6'><h2>BBVA : </h2>{0}</td></tr>",
             " ");
            sb.AppendFormat(@"<tr><td colspan='6'><h2>Observaciones : </h2>{0}</td></tr></table>",
             " ");
            sb.AppendFormat(@"Cancelaciones y/o anulaciones dentro de las 72 horas previas al vuelo conllevan la pérdida del 100% del importe satisfecho. salvo si tienesseguro de anulacin rembolso 100 %");
            sb.AppendFormat(@"<table width='50%' border='1'><thead><tr><th>Firma y sello de la Agenci</th></tr></thead><tbody><tr><td> </td></tr></tbody> <tfoot><tr><td> </td></tr></tfoot><table>");
                sb.Append(@"</table></body></html>");

            return sb.ToString();
        }
    }
}