using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using WsLib.co.com.acesco.erpqa;

namespace WsLib
{
    public class Api
    {
        public string user { get; set; }
        public string password { get; set; }

        public Api(string user, string password)
        {
            this.user = user;
            this.password = password;
        }

        private ZWS_ONBASE Conexion()
        {
            ZWS_ONBASE zWS_ONBASE = new ZWS_ONBASE();
            zWS_ONBASE.Credentials = configurarCredenciales();
            //Buscar algo mas optimo
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            configurarCertificados();
            return zWS_ONBASE;
        }

        private ICredentials configurarCredenciales()
        {
            ICredentials credenciales = new NetworkCredential(user, password);
            return credenciales;
        }

        private void configurarCertificados()
        {
            ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                    return true;   //Is valid

                if (cert.GetCertHashString().ToLower().ToString().EndsWith("?fbcca49d353373d9854325495c5ab5debb6991aa"))
                    return true;   //Is valid

                return true;
            };
        }

        public ZBAPI_CLIENTEResponse ZBAPI_CLIENTE(string bukrs, string stcd1)
        {
            try
            {
                ZBAPI_CLIENTE zbapi_cliente = new ZBAPI_CLIENTE
                {
                    V_BUKRS = bukrs,
                    V_STCD1 = stcd1
                };
                var response = Conexion().ZBAPI_CLIENTE(zbapi_cliente);
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
           
        }

        public ZBAPI_EGRESOResponse ZBAPI_EGRESO(string bukrs, string vblnr, ZONBASE_EGRESOS_PROVEEDOR[] it_proveedor, ZONBASE_EGRESOS_LOTE[] it_lote, ZONBASE_EGRESOS_FACTURAS[] it_facturas, ZONBASE_EGRESOS_EGRESOS[] it_egresos)
        {
            try
            {

                ZBAPI_EGRESO zBAPI_EGRESO = new ZBAPI_EGRESO
                {
                    V_BUKRS = bukrs,
                    V_VBLNR = vblnr,
                    IT_PROVEEDOR = it_proveedor,
                    IT_LOTE = it_lote,
                    IT_FACTURAS = it_facturas,
                    IT_EGRESOS = it_egresos
                };
                var response = Conexion().ZBAPI_EGRESO(zBAPI_EGRESO);
                return response;
            }catch(Exception ex)
            {
                throw ex;
            }            
        }    
        
        public ZBAPI_ORDENESCOMPRAResponse ZBAPI_ORDENESCOMPRA(string ebeln, string bukrs)
        {
            ZBAPI_ORDENESCOMPRA zBAPI_ORDENESCOMPRA = new ZBAPI_ORDENESCOMPRA
            {
                V_EBELN = ebeln,
                V_BUKRS = bukrs
            };
            var response = Conexion().ZBAPI_ORDENESCOMPRA(zBAPI_ORDENESCOMPRA);
            return response;
        }

        public ZBAPI_PROVEEDORResponse ZBAPI_PROVEEDOR(string bukrs, string stcd1)
        {
            ZBAPI_PROVEEDOR zBAPI_PROVEEDOR = new ZBAPI_PROVEEDOR
            {
                V_BUKRS = bukrs,
                V_STCD1 = stcd1
            };
            var response = Conexion().ZBAPI_PROVEEDOR(zBAPI_PROVEEDOR);
            return response;
        }

        public ZBAPI_RECIBOCAJAResponse ZBAPI_RECIBOCAJA(string belnr, string bukrs)
        {
            ZBAPI_RECIBOCAJA zBAPI_RECIBOCAJA = new ZBAPI_RECIBOCAJA
            {
                V_BELNR = belnr,
                V_BUKRS = bukrs
            };
            var response = Conexion().ZBAPI_RECIBOCAJA(zBAPI_RECIBOCAJA);
            return response;
        }
    }
}
