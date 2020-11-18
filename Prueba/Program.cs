using System;
using WsLib;
using WsLib.co.com.acesco.erpqa;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            string user = "webservice";
            string password = "prcwndws";
            Api api = new Api(user, password);
            try
            {
                Console.WriteLine("---Metodo Obtener Cliente---");
                var cliente = api.ZBAPI_CLIENTE("1000", "9006029564");
                Console.WriteLine("{0} - {1}",cliente.NAME, cliente.TEXT30);
                Console.WriteLine("\n---Egresos---");
                ZONBASE_EGRESOS_PROVEEDOR[] it_proveedor =
                {
                    new ZONBASE_EGRESOS_PROVEEDOR { NAME = "", LIFNR = "", STCD1 = "" },
                    new ZONBASE_EGRESOS_PROVEEDOR { NAME = "", LIFNR = "", STCD1 = "" },
                    new ZONBASE_EGRESOS_PROVEEDOR { NAME = "", LIFNR = "", STCD1 = "" }
                };
                ZONBASE_EGRESOS_LOTE[] it_lote =
                {
                    new ZONBASE_EGRESOS_LOTE{ LAUFI = "" },
                    new ZONBASE_EGRESOS_LOTE{ LAUFI = "" },
                    new ZONBASE_EGRESOS_LOTE{ LAUFI = "" }
                };
                ZONBASE_EGRESOS_FACTURAS[] it_facturas =
                {
                    new ZONBASE_EGRESOS_FACTURAS{ XBLNR = ""},
                    new ZONBASE_EGRESOS_FACTURAS{ XBLNR = ""},
                    new ZONBASE_EGRESOS_FACTURAS{ XBLNR = ""},
                    new ZONBASE_EGRESOS_FACTURAS{ XBLNR = ""}
                };
                ZONBASE_EGRESOS_EGRESOS[] it_egresos =
                {
                    new ZONBASE_EGRESOS_EGRESOS { LAUFD = "", VBLNR = "" },
                    new ZONBASE_EGRESOS_EGRESOS { LAUFD = "", VBLNR = "" },
                    new ZONBASE_EGRESOS_EGRESOS { LAUFD = "", VBLNR = "" }
                };
                var egresos = api.ZBAPI_EGRESO("1000", "9006029564", it_proveedor, it_lote, it_facturas, it_egresos);
                Console.WriteLine(egresos.IT_EGRESOS.Length);
                Console.WriteLine("\n---Orden Compra---");
                var ordenCompra = api.ZBAPI_ORDENESCOMPRA("", "");
                Console.WriteLine(ordenCompra.USUARIO);
                Console.WriteLine("\n--- Proveedor ---");
                var proveedores = api.ZBAPI_PROVEEDOR("", "");
                Console.WriteLine(proveedores.NAME1);
                Console.WriteLine("\n--- Recibo de Caja ---");
                var recibocaja = api.ZBAPI_RECIBOCAJA("1600023047", "1000");
                Console.WriteLine(recibocaja.NAME1);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
