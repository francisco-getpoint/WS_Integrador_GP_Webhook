using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Newtonsoft.Json;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static WS_itec2.Service1;

namespace WS_itec2
{
    public partial class Service1 : ServiceBase
    {
        private Timer tmServicio1 = null;

        #region ClasesParaEjecutarAPIs
        //confirmacion SDR ------------------------------------
        [DataContract]
        public partial class Cab_Confirmacion_SDR
        {
            [DataMember(Order = 1)]
            public long count { get; set; }

            [DataMember(Order = 2)]
            public bool resultado { get; set; }

            [DataMember(Order = 3)]
            public string resultado_descripcion { get; set; }

            [DataMember(Order = 4)]
            public long resultado_codigo { get; set; }

            [DataMember(Order = 5)]
            public long limit { get; set; }

            [DataMember(Order = 6)]
            public long rowset { get; set; }

            [DataMember(Order = 99)]
            public List<Cab2_Confirmacion_SDR> cabeceras = new List<Cab2_Confirmacion_SDR>();
        }

        [DataContract]
        public partial class Cab2_Confirmacion_SDR
        {
            [DataMember(Order = 1)]
            public int Id { get; set; }

            [DataMember(Order = 2)]
            public Nullable<int> RecepcionId { get; set; }

            [DataMember(Order = 3)]
            public string INT_NAME { get; set; }

            [DataMember(Order = 4)]
            public string FECHA_HORA { get; set; }

            [DataMember(Order = 5)]
            public long SolRecepId { get; set; }

            [DataMember(Order = 6)]
            public string FechaProceso { get; set; }

            [DataMember(Order = 7)]
            public string TipoDocumento { get; set; }

            [DataMember(Order = 8)]
            public string NumeroDocto { get; set; }

            [DataMember(Order = 9)]
            public string FechaDocto { get; set; }

            [DataMember(Order = 10)]
            public string TipoReferencia { get; set; }

            [DataMember(Order = 11)]
            public string NumeroReferencia { get; set; }

            [DataMember(Order = 12)]
            public string FechaReferencia { get; set; }

            [DataMember(Order = 13)]
            public string RutProveedor { get; set; }

            [DataMember(Order = 14)]
            public string GlosaRdm { get; set; }

            [DataMember(Order = 15)]
            public int TipoSolicitud { get; set; }

            [DataMember(Order = 16)]
            public string Dato1 { get; set; }

            [DataMember(Order = 17)]
            public string Dato2 { get; set; }

            [DataMember(Order = 18)]
            public string Dato3 { get; set; }


            [DataMember(Order = 99)]
            public List<Det_Confirmacion_SDR> items = new List<Det_Confirmacion_SDR>();
        }

        [DataContract]
        public partial class Det_Confirmacion_SDR
        {
            [DataMember(Order = 1)]
            public int Linea { get; set; }

            [DataMember(Order = 2)]
            public string CodigoArticulo { get; set; }

            [DataMember(Order = 3)]
            public string UnidadCompra { get; set; }

            [DataMember(Order = 4)]
            public decimal CantidadSolicitada { get; set; }

            [DataMember(Order = 5)]
            public decimal CantidadRecibida { get; set; }

            [DataMember(Order = 6)]
            public decimal DiferenciaRecepcion { get; set; }

            [DataMember(Order = 7)]
            public int ItemReferencia { get; set; }

            [DataMember(Order = 8)]
            public string LoteSerie { get; set; }

            [DataMember(Order = 9)]
            public string FechaVencto { get; set; }

            [DataMember(Order = 10)]
            public long HuId { get; set; }

            [DataMember(Order = 11)]
            public int Estado { get; set; }

            [DataMember(Order = 12)]
            public string Dato1 { get; set; }

            [DataMember(Order = 13)]
            public string Dato2 { get; set; }

            [DataMember(Order = 14)]
            public string Dato3 { get; set; }

            [DataMember(Order = 15)]
            public string Fecha1Det { get; set; }
        }

        //confirmacion SDD ------------------------------------
        [DataContract]
        public partial class Cab_Confirmacion_SDD
        {
            [DataMember(Order = 1)]
            public long count { get; set; }

            [DataMember(Order = 2)]
            public bool resultado { get; set; }

            [DataMember(Order = 3)]
            public string resultado_descripcion { get; set; }

            [DataMember(Order = 4)]
            public long resultado_codigo { get; set; }

            [DataMember(Order = 5)]
            public long limit { get; set; }

            [DataMember(Order = 6)]
            public long rowset { get; set; }

            [DataMember(Order = 99)]
            public List<Cab2_Confirmacion_SDD> cabeceras = new List<Cab2_Confirmacion_SDD>();
        }

        [DataContract]
        public partial class Cab2_Confirmacion_SDD
        {
            [DataMember(Order = 1)]
            public int Id { get; set; }

            [DataMember(Order = 2)]
            public Nullable<int> ColaPickId { get; set; }

            [DataMember(Order = 3)]
            public string INT_NAME { get; set; }

            [DataMember(Order = 4)]
            public string FECHA_HORA { get; set; }

            [DataMember(Order = 5)]
            public long SolDespId { get; set; }

            [DataMember(Order = 6)]
            public string FechaProceso { get; set; }

            [DataMember(Order = 7)]
            public int TipoDocumento { get; set; }

            [DataMember(Order = 8)]
            public string NumeroDocto { get; set; }

            [DataMember(Order = 9)]
            public string FechaDocto { get; set; }

            [DataMember(Order = 10)]
            public string TipoReferencia { get; set; }

            [DataMember(Order = 11)]
            public string NumeroReferencia { get; set; }

            [DataMember(Order = 12)]
            public string FechaReferencia { get; set; }

            [DataMember(Order = 13)]
            public string RutCliente { get; set; }

            [DataMember(Order = 14)]
            public int TipoSolicitud { get; set; }

            [DataMember(Order = 15)]
            public string Dato1 { get; set; }

            [DataMember(Order = 16)]
            public string Dato2 { get; set; }

            [DataMember(Order = 17)]
            public string Dato3 { get; set; }

            [DataMember(Order = 99)]
            public List<Det_Confirmacion_SDD> Items = new List<Det_Confirmacion_SDD>();
        }

        [DataContract]
        public partial class Det_Confirmacion_SDD
        {
            [DataMember(Order = 1)]
            public int Linea { get; set; }

            [DataMember(Order = 2)]
            public string CodigoArticulo { get; set; }

            [DataMember(Order = 3)]
            public string UnidadVenta { get; set; }

            [DataMember(Order = 4)]
            public decimal CantidadSolicitada { get; set; }

            [DataMember(Order = 5)]
            public int ItemReferencia { get; set; }

            [DataMember(Order = 6)]
            public string LoteSerieSol { get; set; }

            [DataMember(Order = 7)]
            public string FecVenctoSol { get; set; }

            [DataMember(Order = 8)]
            public decimal CantidadDespachada { get; set; }

            [DataMember(Order = 9)]
            public string LoteSerieDesp { get; set; }

            [DataMember(Order = 10)]
            public string FecVenctoDesp { get; set; }

            [DataMember(Order = 11)]
            public int Estado { get; set; }

            [DataMember(Order = 12)]
            public string Dato1 { get; set; }

            [DataMember(Order = 13)]
            public string Dato2 { get; set; }

            [DataMember(Order = 14)]
            public string Dato3 { get; set; }
        }

        // Alerta ---------------------------------------------
        [DataContract]
        public partial class Alerta
        {
            [DataMember(Order = 1)]
            public string app_id { get; set; }

            [DataMember(Order = 2)]
            public string[] include_external_user_ids { get; set; }

            [DataMember(Order = 3)]
            public string channel_for_external_user_ids { get; set; }

            [DataMember(Order = 4)]

            public det_data data = new det_data();

            [DataMember(Order = 5)]

            public det_headings headings = new det_headings();

            [DataMember(Order = 6)]

            public det_contents contents = new det_contents();
        }

        [DataContract]
        public partial class det_data
        {
            [DataMember(Order = 1)]
            public string foo { get; set; }
        }

        [DataContract]
        public partial class det_headings
        {
            [DataMember(Order = 1)]
            public string en { get; set; }
        }

        [DataContract]
        public partial class det_contents
        {
            [DataMember(Order = 1)]
            public string en { get; set; }
        }

        //Cantidades faltantes despacho ----------------------
        [DataContract]
        public partial class Cab_CantidadFaltanteDespacho
        {
            [DataMember(Order = 1)]
            public long count { get; set; }

            [DataMember(Order = 2)]
            public bool resultado { get; set; }

            [DataMember(Order = 3)]
            public string resultado_descripcion { get; set; }

            [DataMember(Order = 4)]
            public long resultado_codigo { get; set; }

            [DataMember(Order = 5)]
            public long limit { get; set; }

            [DataMember(Order = 6)]
            public long rowset { get; set; }

            [DataMember(Order = 99)]
            public List<Cab2_CantidadFaltanteDespacho> cabeceras = new List<Cab2_CantidadFaltanteDespacho>();
        }

        [DataContract]
        public partial class Cab2_CantidadFaltanteDespacho
        {
            [DataMember(Order = 1)]
            public int Id { get; set; }

            [DataMember(Order = 2)]
            public Nullable<int> ColaPickId { get; set; }

            [DataMember(Order = 3)]
            public string INT_NAME { get; set; }

            [DataMember(Order = 4)]
            public string FECHA_HORA { get; set; }

            [DataMember(Order = 5)]
            public long SolDespId { get; set; }

            [DataMember(Order = 6)]
            public string FechaProceso { get; set; }

            [DataMember(Order = 7)]
            public int TipoDocumento { get; set; }

            [DataMember(Order = 8)]
            public string NumeroDocto { get; set; }

            [DataMember(Order = 9)]
            public string FechaDocto { get; set; }

            [DataMember(Order = 10)]
            public string TipoReferencia { get; set; }

            [DataMember(Order = 11)]
            public string NumeroReferencia { get; set; }

            [DataMember(Order = 12)]
            public string FechaReferencia { get; set; }

            [DataMember(Order = 13)]
            public string RutCliente { get; set; }

            [DataMember(Order = 14)]
            public int TipoSolicitud { get; set; }

            [DataMember(Order = 99)]
            public List<Det_CantidadFaltanteDespacho> Items = new List<Det_CantidadFaltanteDespacho>();
        }

        [DataContract]
        public partial class Det_CantidadFaltanteDespacho
        {
            [DataMember(Order = 1)]
            public int Linea { get; set; }

            [DataMember(Order = 2)]
            public string CodigoArticulo { get; set; }

            [DataMember(Order = 3)]
            public string UnidadVenta { get; set; }

            [DataMember(Order = 4)]
            public int ItemReferencia { get; set; }

            [DataMember(Order = 5)]
            public string LoteSerieSol { get; set; }

            [DataMember(Order = 6)]
            public string FecVenctoSol { get; set; }

            [DataMember(Order = 7)]
            public decimal CantidadDespachada { get; set; }

            [DataMember(Order = 8)]
            public decimal CantidadSolicitada { get; set; }

            [DataMember(Order = 9)]
            public decimal CantidadPendiente { get; set; }

            [DataMember(Order = 10)]
            public string LoteSerieDesp { get; set; }

            [DataMember(Order = 11)]
            public string FecVectoDesp { get; set; }

            [DataMember(Order = 12)]
            public int Estado { get; set; }
        }

        //Ajuste Entrada / Salida ----------------------
        //[DataContract]
        //public partial class Cab_Ajuste
        //{
        //    [DataMember(Order = 1)]
        //    public long count { get; set; }

        //    [DataMember(Order = 2)]
        //    public bool resultado { get; set; }

        //    [DataMember(Order = 3)]
        //    public string resultado_descripcion { get; set; }

        //    [DataMember(Order = 4)]
        //    public long resultado_codigo { get; set; }

        //    [DataMember(Order = 5)]
        //    public long limit { get; set; }

        //    [DataMember(Order = 6)]
        //    public long rowset { get; set; }

        //    [DataMember(Order = 99)]
        //    public List<Cab2_Ajuste> cabeceras = new List<Cab2_Ajuste>();
        //}

        //[DataContract]
        //public partial class Cab2_Ajuste
        //{
        //    [DataMember(Order = 1)]
        //    public int Empid { get; set; }

        //    [DataMember(Order = 2)]
        //    public int TipoTransaccion { get; set; }

        //    [DataMember(Order = 3)]
        //    public string FechaProceso { get; set; }

        //    [DataMember(Order = 4)]
        //    public int CodigoBodega { get; set; }

        //    [DataMember(Order = 5)]
        //    public string TipoReferencia { get; set; }

        //    [DataMember(Order = 6)]
        //    public string NumeroReferencia { get; set; }

        //    [DataMember(Order = 7)]
        //    public string Glosa { get; set; }

        //    [DataMember(Order = 99)]
        //    public List<Det_Ajuste> Items = new List<Det_Ajuste>();
        //}

        //[DataContract]
        //public partial class Det_Ajuste
        //{
        //    [DataMember(Order = 1)]
        //    public int Linea { get; set; }

        //    [DataMember(Order = 2)]
        //    public string CodigoArticulo { get; set; }

        //    [DataMember(Order = 3)]
        //    public string UnidadMedida { get; set; }

        //    [DataMember(Order = 4)]
        //    public decimal Cantidad { get; set; }
        //}

        [DataContract]
        public partial class Cab_Ajuste2
        {
            [DataMember(Order = 1)]
            public int Empid { get; set; }

            [DataMember(Order = 2)]
            public int TipoTransaccion { get; set; }

            [DataMember(Order = 3)]
            public string FechaProceso { get; set; }

            [DataMember(Order = 4)]
            public int CodigoBodega { get; set; }

            [DataMember(Order = 5)]
            public string TipoReferencia { get; set; }

            [DataMember(Order = 6)]
            public string NumeroReferencia { get; set; }

            [DataMember(Order = 7)]
            public string Glosa { get; set; }

            [DataMember(Order = 99)]
            public List<Det_Ajuste2> Items = new List<Det_Ajuste2>();
        }

        [DataContract]
        public partial class Det_Ajuste2
        {
            [DataMember(Order = 1)]
            public int Linea { get; set; }

            [DataMember(Order = 2)]
            public string CodigoArticulo { get; set; }

            [DataMember(Order = 3)]
            public string UnidadMedida { get; set; }

            [DataMember(Order = 4)]
            public decimal Cantidad { get; set; }

            [DataMember(Order = 5)]
            public string Lote { get; set; }

            [DataMember(Order = 6)]
            public string FechaVencimiento { get; set; }

            [DataMember(Order = 7)]
            public int Estado { get; set; }
        }

        [DataContract]
        public partial class Cab_WebhookTracking
        {
            [DataMember(Order = 1)]
            public int SolDespId { get; set; }

            [DataMember(Order = 2)]
            public string TipoReferencia { get; set; }

            [DataMember(Order = 3)]
            public string NumeroReferencia { get; set; }

            [DataMember(Order = 4)]
            public string RutCliente { get; set; }

            [DataMember(Order = 5)]
            public int Estado { get; set; }

            [DataMember(Order = 6)]
            public string EstadoGlosa { get; set; }

            [DataMember(Order = 7)]
            public string FechaEstado { get; set; }

            [DataMember(Order = 8)]
            public string HoraEstado { get; set; }
        }

        [DataContract]
        public partial class Cab_AnulaPedido
        {
            [DataMember(Order = 1)]
            public int tipo_solicitud { get; set; }

            [DataMember(Order = 2)]
            public string referencia { get; set; }

            [DataMember(Order = 3)]
            public int odp_wms { get; set; }
        }

        //Pedido DRIVIN ----------------------
        [DataContract]
        public partial class PedidoDrivin_Cab
        {
            [DataMember(Order = 1)]

            public List<PedidoDrivin_clients> clients = new List<PedidoDrivin_clients>();
        }

        [DataContract]
        public partial class PedidoDrivin_clients
        {
            [DataMember(Order = 1)]
            public string code { get; set; }

            [DataMember(Order = 2)]
            public string address { get; set; }

            [DataMember(Order = 3)]
            public string reference { get; set; }

            [DataMember(Order = 4)]
            public string city { get; set; }

            [DataMember(Order = 5)]
            public string country { get; set; }

            [DataMember(Order = 6)]
            public string lat { get; set; }

            [DataMember(Order = 7)]
            public string lng { get; set; }

            [DataMember(Order = 8)]
            public string name { get; set; }

            [DataMember(Order = 9)]
            public string client_name { get; set; }

            [DataMember(Order = 10)]
            public string client_code { get; set; }

            [DataMember(Order = 11)]
            public string address_type { get; set; }

            [DataMember(Order = 12)]
            public string contact_name { get; set; }

            [DataMember(Order = 13)]
            public string contact_phone { get; set; }

            [DataMember(Order = 14)]
            public string contact_email { get; set; }

            [DataMember(Order = 15)]
            public string additional_contact_name { get; set; }

            [DataMember(Order = 16)]
            public string additional_contact_phone { get; set; }

            [DataMember(Order = 17)]
            public string additional_contact_email { get; set; }

            [DataMember(Order = 18)]
            public string start_contact_name { get; set; }

            [DataMember(Order = 19)]
            public string start_contact_phone { get; set; }

            [DataMember(Order = 20)]
            public string start_contact_email { get; set; }

            [DataMember(Order = 21)]
            public string near_contact_name { get; set; }

            [DataMember(Order = 22)]
            public string near_contact_phone { get; set; }

            [DataMember(Order = 23)]
            public string near_contact_email { get; set; }

            [DataMember(Order = 24)]
            public string delivered_contact_name { get; set; }

            [DataMember(Order = 25)]
            public string delivered_contact_phone { get; set; }

            [DataMember(Order = 26)]
            public string delivered_contact_email { get; set; }

            [DataMember(Order = 27)]
            public int service_time { get; set; }

            [DataMember(Order = 28)]
            public string sales_zone_code { get; set; }

            [DataMember(Order = 29)]

            public List<PedidoDrivin_time_windows> time_windows = new List<PedidoDrivin_time_windows>();

            [DataMember(Order = 30)]
            public string tags { get; set; }

            [DataMember(Order = 31)]

            public List<PedidoDrivin_orders> orders = new List<PedidoDrivin_orders>();
        }

        [DataContract]
        public partial class PedidoDrivin_time_windows
        {
            [DataMember(Order = 1)]
            public string start { get; set; }

            [DataMember(Order = 2)]
            public string end { get; set; }
        }

        [DataContract]
        public partial class PedidoDrivin_orders
        {
            [DataMember(Order = 1)]
            public string code { get; set; }

            [DataMember(Order = 2)]
            public string alt_code { get; set; }

            [DataMember(Order = 3)]
            public string description { get; set; }

            [DataMember(Order = 4)]
            public string category { get; set; }

            [DataMember(Order = 5)]
            public int units_1 { get; set; }

            [DataMember(Order = 6)]
            public int units_2 { get; set; }

            [DataMember(Order = 7)]
            public int units_3 { get; set; }

            [DataMember(Order = 8)]
            public int position { get; set; }

            [DataMember(Order = 9)]
            public string delivery_date { get; set; }

            [DataMember(Order = 10)]
            public int priority { get; set; }

            [DataMember(Order = 11)]
            public string custom_1 { get; set; }

            [DataMember(Order = 12)]
            public string custom_2 { get; set; }

            [DataMember(Order = 13)]
            public string custom_3 { get; set; }

            [DataMember(Order = 14)]
            public string custom_4 { get; set; }

            [DataMember(Order = 15)]
            public string custom_5 { get; set; }

            [DataMember(Order = 16)]
            public string supplier_code { get; set; }

            [DataMember(Order = 17)]
            public string supplier_name { get; set; }

            [DataMember(Order = 18)]
            public string deploy_date { get; set; }

            [DataMember(Order = 19)]

            public List<PedidoDrivin_items> items = new List<PedidoDrivin_items>();

            [DataMember(Order = 20)]
            public List<PedidoDrivin_pickups> pickups { get; set; } //= new List<PedidoDrivin_pickups>();
        }

        [DataContract]
        public partial class PedidoDrivin_items
        {
            [DataMember(Order = 1)]
            public string code { get; set; }

            [DataMember(Order = 2)]
            public string description { get; set; }

            [DataMember(Order = 3)]
            public int units { get; set; }

            [DataMember(Order = 4)]
            public int units_1 { get; set; }

            [DataMember(Order = 5)]
            public int units_2 { get; set; }

            [DataMember(Order = 6)]
            public int units_3 { get; set; }
        }

        [DataContract]
        public partial class PedidoDrivin_pickups
        {
            [DataMember(Order = 1)]
            public string code { get; set; }

            [DataMember(Order = 2)]
            public string description { get; set; }
        }

        #endregion

        public Service1()
        {
            this.InitializeComponent();
            this.tmServicio1 = new Timer(double.Parse(ConfigurationManager.AppSettings["Timer"]));
            this.tmServicio1.Elapsed += new ElapsedEventHandler(this.tmServicio1_Elapsed);
        }
        protected override void OnStart(string[] args)
        {
            this.tmServicio1.Start();
        }
        protected override void OnStop()
        {
            this.tmServicio1.Stop();
        }

        //Evento principal que genera todos los procesos ------------------
        private void tmServicio1_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.tmServicio1.Stop();
                LogInfo("tmServicio1_Elapsed", "Inicio", true);

                if (ConfigurationManager.AppSettings["Activa_CONFIRMA_RECEPCION"].ToString() == "True")
                {
                    //1: WebHook extrae Confirmacion de Recepciones 
                    ConfirmacionIngreso("CONFIRMA_RECEPCION");
                }

                if (ConfigurationManager.AppSettings["Activa_CONFIRMA_DESPACHO"].ToString() == "True")
                {
                    //2: WebHook extrae Confirmacion de Despachos
                    ConfirmacionSalida("CONFIRMA_DESPACHO");
                }

                if (ConfigurationManager.AppSettings["Activa_CONFIRMA_DESPACHO_Y_FALTANTES"].ToString() == "True")
                {
                    //3: WebHook retorna Confirmacion de Despachos incluye cantidades faltantes
                    CantidadesFaltantesDespacho("CONFIRMA_DESPACHO_Y_FALTANTES");
                }

                if (ConfigurationManager.AppSettings["Activa_WebhookAlerta"].ToString() == "True")
                {
                    //4: WebHook envias mensajes de alerta
                    WebhookAlerta("ENVIO-ALERTA-APP"); 
                }

                if (ConfigurationManager.AppSettings["Activa_WebhookTracking"].ToString() == "True")
                {
                    //5: WebHook indica los cambios de estado de la SDD
                    WebhookTracking("ENVIO-ALERTA-TRACKING");
                }
                
                if (ConfigurationManager.AppSettings["Activa_CONFIRMA_AJUSTE"].ToString() == "True")
                {
                    //6: WebHook envia hacia ERP datos de Ajuste de entrada o salida generado en Getpoint
                    ConfirmacionAjuste("CONFIRMA_AJUSTE");
                }

                if (ConfigurationManager.AppSettings["Activa_ANULA_PEDIDO"].ToString() == "True")
                {
                    //7: WebHook envia hacia ERP datos de Anulacion de un Pedido o Despacho
                    AnulaPedido("ANULA_PEDIDO");
                }

                if (ConfigurationManager.AppSettings["Activa_CREA_PEDIDO_DRIVIN"].ToString() == "True")
                {
                    //8: Llamado API crea pedido en DRIV.IN con los datos de la ODP despachada
                    CreaPedidoDrivin("CREA_PEDIDO_DRIVIN");
                }

                this.tmServicio1.Start();
            }
            catch (Exception ex)
            {
                LogInfo("WS_Integrador_GP_Webhook", "Error en tmServicio1_Elapsed: " + ex.Message.Trim(), true);

                Environment.Exit(1); //reinicia el servicio 
            }
        }

        //=====================================
        // 1 - WEBHOOK CONFIRMACION SDR
        //=====================================
        //      sp_proc_INT_ConfirmaIngresoRDM: procedimiento que inserta los datos para el Webhook de Confirmacion de Recepcion -----  
        //      NombreProceso = CONFIRMA_RECEPCION
        private void ConfirmacionIngreso(string NombreProceso)
        {
            try
            {
                LogInfo("ConfirmacionIngreso", NombreProceso.Trim() + " - Inicio ejecucion", true, false);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae RDM (Recepciones de mercancia) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        Cab_Confirmacion_SDR CabJson = new Cab_Confirmacion_SDR();
                        Cab2_Confirmacion_SDR Cabecera = new Cab2_Confirmacion_SDR();
                        Det_Confirmacion_SDR Detalle = new Det_Confirmacion_SDR();

                        string var_IntId = "";

                        //Recorre la confirmaciones de recepcion --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Cuando cambie de IntId debe cargar la estructura para enviar al Webhook -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                #region Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                var request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //VALIDAR SI TOKEN ES DINAMICO
                                //si token dinamico -> validar fecuencia
                                // si es 1 por llamado genrarlo
                                // si es por minutos calcular rango si debe actualizar o no


                                //si genero token actualizar value en 


                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                //=======================================================================
                                //======================= ESPECIAL COAGRA ================================
                                //=========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("COAGRA")) //si el proceso contiene coagra
                                {
                                    string Token_ = "";
                                    string NombreCookie1 = "", ValorCookie1 = "", NombreCookie2 = "", ValorCookie2 = "";

                                    CookieCOAGRA("COOKIE_TOKEN_COAGRA", ref Token_, ref NombreCookie1, ref ValorCookie1, ref NombreCookie2, ref ValorCookie2);

                                    //Agrega Cookies obtenidas -----
                                    request.AddCookie(NombreCookie1, ValorCookie1);
                                    request.AddCookie(NombreCookie2, ValorCookie2);

                                    //Agrega headers adicional -----
                                    request.AddHeader("x-csrf-token", Token_);

                                }
                                //FIN ============== ESPECIAL PARA COAGRA, PARAMETRIZAR LUEGO =============

                                //======================================================================
                                //======================= ESPECIAL HONDA ================================
                                //========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("HONDA")) //si el proceso contiene coagra
                                {
                                    string Token_ = "";

                                    TokenHONDA("TOKEN_HONDA", ref Token_);

                                    //Agrega headers adicional -----
                                    request.AddHeader("Authorization", Token_);
                                }
                                //FIN ============== ESPECIAL PARA HONDA, PARAMETRIZAR LUEGO =============

                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                CabJson = new Cab_Confirmacion_SDR();
                                Cabecera = new Cab2_Confirmacion_SDR();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda IntId que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //--------------------------------------------
                                Cabecera.Id = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.RecepcionId = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.INT_NAME = NombreProceso.Trim();
                                Cabecera.FECHA_HORA = DateTime.Parse(myData.Tables[0].Rows[i]["FechaEstado"].ToString()).ToString("dd-MM-yyyy HH:mm"); // "30-05-2022 13:35";
                                Cabecera.SolRecepId = long.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString().Trim());
                                Cabecera.FechaProceso = DateTime.Parse(myData.Tables[0].Rows[i]["FechaProceso"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoDocumento = myData.Tables[0].Rows[i]["TipoDocumento"].ToString();
                                Cabecera.NumeroDocto = myData.Tables[0].Rows[i]["NumeroDocto"].ToString();
                                Cabecera.FechaDocto = DateTime.Parse(myData.Tables[0].Rows[i]["FechaDocto"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoReferencia = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                Cabecera.NumeroReferencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                Cabecera.FechaReferencia = DateTime.Parse(myData.Tables[0].Rows[i]["FechaReferencia"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoSolicitud = int.Parse(myData.Tables[0].Rows[i]["TipoSolicitud"].ToString());
                                Cabecera.Dato1 = myData.Tables[0].Rows[i]["Dato1"].ToString();
                                Cabecera.Dato2 = myData.Tables[0].Rows[i]["Dato2"].ToString();
                                Cabecera.Dato3 = myData.Tables[0].Rows[i]["Dato3"].ToString();
                                //--------------------------------------------

                                //Busca detalles relacionados al IntId
                                string CondicionBusqueda = "IntId = " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                DataRow[] resultado = myData.Tables[0].Select(CondicionBusqueda);

                                foreach (DataRow fila in resultado)
                                {
                                    Detalle = new Det_Confirmacion_SDR();

                                    Detalle.Linea = int.Parse(fila["Linea"].ToString()); // 1;
                                    Detalle.CodigoArticulo = fila["CodigoArticulo"].ToString(); // "5";
                                    Detalle.UnidadCompra = fila["UnidadMedida"].ToString(); // "UN";
                                    Detalle.CantidadSolicitada = decimal.Parse(fila["Cantidad"].ToString()); // 150;
                                    Detalle.CantidadRecibida = decimal.Parse(fila["CantidadProc"].ToString()); // 100;
                                    Detalle.DiferenciaRecepcion = decimal.Parse(fila["Diferencia"].ToString()); // 100;

                                    Detalle.ItemReferencia = int.Parse(fila["ItemReferencia"].ToString()); // 0;
                                    Detalle.LoteSerie = fila["NroSerieDesp"].ToString(); // "132561";
                                    Detalle.FechaVencto = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy"); //fila["FechaVectoDesp"].ToString(); 

                                    Detalle.HuId = long.Parse(fila["Texto1"].ToString()); // 30025;
                                    Detalle.Estado = int.Parse(fila["Texto2"].ToString());

                                    Detalle.Dato1 = fila["Dato1Det"].ToString();
                                    Detalle.Dato2 = fila["Dato2Det"].ToString();
                                    Detalle.Dato3 = fila["Dato3Det"].ToString();

                                    Detalle.Fecha1Det = DateTime.Parse(fila["Fecha1Det"].ToString()).ToString("dd-MM-yyyy");

                                    Cabecera.items.Add(Detalle);
                                }

                                CabJson.cabeceras.Add(Cabecera);

                                //-------------------------------------------------------------
                                //Crea body para llamado con estructura de variable cargada ---
                                var body = JsonConvert.SerializeObject(CabJson);

                                //Guarda JSON que se envia ------------------
                                LogInfo("ConfirmacionIngreso", " JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, body.Trim());

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("ConfirmacionIngreso", NombreProceso.Trim() + " - Ejecuta api NumeroReferencia " + myData.Tables[0].Rows[i]["Folio"].ToString().Trim());

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK, retorna status 200 --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK))
                                {
                                    //====================== ESPECIAL ==========================
                                    // si hay que esperar respuesta del Webhook ----------------
                                    //==========================================================
                                    if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                    {
                                        //Debe venir la siguiente respuesta:
                                        //{
                                        //    "Resultado": "OK",
                                        //    "Descripcion": "Integracion OK"
                                        //}

                                        LogInfo("ConfirmacionIngreso", "JSON respuesta recibido.",true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(),Cabecera.NumeroReferencia, response.Content.ToString());

                                        JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                        string Resultado;
                                        string Descripcion;

                                        try
                                        {
                                            Resultado = rss["Resultado"].ToString(); //OK - ERROR
                                            Descripcion = rss["Descripcion"].ToString(); //descripcion 
                                        }
                                        catch (Exception ex)
                                        {
                                            Resultado = "ERROR";
                                            Descripcion = "Respuesta no retorna estructura definida (Resultado y Descripcion)";
                                        }

                                        if (Resultado.Trim() == "OK")
                                        {
                                            //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2, 
                                                                                                                                     ""); //Procesado

                                            Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                        " .Resultado: " + Resultado.Trim() +
                                                        " .Descripcion: " + Descripcion.Trim();

                                            LogInfo("ConfirmacionIngreso",Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(),Cabecera.NumeroReferencia);
                                        }
                                        else
                                        {
                                            //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     3, 
                                                                                                                                     ""); //Procesado con error

                                            Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                        " .Resultado: " + Resultado.Trim() +
                                                        " .Descripcion: " + Descripcion.Trim();

                                            LogInfo("ConfirmacionIngreso",Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(),Cabecera.NumeroReferencia);
                                        }

                                        //Guarda respuesta en Dato2 RDM procesada -------------
                                        result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                    EmpIdGlobal,
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                    "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());
                                    } //FIN si hay que esperar respuesta del Webhook ================
                                    else
                                    {
                                        //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 2, 
                                                                                                                                 ""); //Procesado

                                        Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo("ConfirmacionIngreso", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta);
                                    }
                                }
                                else
                                {
                                    //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3, 
                                                                                                                             ""); //Procesado con error

                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo("ConfirmacionIngreso", Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                }
                            } //FIN si cambia de IntId

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo("ConfirmacionIngreso", "Error: " + ex.Message.Trim(), true, true, NombreProceso.Trim());
            }
        }

        //=====================================
        // 2 - WEBHOOK CONFIRMACION SDD
        //=====================================
        //      sp_proc_INT_ConfirmacionODP: procedimiento que inserta los datos para el Webhook de Confirmacion de Despachos -----  
        //      NombreProceso = CONFIRMA_DESPACHO

        private void ConfirmacionSalida(string NombreProceso)
        {
            try
            {
                LogInfo(NombreProceso, NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal = 0;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae ODP (Olas de Picking) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        Cab_Confirmacion_SDD CabJson = new Cab_Confirmacion_SDD();
                        Cab2_Confirmacion_SDD Cabecera = new Cab2_Confirmacion_SDD();
                        Det_Confirmacion_SDD Detalle = new Det_Confirmacion_SDD();

                        var client = new RestClient();
                        var request = new RestRequest(Method.GET);
                        DataSet dsHeaders = new DataSet();
                        string body = "";

                        string ListaIdProcesados = "";

                        string var_IntId = "";
                        string var_Folio = "";
                        string Token_ = "";
                        string NombreCookie1 = "";
                        string ValorCookie1 = "";
                        string NombreCookie2 = "";
                        string ValorCookie2 = "";
                        int i = 0;

                        //Recorre la confirmaciones de salida --------------
                        for (i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            // Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------
                            if (ConfigurationManager.AppSettings["ConfirmacionMultiplePorODP"].ToString() == "True")
                            {
                                //Cuando cambie de ODP debe cargar la estructura con la API y no sea el primer registro -----
                                if (myData.Tables[0].Rows[i]["Folio"].ToString().Trim() != var_Folio && var_Folio != "")
                                {
                                    //Crea body para llamado con estructura de variable cargada ---
                                    body = JsonConvert.SerializeObject(CabJson);

                                    //Guarda JSON que se envia ------------------
                                    LogInfo(NombreProceso, "JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, body.Trim());

                                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                                    //EJECUTA LLAMADO API ---------------------------
                                    IRestResponse response = client.Execute(request);

                                    LogInfo(NombreProceso, myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true);

                                    HttpStatusCode CodigoRetorno = response.StatusCode;
                                    //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                    string Respuesta = "";

                                    //Si finalizó OK --------------------------
                                    if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                    {
                                        //====================== ESPECIAL ==========================
                                        // si hay que esperar respuesta del Webhook ----------------
                                        //==========================================================
                                        if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                        {
                                            //Debe venir la siguiente respuesta:
                                            //{
                                            //    "Resultado": "OK",
                                            //    "Descripcion": "Integracion OK"
                                            //}

                                            LogInfo(NombreProceso, "JSON respuesta recibido", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, response.Content.ToString());

                                            JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                            string Resultado;
                                            string Descripcion;

                                            try
                                            {
                                                Resultado = rss["Resultado"].ToString(); //OK - ERROR
                                                Descripcion = rss["Descripcion"].ToString(); //descripcion 
                                            }
                                            catch (Exception ex)
                                            {
                                                Resultado = "ERROR";
                                                Descripcion = "Respuesta no retorna estructura definida (Resultado y Descripcion)";
                                            }

                                            if (Resultado.Trim() == "OK")
                                            {
                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         2,
                                                                                                                                         ListaIdProcesados.Trim()); //Procesado

                                                Respuesta = "Integracion OK" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                            }
                                            else
                                            {
                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         3,
                                                                                                                                         ListaIdProcesados.Trim()); //Procesado con error

                                                Respuesta = "Error" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                            }

                                            //Guarda respuesta en Dato2 ODP procesada -------------
                                            result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                        EmpIdGlobal,
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                        "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());

                                        } //FIN si hay que esperar respuesta del Webhook ================
                                        else
                                        { //llamado estandar, solo esperamos una ejecucion exitosa (status 200 - OK)
                                          //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2, 
                                                                                                                                     ListaIdProcesados.Trim()); //Procesado

                                            Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                        }
                                    }
                                    else //status Error <> 200
                                    {
                                        //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 3, 
                                                                                                                                 ListaIdProcesados.Trim()); //Procesado con error

                                        Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                    }

                                    //Inicializa la estructura principal
                                    CabJson = new Cab_Confirmacion_SDD();

                                    //Inicializa lista de Id procesados
                                    ListaIdProcesados = "";
                                }

                            } // FIN Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------

                            //Cuando cambie de Id Interno L_IntegraConfirmaciones carga estructura con la API -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API del cliente para enviar la confirmacion de la SDD --------------------
                                #region Carga URL de la API del cliente para enviar la confirmacion de la SDD 

                                client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //Trae informacion para headers segun el nombre proceso -------
                                dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                    EmpId,
                                                                                                                    myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                    2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                //=======================================================================
                                //======================= ESPECIAL COAGRA ===========================
                                //=========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("COAGRA")) //si el proceso contiene coagra debe rescatar el TOKEN para Coagra 
                                {
                                    Token_ = "";
                                    NombreCookie1 = "";
                                    ValorCookie1 = "";
                                    NombreCookie2 = "";
                                    ValorCookie2 = "";

                                    CookieCOAGRA("COOKIE_TOKEN_COAGRA", ref Token_, ref NombreCookie1, ref ValorCookie1, ref NombreCookie2, ref ValorCookie2);

                                    //Agrega Cookies obtenidas -----
                                    request.AddCookie(NombreCookie1, ValorCookie1);
                                    request.AddCookie(NombreCookie2, ValorCookie2);

                                    //Agrega headers adicional -----
                                    request.AddHeader("x-csrf-token", Token_);
                                }
                                //FIN ============== ESPECIAL PARA COAGRA, PARAMETRIZAR LUEGO =============

                                //======================================================================
                                //======================= ESPECIAL HONDA ================================
                                //========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("HONDA")) //si el proceso contiene coagra
                                {
                                    Token_ = "";

                                    TokenHONDA("TOKEN_HONDA", ref Token_);

                                    //Agrega headers adicional -----
                                    request.AddHeader("Authorization", Token_);
                                }
                                //FIN ============== ESPECIAL PARA HONDA, PARAMETRIZAR LUEGO =============

                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                //CabJson = new Cab_Confirmacion_SDD();

                                Cabecera = new Cab2_Confirmacion_SDD();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda documento referencia que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //Crea lista de Id internos procesados, se usa en confirmacion multiple para marcarlos todos a la vez con OK o ERROR
                                ListaIdProcesados = ListaIdProcesados.Trim() + var_IntId.Trim() + ";";

                                //Guarda ODP que esta procesando ---------
                                var_Folio = myData.Tables[0].Rows[i]["Folio"].ToString().Trim();

                                //--------------------------------------------
                                Cabecera.Id = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.ColaPickId = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.INT_NAME = NombreProceso.Trim();
                                Cabecera.FECHA_HORA = DateTime.Parse(myData.Tables[0].Rows[i]["FechaEstado"].ToString()).ToString("dd-MM-yyyy HH:mm"); // "30-05-2022 13:35";
                                Cabecera.SolDespId = long.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString().Trim());
                                Cabecera.FechaProceso = DateTime.Parse(myData.Tables[0].Rows[i]["FechaProceso"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoDocumento = int.Parse(myData.Tables[0].Rows[i]["TipoDocumento"].ToString());
                                Cabecera.NumeroDocto = myData.Tables[0].Rows[i]["NumeroDocto"].ToString();
                                Cabecera.FechaDocto = DateTime.Parse(myData.Tables[0].Rows[i]["FechaDocto"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoReferencia = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                Cabecera.NumeroReferencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                Cabecera.FechaReferencia = DateTime.Parse(myData.Tables[0].Rows[i]["FechaReferencia"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.RutCliente = myData.Tables[0].Rows[i]["RutCliente"].ToString();
                                Cabecera.TipoSolicitud = int.Parse(myData.Tables[0].Rows[i]["TipoSolicitud"].ToString());
                                Cabecera.Dato1 = myData.Tables[0].Rows[i]["Dato1"].ToString();
                                Cabecera.Dato2 = myData.Tables[0].Rows[i]["Dato2"].ToString();
                                Cabecera.Dato3 = myData.Tables[0].Rows[i]["Dato3"].ToString();

                                //Busca los detalles relacionados y los agrega a la cabecera
                                string CondicionBusqueda = "IntId = " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                DataRow[] resultado = myData.Tables[0].Select(CondicionBusqueda);

                                foreach (DataRow fila in resultado)
                                {
                                    Detalle = new Det_Confirmacion_SDD();

                                    Detalle.Linea = int.Parse(fila["Linea"].ToString()); // 1;
                                    Detalle.CodigoArticulo = fila["CodigoArticulo"].ToString(); // "5";
                                    Detalle.UnidadVenta = fila["UnidadMedida"].ToString(); // "UN";
                                    Detalle.CantidadSolicitada = decimal.Parse(fila["Cantidad"].ToString()); // 150;
                                    Detalle.ItemReferencia = int.Parse(fila["ItemReferencia"].ToString()); // 0;
                                    Detalle.FecVenctoSol = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    Detalle.CantidadDespachada = decimal.Parse(fila["CantidadProc"].ToString()); // 100;
                                    Detalle.LoteSerieDesp = fila["NroSerieDesp"].ToString(); // "132561";
                                    Detalle.FecVenctoDesp = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    Detalle.Dato1 = fila["Dato1Det"].ToString();
                                    Detalle.Dato2 = fila["Dato2Det"].ToString();
                                    Detalle.Dato3 = fila["Dato3Det"].ToString();

                                    Cabecera.Items.Add(Detalle);
                                }

                                CabJson.cabeceras.Add(Cabecera);

                                // Si genera confirmaciones individuales ------------------------------------------------
                                if (ConfigurationManager.AppSettings["ConfirmacionMultiplePorODP"].ToString() == "False")
                                {
                                    //Crea body para llamado con estructura de variable cargada ---
                                    body = JsonConvert.SerializeObject(CabJson);

                                    //Guarda JSON que se envia ------------------
                                    LogInfo(NombreProceso, "JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, body.Trim());

                                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                                    //EJECUTA LLAMADO API ---------------------------
                                    IRestResponse response = client.Execute(request);

                                    LogInfo(NombreProceso, myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true);

                                    HttpStatusCode CodigoRetorno = response.StatusCode;
                                    //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                    string Respuesta = "";

                                    //Si finalizó OK --------------------------
                                    if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                    {
                                        //====================== ESPECIAL ==========================
                                        // si hay que esperar respuesta del Webhook ----------------
                                        //==========================================================
                                        if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                        {
                                            //Debe venir la siguiente respuesta:
                                            //{
                                            //    "Resultado": "OK",
                                            //    "Descripcion": "Integracion OK"
                                            //}

                                            LogInfo(NombreProceso, "JSON respuesta recibido", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, response.Content.ToString());

                                            JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                            string Resultado;
                                            string Descripcion;

                                            try
                                            {
                                                Resultado = rss["Resultado"].ToString(); //OK - ERROR
                                                Descripcion = rss["Descripcion"].ToString(); //descripcion 
                                            }
                                            catch (Exception ex)
                                            {
                                                Resultado = "ERROR";
                                                Descripcion = "Respuesta no retorna estructura definida (Resultado y Descripcion)";
                                            }

                                            if (Resultado.Trim() == "OK")
                                            {
                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         2,
                                                                                                                                         ""); //Procesado

                                                Respuesta = "Integracion OK" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                            }
                                            else
                                            {
                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         3,
                                                                                                                                         ""); //Procesado con error

                                                Respuesta = "Error" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                            }

                                            //Guarda respuesta en Dato2 ODP procesada -------------
                                            result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                        EmpIdGlobal,
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                        "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());

                                        } //FIN si hay que esperar respuesta del Webhook ================
                                        else
                                        { //llamado estandar, solo esperamos una ejecucion exitosa (status 200 - OK)
                                          //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2,
                                                                                                                                     ""); //Procesado

                                            Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                        }
                                    }
                                    else //status Error <> 200
                                    {
                                        //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 3,
                                                                                                                                 ""); //Procesado con error

                                        Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                    }

                                    //Inicializa variable principal
                                    CabJson = new Cab_Confirmacion_SDD();

                                } // FIN Si genera confirmaciones individuales ------------------------------------------------

                            } //FIN si cambia de Id integracion

                        } //FIN ciclo recorre Confirmaciones

                        // Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------
                        if (ConfigurationManager.AppSettings["ConfirmacionMultiplePorODP"].ToString() == "True")
                        {
                            //Al finalizar el ciclo envia la confirmacion de la ultima ODP que estaba procesando
                            //Cuando cambie de ODP debe cargar la estructura con la API -----

                            //Ubica el puntero en la ultima fila ---
                            i = myData.Tables[0].Rows.Count - 1;

                            if (var_Folio != "")
                            {
                                //Crea body para llamado con estructura de variable cargada ---
                                body = JsonConvert.SerializeObject(CabJson);

                                //Guarda JSON que se envia ------------------
                                LogInfo(NombreProceso, "JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, body.Trim());

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo(NombreProceso, myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true);

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                {
                                    //====================== ESPECIAL ==========================
                                    // si hay que esperar respuesta del Webhook ----------------
                                    //==========================================================
                                    if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                    {
                                        //Debe venir la siguiente respuesta:
                                        //{
                                        //    "Resultado": "OK",
                                        //    "Descripcion": "Integracion OK"
                                        //}

                                        LogInfo(NombreProceso, "JSON respuesta recibido", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, response.Content.ToString());

                                        JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                        string Resultado;
                                        string Descripcion;

                                        try
                                        {
                                            Resultado = rss["Resultado"].ToString(); //OK - ERROR
                                            Descripcion = rss["Descripcion"].ToString(); //descripcion 
                                        }
                                        catch (Exception ex)
                                        {
                                            Resultado = "ERROR";
                                            Descripcion = "Respuesta no retorna estructura definida (Resultado y Descripcion)";
                                        }

                                        if (Resultado.Trim() == "OK")
                                        {
                                            //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2,
                                                                                                                                     ListaIdProcesados.Trim()); //Procesado

                                            Respuesta = "Integracion OK" +
                                                        ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                        ". Resultado: " + Resultado.Trim() +
                                                        ". Descripcion: " + Descripcion.Trim();

                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                        }
                                        else
                                        {
                                            //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     3,
                                                                                                                                     ListaIdProcesados.Trim()); //Procesado con error

                                            Respuesta = "Error" +
                                                        ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                        ". Resultado: " + Resultado.Trim() +
                                                        ". Descripcion: " + Descripcion.Trim();

                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                        }

                                        //Guarda respuesta en Dato2 ODP procesada -------------
                                        result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                    EmpIdGlobal,
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                    "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());

                                    } //FIN si hay que esperar respuesta del Webhook ================
                                    else
                                    { //llamado estandar, solo esperamos una ejecucion exitosa (status 200 - OK)
                                      //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 2,
                                                                                                                                 ListaIdProcesados.Trim()); //Procesado

                                        Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                    }
                                }
                                else //status Error <> 200
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3,
                                                                                                                             ListaIdProcesados.Trim()); //Procesado con error

                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                }

                                //Inicializa la estructura principal
                                CabJson = new Cab_Confirmacion_SDD();

                                //Inicializa lista de Id procesados
                                ListaIdProcesados = "";
                            }

                        } // FIN Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------

                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo(NombreProceso, "Error: " + ex.Message.Trim(), true, true, NombreProceso.Trim());
            }
        }
        private void ConfirmacionSalida_ORIGINAL(string NombreProceso)
        {
            try
            {
                LogInfo("ConfirmacionSalida", NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae ODP (Olas de Picking) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        Cab_Confirmacion_SDD CabJson = new Cab_Confirmacion_SDD();
                        Cab2_Confirmacion_SDD Cabecera = new Cab2_Confirmacion_SDD();
                        Det_Confirmacion_SDD Detalle = new Det_Confirmacion_SDD();

                        string var_IntId = "";

                        //Recorre la confirmaciones de salida --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Cuando cambie de Folio ODP debe cargar la estructura con la API -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API del cliente para enviar la confirmacion de la SDD --------------------
                                #region Carga URL de la API del cliente para enviar la confirmacion de la SDD 

                                var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                var request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                //=======================================================================
                                //======================= ESPECIAL COAGRA ===========================
                                //=========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("COAGRA")) //si el proceso contiene coagra debe rescatar el TOKEN para Coagra 
                                {
                                    string Token_ = "";
                                    string NombreCookie1 = "", ValorCookie1 = "", NombreCookie2 = "", ValorCookie2 = "";

                                    CookieCOAGRA("COOKIE_TOKEN_COAGRA", ref Token_, ref NombreCookie1, ref ValorCookie1, ref NombreCookie2, ref ValorCookie2);

                                    //Agrega Cookies obtenidas -----
                                    request.AddCookie(NombreCookie1, ValorCookie1);
                                    request.AddCookie(NombreCookie2, ValorCookie2);

                                    //Agrega headers adicional -----
                                    request.AddHeader("x-csrf-token", Token_);
                                }
                                //FIN ============== ESPECIAL PARA COAGRA, PARAMETRIZAR LUEGO =============

                                //======================================================================
                                //======================= ESPECIAL HONDA ================================
                                //========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("HONDA")) //si el proceso contiene coagra
                                {
                                    string Token_ = "";

                                    TokenHONDA("TOKEN_HONDA", ref Token_);

                                    //Agrega headers adicional -----
                                    request.AddHeader("Authorization", Token_);
                                }
                                //FIN ============== ESPECIAL PARA HONDA, PARAMETRIZAR LUEGO =============

                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                CabJson = new Cab_Confirmacion_SDD();
                                Cabecera = new Cab2_Confirmacion_SDD();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda documento referencia que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //--------------------------------------------
                                Cabecera.Id = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.ColaPickId = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.INT_NAME = NombreProceso.Trim();
                                Cabecera.FECHA_HORA = DateTime.Parse(myData.Tables[0].Rows[i]["FechaEstado"].ToString()).ToString("dd-MM-yyyy HH:mm"); // "30-05-2022 13:35";
                                Cabecera.SolDespId = long.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString().Trim());
                                Cabecera.FechaProceso = DateTime.Parse(myData.Tables[0].Rows[i]["FechaProceso"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoDocumento = int.Parse(myData.Tables[0].Rows[i]["TipoDocumento"].ToString());
                                Cabecera.NumeroDocto = myData.Tables[0].Rows[i]["NumeroDocto"].ToString();
                                Cabecera.FechaDocto = DateTime.Parse(myData.Tables[0].Rows[i]["FechaDocto"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoReferencia = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                Cabecera.NumeroReferencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                Cabecera.FechaReferencia = DateTime.Parse(myData.Tables[0].Rows[i]["FechaReferencia"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.RutCliente = myData.Tables[0].Rows[i]["RutCliente"].ToString();
                                Cabecera.TipoSolicitud = int.Parse(myData.Tables[0].Rows[i]["TipoSolicitud"].ToString());
                                Cabecera.Dato1 = myData.Tables[0].Rows[i]["Dato1"].ToString();
                                Cabecera.Dato2 = myData.Tables[0].Rows[i]["Dato2"].ToString();
                                Cabecera.Dato3 = myData.Tables[0].Rows[i]["Dato3"].ToString();

                                //Busca los detalles relacionados y los agrega a la cabecera
                                string CondicionBusqueda = "IntId = " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                DataRow[] resultado = myData.Tables[0].Select(CondicionBusqueda);

                                foreach (DataRow fila in resultado)
                                {
                                    Detalle = new Det_Confirmacion_SDD();

                                    Detalle.Linea = int.Parse(fila["Linea"].ToString()); // 1;
                                    Detalle.CodigoArticulo = fila["CodigoArticulo"].ToString(); // "5";
                                    Detalle.UnidadVenta = fila["UnidadMedida"].ToString(); // "UN";
                                    Detalle.CantidadSolicitada = decimal.Parse(fila["Cantidad"].ToString()); // 150;
                                    Detalle.ItemReferencia = int.Parse(fila["ItemReferencia"].ToString()); // 0;
                                    Detalle.FecVenctoSol = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy"); 
                                    Detalle.CantidadDespachada = decimal.Parse(fila["CantidadProc"].ToString()); // 100;
                                    Detalle.LoteSerieDesp = fila["NroSerieDesp"].ToString(); // "132561";
                                    Detalle.FecVenctoDesp = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    Detalle.Dato1 = fila["Dato1Det"].ToString();
                                    Detalle.Dato2 = fila["Dato2Det"].ToString();
                                    Detalle.Dato3 = fila["Dato3Det"].ToString();

                                    Cabecera.Items.Add(Detalle);
                                }

                                CabJson.cabeceras.Add(Cabecera);

                                //-------------------------------------------------------------
                                //Crea body para llamado con estructura de variable cargada ---
                                var body = JsonConvert.SerializeObject(CabJson);

                                //Guarda JSON que se envia ------------------
                                LogInfo("ConfirmacionSalida","JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, body.Trim());

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("ConfirmacionSalida", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true);

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                {
                                    //====================== ESPECIAL ==========================
                                    // si hay que esperar respuesta del Webhook ----------------
                                    //==========================================================
                                    if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                    {
                                        //Debe venir la siguiente respuesta:
                                        //{
                                        //    "Resultado": "OK",
                                        //    "Descripcion": "Integracion OK"
                                        //}

                                        LogInfo("ConfirmacionSalida","JSON respuesta recibido", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia, response.Content.ToString());

                                        JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                        string Resultado;
                                        string Descripcion;

                                        try
                                        {
                                            Resultado = rss["Resultado"].ToString(); //OK - ERROR
                                            Descripcion = rss["Descripcion"].ToString(); //descripcion 
                                        }
                                        catch (Exception ex)
                                        {
                                            Resultado = "ERROR";
                                            Descripcion = "Respuesta no retorna estructura definida (Resultado y Descripcion)";
                                        }

                                        if (Resultado.Trim() == "OK")
                                        {
                                            //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2,
                                                                                                                                     ""); //Procesado

                                            Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                        " .Resultado: " + Resultado.Trim() +
                                                        " .Descripcion: " + Descripcion.Trim();

                                            LogInfo("ConfirmacionSalida", Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                        }
                                        else
                                        {
                                            //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     3, 
                                                                                                                                     ""); //Procesado con error

                                            Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() + 
                                                        " .Resultado: " + Resultado.Trim() + 
                                                        " .Descripcion: " + Descripcion.Trim();

                                            LogInfo("ConfirmacionSalida", Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                        }

                                        //Guarda respuesta en Dato2 ODP procesada -------------
                                        result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                    EmpIdGlobal,
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                    "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());

                                    } //FIN si hay que esperar respuesta del Webhook ================
                                    else
                                    {
                                        //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 2,
                                                                                                                                 ""); //Procesado

                                        Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo("ConfirmacionSalida", Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                    }
                                }
                                else //status Error <> 200
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3,
                                                                                                                             ""); //Procesado con error

                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo("ConfirmacionSalida", Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.NumeroReferencia);
                                }

                            } //FIN si cambia de Id integracion

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo("ConfirmacionSalida", "Error: " + ex.Message.Trim(), true, true, NombreProceso.Trim());
            }
        }

        //==============================================================================
        // 3 - WEBHOOK Confirmacion de Despachos mas cantidades faltantes en el despacho
        //==============================================================================
        //      sp_proc_INT_ConfirmacionODP_Faltante: procedimiento que inserta los datos para el Webhook de Confirmacion de Despachos mas cantidades faltantes-----  
        private void CantidadesFaltantesDespacho(string NombreProceso)
        {
            try
            {
                LogInfo("CantidadesFaltantesDespacho", NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae Cantidades Faltantes Despacho
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0) //Si retorna datos
                    {                        
                        Cab_CantidadFaltanteDespacho CabJson = new Cab_CantidadFaltanteDespacho();
                        Cab2_CantidadFaltanteDespacho Cabecera = new Cab2_CantidadFaltanteDespacho();
                        Det_CantidadFaltanteDespacho Detalle = new Det_CantidadFaltanteDespacho();

                        string var_IntId = "";

                        //Recorre la confirmaciones de salida --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Cuando cambie de Folio o entre por primera vez debe cargar la estructura con la API -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API para integrar a Woocommerce segun nombre proceso que genero documento --------------------
                                #region Carga ruta de la API para integrar a Woocommerce segun nombre proceso que genero documento 
                                var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                var request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                CabJson = new Cab_CantidadFaltanteDespacho();
                                Cabecera = new Cab2_CantidadFaltanteDespacho();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda documento referencia que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //--------------------------------------------
                                Cabecera.Id = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.ColaPickId = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                Cabecera.INT_NAME = NombreProceso.Trim();
                                Cabecera.FECHA_HORA = DateTime.Parse(myData.Tables[0].Rows[i]["FechaEstado"].ToString()).ToString("dd-MM-yyyy HH:mm"); // "30-05-2022 13:35";
                                Cabecera.SolDespId = long.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString().Trim());
                                Cabecera.FechaProceso = DateTime.Parse(myData.Tables[0].Rows[i]["FechaProceso"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoDocumento = int.Parse(myData.Tables[0].Rows[i]["TipoDocumento"].ToString());
                                Cabecera.NumeroDocto = myData.Tables[0].Rows[i]["NumeroDocto"].ToString();
                                Cabecera.FechaDocto = DateTime.Parse(myData.Tables[0].Rows[i]["FechaDocto"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.TipoReferencia = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                Cabecera.NumeroReferencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                Cabecera.FechaReferencia = DateTime.Parse(myData.Tables[0].Rows[i]["FechaReferencia"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                Cabecera.RutCliente = myData.Tables[0].Rows[i]["RutCliente"].ToString();
                                Cabecera.TipoSolicitud = int.Parse(myData.Tables[0].Rows[i]["TipoSolicitud"].ToString());

                                //Busca detalles relacionados
                                string CondicionBusqueda = "IntId = " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                DataRow[] resultado = myData.Tables[0].Select(CondicionBusqueda);

                                foreach (DataRow fila in resultado)
                                {
                                    Detalle = new Det_CantidadFaltanteDespacho();

                                    Detalle.Linea = int.Parse(fila["Linea"].ToString()); // 1;
                                    Detalle.CodigoArticulo = fila["CodigoArticulo"].ToString(); // "5";
                                    Detalle.UnidadVenta = fila["UnidadMedida"].ToString(); // "UN";
                                    Detalle.ItemReferencia = int.Parse(fila["ItemReferencia"].ToString()); // 0;
                                    Detalle.FecVenctoSol = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    Detalle.CantidadDespachada = decimal.Parse(fila["CantidadProc"].ToString()); // 100;
                                    Detalle.CantidadSolicitada = decimal.Parse(fila["Cantidad"].ToString()); // 150;
                                    Detalle.CantidadPendiente = decimal.Parse(fila["Valor1"].ToString()); // 150;
                                    Detalle.LoteSerieDesp = fila["NroSerieDesp"].ToString(); // "132561";
                                    Detalle.FecVectoDesp = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    Detalle.Estado = int.Parse(fila["EstadoDet"].ToString());
                                    Cabecera.Items.Add(Detalle);
                                }

                                CabJson.cabeceras.Add(Cabecera);

                                //-------------------------------------------------------------
                                //Crea body para llamado con estructura de variable cargada ---
                                var body = JsonConvert.SerializeObject(CabJson);

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("CantidadesFaltantesDespacho", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim());

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             2, 
                                                                                                                             ""); //Procesado

                                    Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo("CantidadesFaltantesDespacho", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta.Trim());
                                }
                                else
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3, 
                                                                                                                             ""); //Procesado con error

                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo("CantidadesFaltantesDespacho", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta.Trim());
                                }

                            } //FIN si cambia de Id integracion

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo("CantidadesFaltantesDespacho", NombreProceso.Trim() + " - Error: " + ex.Message, true);
            }
        }

        //=======================================
        // 4 - WEBHOOK ALERTA ======================
        //=============================================
        private void WebhookAlerta(string NombreProceso)
        {
            try
            {
                LogInfo("WebhookAlerta", NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Alertas ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        //Recorre la confirmaciones de salida --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Carga ruta de la API para integrar a Woocommerce segun nombre proceso que genero documento --------------------
                            var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                            EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                            client.Timeout = -1;

                            //Indica el metodo de llamado de la API ----
                            var request = new RestRequest(Method.GET);
                            switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                            {
                                case "GET":
                                    request = new RestRequest(Method.GET); //consulta
                                    break;
                                case "POST":
                                    request = new RestRequest(Method.POST); //crea
                                    break;
                                case "PUT":
                                    request = new RestRequest(Method.PUT); //modifica
                                    break;
                            }

                            //Trae informacion para headers segun el nombre proceso -------
                            DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                        EmpId,
                                                                                                                        myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                        2);

                            //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                            if (dsHeaders.Tables.Count > 0)
                            {
                                for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                {
                                    //agrega key y su valor -----------
                                    request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                }
                            }

                            Alerta CabJson = new Alerta();
                            det_data d_data = new det_data();
                            det_headings d_headings = new det_headings();
                            det_contents d_contents = new det_contents();

                            string var_IntId = "";

                            //Cuando cambie de Id interno de integracion debe cargar la estructura con la API -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                CabJson = new Alerta();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda documento referencia que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //--------------------------------------------
                                CabJson.app_id = "f3314068-19b2-4348-a920-dff13f3b6647";

                                string[] Palabras = myData.Tables[0].Rows[i]["Texto1Cab"].ToString().Remove(myData.Tables[0].Rows[i]["Texto1Cab"].ToString().LastIndexOf(",")).Trim().Split(',');

                                CabJson.include_external_user_ids = Palabras;
                                CabJson.channel_for_external_user_ids = "push";

                                d_data = new det_data();
                                d_data.foo= "bar";
                                CabJson.data = d_data;

                                d_headings = new det_headings();
                                d_headings.en = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                CabJson.headings= d_headings;

                                d_contents = new det_contents();
                                d_contents.en = myData.Tables[0].Rows[i]["RazonSocial"].ToString();
                                CabJson.contents = d_contents;

                                //No procesa detalles ----------

                                //-------------------------------------------------------------
                                //Crea body para llamado con estructura de variable cargada ---
                                var body = JsonConvert.SerializeObject(CabJson);

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //------------------------------------------
                                // EJECUTA LLAMADO API ------------------------
                                //------------------------------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("WebhookAlerta", "IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim());

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             2, 
                                                                                                                             ""); //Procesado

                                    //Respuesta = "Integracion OK. " + rss["Message"].ToString().Trim() + ". Folio: " + myData.Tables[0].Rows[i]["Folio"].ToString().Trim();
                                    Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                    LogInfo("WebhookAlerta", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta);

                                }
                                else
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3, 
                                                                                                                             ""); //Procesado con error
                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo("WebhookAlerta", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta);
                                }

                            } //FIN si cambia de Id integracion

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo("WebhookAlerta", NombreProceso.Trim() + " - Error: " + ex.Message, true);
            }
        }

        //====================================
        // 5 - WEBHOOK TRACKING SDD =============
        //==========================================
        private void WebhookTracking(string NombreProceso)
        {
            try
            {
                LogInfo("WebhookTracking", NombreProceso.Trim() + " - Inicio ejecucion", true, false);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Alertas ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0) //si trae datos
                    {
                        //Recorre la confirmaciones de salida --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Carga ruta de la API para integrar a Woocommerce segun nombre proceso que genero documento --------------------
                            var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                            EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                            client.Timeout = -1;

                            //Indica el metodo de llamado de la API ----
                            var request = new RestRequest(Method.GET);
                            switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                            {
                                case "GET":
                                    request = new RestRequest(Method.GET); //consulta
                                    break;
                                case "POST":
                                    request = new RestRequest(Method.POST); //crea
                                    break;
                                case "PUT":
                                    request = new RestRequest(Method.PUT); //modifica
                                    break;
                            }

                            //Trae informacion para headers segun el nombre proceso -------
                            DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                        EmpId,
                                                                                                                        myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                        2);

                            //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                            if (dsHeaders.Tables.Count > 0)
                            {
                                for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                {
                                    //agrega key y su valor -----------
                                    request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                }
                            }

                            //======================================================================
                            //======================= ESPECIAL HONDA ================================
                            //========================================================================
                            if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("HONDA")) //si el proceso contiene coagra
                            {
                                string Token_ = "";

                                TokenHONDA("TOKEN_HONDA", ref Token_);

                                //Agrega headers adicional -----
                                request.AddHeader("Authorization", Token_);
                            }
                            //FIN ============== ESPECIAL PARA HONDA, PARAMETRIZAR LUEGO =============

                            Cab_WebhookTracking Tracking = new Cab_WebhookTracking();

                            string var_IntId = "";

                            //Cuando cambie de Id interno de integracion debe cargar la estructura con la API -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                Tracking = new Cab_WebhookTracking();

                                string[] Palabras = myData.Tables[0].Rows[i]["Texto1Cab"].ToString().Trim().Split('|');                                
                                //viene concatenado: estado|glosaEstado|Fecha|hora

                                //Guarda documento referencia que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                Tracking.SolDespId = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString());
                                Tracking.TipoReferencia = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                Tracking.NumeroReferencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                Tracking.RutCliente = myData.Tables[0].Rows[i]["RutCliente"].ToString();

                                Tracking.Estado = int.Parse(Palabras[0].Trim());
                                Tracking.EstadoGlosa = Palabras[1].Trim();
                                Tracking.FechaEstado = Palabras[2].Trim();
                                Tracking.HoraEstado = Palabras[3].Trim();

                                //No procesa detalles ----------

                                //-------------------------------------------------------------
                                //Crea body para llamado con estructura de variable cargada ---
                                var body = JsonConvert.SerializeObject(Tracking);

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //------------------------------------------
                                // EJECUTA LLAMADO API ------------------------
                                //------------------------------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("WebhookTracking", "IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true, false);

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, marca en estado Procesado 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             2, 
                                                                                                                             ""); //Procesado OK

                                    //Respuesta = "Integracion OK. " + rss["Message"].ToString().Trim() + ". Folio: " + myData.Tables[0].Rows[i]["Folio"].ToString().Trim();
                                    Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                    LogInfo("WebhookTracking", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta, true, false);

                                }
                                else
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, marca en estado error 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3, 
                                                                                                                             ""); //Procesado con error
                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                    LogInfo("WebhookTracking", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta, true, false);
                                }

                            } //FIN si cambia de Id integracion

                        } //FIN ciclo recorre Confirmaciones

                    } // FIN si trae datos
                }
            }
            catch (Exception ex)
            {
                LogInfo("WebhookTracking", NombreProceso.Trim() + " - Error: " + ex.Message, true, false);
            }
        }

        //====================================
        // 6 - WEBHOOK AJUSTE ===================
        //==========================================
        //      sp_proc_INT_ConfirmaAjuste: procedimiento que carga tabla de integracion con los datos para informar un Ajuste de Entrada, Salida o por Inventario -----------
        //      NombreProceso = CONFIRMA_AJUSTE
        private void ConfirmacionAjuste(string NombreProceso)
        {
            try
            {
                LogInfo("ConfirmacionAjuste", NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal;
                int Estado;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae Ajustes generados, de Entrada (1) o de Salida (2) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        //Cab_Ajuste CabJson =  new Cab_Ajuste();
                        //Cab2_Ajuste Cabecera = new Cab2_Ajuste();
                        //Det_Ajuste Detalle = new Det_Ajuste();

                        Cab_Ajuste2 Cabecera = new Cab_Ajuste2();
                        Det_Ajuste2 Detalle = new Det_Ajuste2();

                        string var_IntId = "";

                        //Recorre la confirmaciones de recepcion --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Cuando cambie de IntId debe cargar la estructura para enviar al Webhook -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                #region Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                var request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //VALIDAR SI TOKEN ES DINAMICO
                                //si token dinamico -> validar fecuencia
                                // si es 1 por llamado genrarlo
                                // si es por minutos calcular rango si debe actualizar o no


                                //si genero token actualizar value en 


                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                //=======================================================================
                                //======================= ESPECIAL COAGRA ================================
                                //=========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("COAGRA")) //si el proceso contiene coagra
                                {
                                    string Token_ = "";
                                    string NombreCookie1 = "", ValorCookie1 = "", NombreCookie2 = "", ValorCookie2 = "";

                                    CookieCOAGRA("COOKIE_TOKEN_COAGRA", ref Token_, ref NombreCookie1, ref ValorCookie1, ref NombreCookie2, ref ValorCookie2);

                                    //Agrega Cookies obtenidas -----
                                    request.AddCookie(NombreCookie1, ValorCookie1);
                                    request.AddCookie(NombreCookie2, ValorCookie2);

                                    //Agrega headers adicional -----
                                    request.AddHeader("x-csrf-token", Token_);

                                }
                                //FIN ============== ESPECIAL PARA COAGRA, PARAMETRIZAR LUEGO =============

                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                //CabJson = new Cab_Ajuste();
                                //Cabecera = new Cab2_Ajuste();

                                Cabecera = new Cab_Ajuste2();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda IntId que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //--------------------------------------------
                                Cabecera.Empid = int.Parse(myData.Tables[0].Rows[i]["Empid"].ToString().Trim());
                                Cabecera.TipoTransaccion = int.Parse(myData.Tables[0].Rows[i]["TipoSolicitud"].ToString().Trim());
                                Cabecera.FechaProceso = DateTime.Parse(myData.Tables[0].Rows[i]["FechaEstado"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022 13:35";
                                Cabecera.CodigoBodega = int.Parse(myData.Tables[0].Rows[i]["Valor1Cab"].ToString().Trim());
                                Cabecera.TipoReferencia = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                Cabecera.NumeroReferencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                Cabecera.Glosa = myData.Tables[0].Rows[i]["Texto1Cab"].ToString();
                                //--------------------------------------------

                                //Busca detalles relacionados al IntId
                                string CondicionBusqueda = "IntId = " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                DataRow[] resultado = myData.Tables[0].Select(CondicionBusqueda);

                                foreach (DataRow fila in resultado)
                                {
                                    //Detalle = new Det_Ajuste();
                                    Detalle = new Det_Ajuste2();

                                    Detalle.Linea = int.Parse(fila["Linea"].ToString()); // 1;
                                    Detalle.CodigoArticulo = fila["CodigoArticulo"].ToString(); // "5";
                                    Detalle.UnidadMedida = fila["UnidadMedida"].ToString(); // "UN";
                                    Detalle.Cantidad = decimal.Parse(fila["Cantidad"].ToString()); // 150;
                                    Detalle.Lote = fila["NroSerieDesp"].ToString();
                                    Detalle.FechaVencimiento = DateTime.Parse(myData.Tables[0].Rows[i]["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    Detalle.Estado = int.Parse(fila["EstadoDet"].ToString());

                                    Cabecera.Items.Add(Detalle);
                                }

                                //CabJson.cabeceras.Add(Cabecera);

                                //-------------------------------------------------------------
                                //Crea body para llamado con estructura de variable cargada ---
                                //var body = JsonConvert.SerializeObject(CabJson);
                                var body = JsonConvert.SerializeObject(Cabecera);

                                //Guarda JSON que se envia ------------------
                                LogInfo("ConfirmacionAjuste", " JSON Enviado: " + body.Trim());

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("ConfirmacionAjuste", NombreProceso.Trim() + " - Ejecuta api NumeroReferencia " + myData.Tables[0].Rows[i]["Folio"].ToString().Trim());

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK, retorna status 200 --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK))
                                {
                                    Estado = 2; //Procesado OK

                                    //====================== ESPECIAL ==========================
                                    // si hay que esperar respuesta del Webhook ----------------
                                    //==========================================================
                                    if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                    {
                                        //Debe venir la siguiente respuesta:
                                        //{
                                        //    "Resultado": "OK",
                                        //    "Descripcion": "Integracion OK"
                                        //}

                                        LogInfo("ConfirmacionAjuste", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". JSON Recibido: " + response.Content.ToString());

                                        try
                                        {
                                            JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                            string Resultado;
                                            string Descripcion;

                                            Resultado = rss["Resultado"].ToString(); //OK - ERROR
                                            Descripcion = rss["Descripcion"].ToString(); //descripcion 

                                            if (Resultado.Trim() == "OK")
                                            {
                                                Estado = 2; //Procesado OK
                                                Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            " .Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim();
                                            }
                                            else
                                            {
                                                Estado = 3; //Procesado con ERROR
                                                Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            " .Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim();
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            Respuesta = "Integrado OK, pero Respuesta no retorna estructura definida (Resultado y Descripcion). Error:" + ex.Message.Trim();
                                        }

                                        //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 Estado,
                                                                                                                                 "");

                                        LogInfo("ConfirmacionAjuste", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta.Trim());

                                    } //FIN si hay que esperar respuesta del Webhook ================ 
                                    else
                                    {
                                        //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 2, 
                                                                                                                                 ""); //Procesado

                                        Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo("ConfirmacionAjuste", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta);
                                    }
                                }
                                else
                                {
                                    //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3, 
                                                                                                                             ""); //Procesado con error

                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() + ". Status: " + CodigoRetorno.ToString();
                                    LogInfo("ConfirmacionAjuste", myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta.Trim());
                                }
                            } //FIN si cambia de IntId

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo("ConfirmacionAjuste", NombreProceso.Trim() + " - Error: " + ex.Message, true);
            }
        }

        //=====================================
        // 7 - WEBHOOK ANULA DESPACHO ===========
        //==========================================
        //      sp_proc_INT_AnulaPedidoWEBHOOK: procedimiento que carga tabla con datos para informar Anulacion de Pedido en Getpoint ----------
        //      NombreProceso = ANULA_PEDIDO
        private void AnulaPedido(string NombreProceso)
        {
            try
            {
                LogInfo(NombreProceso.Trim(), "Inicio ejecucion", true, false);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae RDM (Recepciones de mercancia) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        Cab_AnulaPedido Cabecera = new Cab_AnulaPedido();

                        string var_IntId = "";

                        //Recorre la confirmaciones de recepcion --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Cuando cambie de IntId debe cargar la estructura para enviar al Webhook -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                #region Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                var request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //VALIDAR SI TOKEN ES DINAMICO
                                //si token dinamico -> validar fecuencia
                                // si es 1 por llamado genrarlo
                                // si es por minutos calcular rango si debe actualizar o no


                                //si genero token actualizar value en 


                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                //=======================================================================
                                //======================= ESPECIAL COAGRA ================================
                                //=========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("COAGRA")) //si el proceso contiene coagra
                                {
                                    string Token_ = "";
                                    string NombreCookie1 = "", ValorCookie1 = "", NombreCookie2 = "", ValorCookie2 = "";

                                    CookieCOAGRA("COOKIE_TOKEN_COAGRA", ref Token_, ref NombreCookie1, ref ValorCookie1, ref NombreCookie2, ref ValorCookie2);

                                    //Agrega Cookies obtenidas -----
                                    request.AddCookie(NombreCookie1, ValorCookie1);
                                    request.AddCookie(NombreCookie2, ValorCookie2);

                                    //Agrega headers adicional -----
                                    request.AddHeader("x-csrf-token", Token_);

                                }
                                //FIN ============== ESPECIAL PARA COAGRA, PARAMETRIZAR LUEGO =============

                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa variable principal
                                Cabecera = new Cab_AnulaPedido();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda IntId que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //--------------------------------------------
                                //  {
                                //      "tipo_solicitud":1,
                                //      "referencia":4600465841,
                                //      "odp_wms":12345
                                //  }

                                Cabecera.tipo_solicitud = int.Parse(myData.Tables[0].Rows[i]["TipoSolicitud"].ToString());
                                Cabecera.referencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                Cabecera.odp_wms = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());

                                //-------------------------------------------------------------
                                //Crea body para llamado con estructura de variable cargada ---
                                var body = JsonConvert.SerializeObject(Cabecera);

                                //Guarda JSON que se envia ------------------
                                LogInfo(NombreProceso, " JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.odp_wms.ToString().Trim(), body.Trim());

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo(NombreProceso, NombreProceso.Trim() + " - Ejecuta api NumeroReferencia " + myData.Tables[0].Rows[i]["Folio"].ToString().Trim());

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK, retorna status 200 --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK))
                                {
                                    //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             2, 
                                                                                                                             ""); //Procesado

                                    Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo(NombreProceso, myData.Tables[0].Rows[i]["NombreProceso"].ToString() + " - " + Respuesta);
                                }
                                else
                                {
                                    //Actualiza estado de L_IntegraConfirmaciones, deja en estado Procesado 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3, 
                                                                                                                             ""); //Procesado con error

                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Cabecera.odp_wms.ToString().Trim());
                                }
                            } //FIN si cambia de IntId

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo(NombreProceso, "Error: " + ex.Message.Trim(), true, true, NombreProceso.Trim());
            }
        }

        //=====================================
        // 8 - API CREA PEDIDO DRIVIN ============
        //==========================================
        //      sp_proc_INT_CreaPedidoDrivin: procedimiento que carga tabla con datos para Crear Pedido de Driv.in ----------
        //      NombreProceso = CREA_PEDIDO_DRIVIN 
        private void CreaPedidoDrivin(string NombreProceso)
        {
            try
            {
                LogInfo(NombreProceso, NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                string result = "";
                int EmpId;
                int EmpIdGlobal = 0;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae ODP (Olas de Picking) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        //Cab_Confirmacion_SDD CabJson = new Cab_Confirmacion_SDD();
                        //Cab2_Confirmacion_SDD Cabecera = new Cab2_Confirmacion_SDD();
                        //Det_Confirmacion_SDD Detalle = new Det_Confirmacion_SDD();

                        PedidoDrivin_Cab Drivin_Cab = new PedidoDrivin_Cab();
                        PedidoDrivin_clients Drivin_clients = new PedidoDrivin_clients();
                        PedidoDrivin_time_windows Drivin_time_windows = new PedidoDrivin_time_windows();
                        PedidoDrivin_orders Drivin_orders = new PedidoDrivin_orders();
                        PedidoDrivin_items Drivin_items = new PedidoDrivin_items();
                        PedidoDrivin_pickups Drivin_pickups = new PedidoDrivin_pickups();

                        var client = new RestClient();
                        var request = new RestRequest(Method.GET);
                        DataSet dsHeaders = new DataSet();
                        string body = "";

                        string ListaIdProcesados = "";

                        string var_IntId = "";
                        string var_Folio = "";
                        string Token_ = "";
                        string NombreCookie1 = "";
                        string ValorCookie1 = "";
                        string NombreCookie2 = "";
                        string ValorCookie2 = "";
                        int i = 0;

                        //Recorre la confirmaciones de salida --------------
                        for (i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            // Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------
                            if (ConfigurationManager.AppSettings["ConfirmacionMultiplePorODP"].ToString() == "True")
                            {
                                //Cuando cambie de ODP debe cargar la estructura con la API y no sea el primer registro -----
                                if (myData.Tables[0].Rows[i]["Folio"].ToString().Trim() != var_Folio && var_Folio != "")
                                {
                                    //Crea body para llamado con estructura de variable cargada ---
                                    body = JsonConvert.SerializeObject(Drivin_Cab);

                                    //Guarda JSON que se envia ------------------
                                    LogInfo(NombreProceso, "JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code, body.Trim());

                                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                                    //EJECUTA LLAMADO API ---------------------------
                                    IRestResponse response = client.Execute(request);

                                    LogInfo(NombreProceso, myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true);

                                    HttpStatusCode CodigoRetorno = response.StatusCode;
                                    //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                    string Respuesta = "";

                                    //Si finalizó OK --------------------------
                                    if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                    {
                                        //====================== ESPECIAL ==========================
                                        // si hay que esperar respuesta del Webhook ----------------
                                        //==========================================================
                                        if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                        {
                                            //Respuesta OK:
                                            //{
                                            //    "success": true,
                                            //    "status": "OK",
                                            //    "response": 
                                            //    {
                                            //        "added": [],
                                            //        "edited": 
                                            //            [
                                            //                "100203955"
                                            //            ],
                                            //        "skipped": []
                                            //    }
                                            //}

                                            //Respuesta ERROR
                                            //{
                                            //    "success": false,
                                            //    "status": "Error",
                                            //    "response": 
                                            //    {
                                            //        "description": "Invalid address",
                                            //        "details": 
                                            //             [.... campo erroneo y json enviado....

                                            LogInfo(NombreProceso, "JSON respuesta recibido", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code, response.Content.ToString());

                                            JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                            string Resultado;
                                            string Descripcion;

                                            try
                                            {
                                                Resultado = rss["status"].ToString(); //OK - Error
                                                Descripcion = ""; // rss["status"].ToString(); //OK - ERROR
                                            }
                                            catch (Exception ex)
                                            {
                                                Resultado = "ERROR";
                                                Descripcion = "Respuesta no retorna estructura definida";
                                            }

                                            if (Resultado.Trim() == "OK")
                                            {
                                                //Si es ok guardamos mensaje exitoso
                                                Descripcion = "Pedido integrado correctamente en Drivin";

                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         2,
                                                                                                                                         ListaIdProcesados.Trim()); //Procesado

                                                Respuesta = "Integracion OK" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                            }
                                            else
                                            {
                                                //Si es error rescata descripcion error
                                                Descripcion = rss["Response"]["Description"].ToString();

                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         3,
                                                                                                                                         ListaIdProcesados.Trim()); //Procesado con error

                                                Respuesta = "Error" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                            }

                                            //Guarda respuesta en Dato2 ODP procesada -------------
                                            result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                        EmpIdGlobal,
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                        "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());

                                        } //FIN si hay que esperar respuesta del Webhook ================
                                        else
                                        { //llamado estandar, solo esperamos una ejecucion exitosa (status 200 - OK)
                                          //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2,
                                                                                                                                     ListaIdProcesados.Trim()); //Procesado

                                            Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                        }
                                    }
                                    else //status Error <> 200
                                    {
                                        //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 3,
                                                                                                                                 ListaIdProcesados.Trim()); //Procesado con error

                                        Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                    }

                                    //Inicializa la estructura principal
                                    //CabJson = new Cab_Confirmacion_SDD();
                                    Drivin_Cab = new PedidoDrivin_Cab();

                                    //Inicializa lista de Id procesados
                                    ListaIdProcesados = "";
                                }

                            } 
                            // FIN Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------

                            //Cuando cambie de Id Interno L_IntegraConfirmaciones carga estructura con la API -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API del cliente para enviar la confirmacion de la SDD --------------------
                                #region Carga URL de la API del cliente para enviar la confirmacion de la SDD 

                                client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //Trae informacion para headers segun el nombre proceso -------
                                dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                    EmpId,
                                                                                                                    myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                    2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                //=======================================================================
                                //======================= ESPECIAL COAGRA ===========================
                                //=========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("COAGRA")) //si el proceso contiene coagra debe rescatar el TOKEN para Coagra 
                                {
                                    Token_ = "";
                                    NombreCookie1 = "";
                                    ValorCookie1 = "";
                                    NombreCookie2 = "";
                                    ValorCookie2 = "";

                                    CookieCOAGRA("COOKIE_TOKEN_COAGRA", ref Token_, ref NombreCookie1, ref ValorCookie1, ref NombreCookie2, ref ValorCookie2);

                                    //Agrega Cookies obtenidas -----
                                    request.AddCookie(NombreCookie1, ValorCookie1);
                                    request.AddCookie(NombreCookie2, ValorCookie2);

                                    //Agrega headers adicional -----
                                    request.AddHeader("x-csrf-token", Token_);
                                }
                                //FIN ============== ESPECIAL PARA COAGRA, PARAMETRIZAR LUEGO =============

                                //======================================================================
                                //======================= ESPECIAL HONDA ================================
                                //========================================================================
                                if (myData.Tables[0].Rows[i]["NombreProceso"].ToString().Contains("HONDA")) //si el proceso contiene coagra
                                {
                                    Token_ = "";

                                    TokenHONDA("TOKEN_HONDA", ref Token_);

                                    //Agrega headers adicional -----
                                    request.AddHeader("Authorization", Token_);
                                }
                                //FIN ============== ESPECIAL PARA HONDA, PARAMETRIZAR LUEGO =============

                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------

                                //Inicializa cliente
                                //Drivin_Cab = new PedidoDrivin_Cab();
                                Drivin_clients = new PedidoDrivin_clients();
                                Drivin_time_windows = new PedidoDrivin_time_windows();
                                Drivin_orders = new PedidoDrivin_orders();

                                DateTime fecha;
                                fecha = DateTime.Now;

                                //Guarda documento referencia que esta procesando ---------
                                var_IntId = myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                //Crea lista de Id internos procesados, se usa en confirmacion multiple para marcarlos todos a la vez con OK o ERROR
                                ListaIdProcesados = ListaIdProcesados.Trim() + var_IntId.Trim() + ";";

                                //Guarda ODP que esta procesando ---------
                                var_Folio = myData.Tables[0].Rows[i]["Folio"].ToString().Trim();

                                //--------------------------------------------
                                //Cabecera.Id = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                //Cabecera.ColaPickId = int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString().Trim());
                                //Cabecera.INT_NAME = NombreProceso.Trim();
                                //Cabecera.FECHA_HORA = DateTime.Parse(myData.Tables[0].Rows[i]["FechaEstado"].ToString()).ToString("dd-MM-yyyy HH:mm"); // "30-05-2022 13:35";
                                //Cabecera.SolDespId = long.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString().Trim());
                                //Cabecera.FechaProceso = DateTime.Parse(myData.Tables[0].Rows[i]["FechaProceso"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                //Cabecera.TipoDocumento = int.Parse(myData.Tables[0].Rows[i]["TipoDocumento"].ToString());
                                //Cabecera.NumeroDocto = myData.Tables[0].Rows[i]["NumeroDocto"].ToString();
                                //Cabecera.FechaDocto = DateTime.Parse(myData.Tables[0].Rows[i]["FechaDocto"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                //Cabecera.TipoReferencia = myData.Tables[0].Rows[i]["TipoReferencia"].ToString();
                                //Cabecera.NumeroReferencia = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString();
                                //Cabecera.FechaReferencia = DateTime.Parse(myData.Tables[0].Rows[i]["FechaReferencia"].ToString()).ToString("dd-MM-yyyy"); // "30-05-2022";
                                //Cabecera.RutCliente = myData.Tables[0].Rows[i]["RutCliente"].ToString();
                                //Cabecera.TipoSolicitud = int.Parse(myData.Tables[0].Rows[i]["TipoSolicitud"].ToString());
                                //Cabecera.Dato1 = myData.Tables[0].Rows[i]["Dato1"].ToString();
                                //Cabecera.Dato2 = myData.Tables[0].Rows[i]["Dato2"].ToString();
                                //Cabecera.Dato3 = myData.Tables[0].Rows[i]["Dato3"].ToString();

                                //divide Texto1 que viene con datos concatenados
                                string[] Palabras = myData.Tables[0].Rows[i]["Texto1Cab"].ToString().Trim().Split('¬');

                                //0   s.Contacto + '¬' +
                                //1   s.Vendedor + '¬' +
                                //2   s.Pais + '¬' +
                                //3   reg.DescripcionRe + '¬' +
                                //4   ciu.DescripcionCi + '¬' +
                                //5   com.DescripcionCo + '¬' +
                                //6   s.Direccion + '¬' +
                                //7   s.CodigoPostal + '¬' +
                                //8   s.Email
                                //9   Telefono

                                //carga campos clase principal clientes ---------
                                Drivin_clients.code = ""; //": "201000345",
                                Drivin_clients.address = Palabras[6].ToString(); //La Oración 43",
                                Drivin_clients.reference = ""; //Departamento 208",
                                Drivin_clients.city = Palabras[5].ToString(); //Las Condes",
                                Drivin_clients.country = Palabras[2].ToString(); //Chile",
                                Drivin_clients.lat = ""; //-33.401779",
                                Drivin_clients.lng = ""; //-70.556216",
                                Drivin_clients.name = myData.Tables[0].Rows[i]["RazonSocial"].ToString(); ; //Nicolas Aguirre",
                                Drivin_clients.client_name = myData.Tables[0].Rows[i]["RazonSocial"].ToString(); //Nicolas Aguirre",
                                //Drivin_clients.client_code": null,
                                Drivin_clients.address_type = ""; //Departamento",
                                Drivin_clients.contact_name = Palabras[0]; //Nicolas Aguirre",
                                Drivin_clients.contact_phone = Palabras[9]; //9999999999",
                                Drivin_clients.contact_email = Palabras[8]; //nicolasaguirre@email.com",
                                Drivin_clients.additional_contact_name = ""; //Alexandra Araujo",
                                Drivin_clients.additional_contact_phone = ""; //22222222222",
                                Drivin_clients.additional_contact_email = ""; //alexandraraujo@email.com",
                                Drivin_clients.start_contact_name = ""; //Alexandra Araujo",
                                Drivin_clients.start_contact_phone = ""; //22222222222",
                                Drivin_clients.start_contact_email = ""; //alexandraraujo@email.com",
                                Drivin_clients.near_contact_name = ""; //Alexandra Araujo",
                                Drivin_clients.near_contact_phone = ""; //22222222222",
                                Drivin_clients.near_contact_email = ""; //alexandraraujo@email.com",
                                Drivin_clients.delivered_contact_name = Palabras[0]; //Nicolas Aguirre",
                                Drivin_clients.delivered_contact_phone = Palabras[9]; //9999999999",
                                Drivin_clients.delivered_contact_email = Palabras[8]; //nicolasaguirre@email.com",
                                Drivin_clients.service_time = 0; // ": 15,
                                Drivin_clients.sales_zone_code = ""; //Zona Sur",

                                //carga campos para 1 item en clase datos --------
                                Drivin_time_windows.start = "00:00";
                                Drivin_time_windows.end = "00:00";
                                Drivin_clients.time_windows.Add(Drivin_time_windows);

                                //carga campos para 1 item en clase Order -----------
                                Drivin_orders.code = myData.Tables[0].Rows[i]["NumeroReferencia"].ToString(); //100203955",
                                Drivin_orders.alt_code = ""; //996",
                                Drivin_orders.description = ""; //",
                                Drivin_orders.category = ""; //Delivery",
                                //Drivin_orders.units_1 = 0; //
                                //Drivin_orders.units_2 = 0; //
                                //Drivin_orders.units_3 = 0; //
                                Drivin_orders.position = 0; // ": 1,
                                Drivin_orders.delivery_date = ""; //2024-02-14",
                                Drivin_orders.priority = 0; // ": 0,
                                Drivin_orders.custom_1 = ""; //caja amarilla",
                                //Drivin_orders.custom_2 = ""; //
                                //Drivin_orders.custom_3 = ""; //
                                //Drivin_orders.custom_4 = ""; //
                                //Drivin_orders.custom_5 = ""; //
                                Drivin_orders.supplier_code = ""; //Falabella.com",
                                Drivin_orders.supplier_name = ""; //Falabella.com",
                                Drivin_orders.deploy_date = ""; //2024-02-14",

                                //Busca los detalles relacionados y los agrega a la cabecera
                                string CondicionBusqueda = "IntId = " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();

                                DataRow[] resultado = myData.Tables[0].Select(CondicionBusqueda);

                                foreach (DataRow fila in resultado)
                                {
                                    Drivin_items = new PedidoDrivin_items(); //Det_Confirmacion_SDD();

                                    Drivin_items.code = fila["CodigoArticulo"].ToString(); ; // 999890922",
                                    Drivin_items.description = fila["CodigoArticulo"].ToString(); // "BUZO ADIDAS TALLA 36/ AZUL",
                                    Drivin_items.units = int.Parse(fila["Cantidad"].ToString()); //1,
                                    Drivin_items.units_1 = int.Parse(fila["Cantidad"].ToString()); //1,
                                    //Drivin_items.units_2": null,
                                    //Drivin_items.units_3": null

                                    Drivin_orders.items.Add(Drivin_items);

                                    //Detalle.Linea = int.Parse(fila["Linea"].ToString()); // 1;
                                    //Detalle.CodigoArticulo = fila["CodigoArticulo"].ToString(); // "5";
                                    //Detalle.UnidadVenta = fila["UnidadMedida"].ToString(); // "UN";
                                    //Detalle.CantidadSolicitada = decimal.Parse(fila["Cantidad"].ToString()); // 150;
                                    //Detalle.ItemReferencia = int.Parse(fila["ItemReferencia"].ToString()); // 0;
                                    //Detalle.FecVenctoSol = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    //Detalle.CantidadDespachada = decimal.Parse(fila["CantidadProc"].ToString()); // 100;
                                    //Detalle.LoteSerieDesp = fila["NroSerieDesp"].ToString(); // "132561";
                                    //Detalle.FecVenctoDesp = DateTime.Parse(fila["FechaVectoDesp"].ToString()).ToString("dd-MM-yyyy");
                                    //Detalle.Dato1 = fila["Dato1Det"].ToString();
                                    //Detalle.Dato2 = fila["Dato2Det"].ToString();
                                    //Detalle.Dato3 = fila["Dato3Det"].ToString();

                                    //Cabecera.Items.Add(Detalle);
                                }

                                //CabJson.cabeceras.Add(Cabecera);

                                Drivin_clients.orders.Add(Drivin_orders);
                                Drivin_Cab.clients.Add(Drivin_clients);

                                // Si genera confirmaciones individuales ------------------------------------------------
                                if (ConfigurationManager.AppSettings["ConfirmacionMultiplePorODP"].ToString() == "False")
                                {
                                    //Crea body para llamado con estructura de variable cargada ---
                                    body = JsonConvert.SerializeObject(Drivin_Cab);

                                    //Guarda JSON que se envia ------------------
                                    LogInfo(NombreProceso, "JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code, body.Trim());

                                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                                    //EJECUTA LLAMADO API ---------------------------
                                    IRestResponse response = client.Execute(request);

                                    LogInfo(NombreProceso, myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true);

                                    HttpStatusCode CodigoRetorno = response.StatusCode;
                                    //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                    string Respuesta = "";

                                    //Si finalizó OK --------------------------
                                    if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                    {
                                        //====================== ESPECIAL ==========================
                                        // si hay que esperar respuesta del Webhook ----------------
                                        //==========================================================
                                        if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                        {
                                            //Respuesta OK:
                                            //{
                                            //    "success": true,
                                            //    "status": "OK",
                                            //    "response": 
                                            //    {
                                            //        "added": [],
                                            //        "edited": 
                                            //            [
                                            //                "100203955"
                                            //            ],
                                            //        "skipped": []
                                            //    }
                                            //}

                                            //Respuesta ERROR
                                            //{
                                            //    "success": false,
                                            //    "status": "Error",
                                            //    "response": 
                                            //    {
                                            //        "description": "Invalid address",
                                            //        "details": 
                                            //             [.... campo erroneo y json enviado....

                                            LogInfo(NombreProceso, "JSON respuesta recibido", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code, response.Content.ToString());

                                            JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                            string Resultado;
                                            string Descripcion;

                                            try
                                            {
                                                Resultado = rss["status"].ToString(); //OK - Error
                                                Descripcion = ""; // rss["status"].ToString(); //OK - ERROR
                                            }
                                            catch (Exception ex)
                                            {
                                                Resultado = "ERROR";
                                                Descripcion = "Respuesta no retorna estructura definida";
                                            }

                                            if (Resultado.Trim() == "OK")
                                            {
                                                //Si es ok guardamos mensaje exitoso
                                                Descripcion = "Pedido integrado correctamente en Drivin";

                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         2,
                                                                                                                                         ""); //Procesado

                                                Respuesta = "Integracion OK" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                            }
                                            else
                                            {
                                                //Si es error rescata descripcion error
                                                Descripcion = rss["Response"]["Description"].ToString();

                                                //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                                result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                         3,
                                                                                                                                         ""); //Procesado con error

                                                Respuesta = "Error" +
                                                            ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                            ". Resultado: " + Resultado.Trim() +
                                                            ". Descripcion: " + Descripcion.Trim();

                                                LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                            }

                                            //Guarda respuesta en Dato2 ODP procesada -------------
                                            result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                        EmpIdGlobal,
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                        int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                        "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());

                                        } //FIN si hay que esperar respuesta del Webhook ================
                                        else
                                        { //llamado estandar, solo esperamos una ejecucion exitosa (status 200 - OK)
                                          //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2,
                                                                                                                                     ""); //Procesado

                                            Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                        }
                                    }
                                    else //status Error <> 200
                                    {
                                        //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 3,
                                                                                                                                 ""); //Procesado con error

                                        Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                    }

                                    //Inicializa variable principal
                                    //CabJson = new Cab_Confirmacion_SDD();
                                    Drivin_Cab = new PedidoDrivin_Cab();

                                } 
                                // FIN Si genera confirmaciones individuales ------------------------------------------------

                            } //FIN si cambia de Id integracion

                        } //FIN ciclo recorre Confirmaciones

                        // Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------
                        if (ConfigurationManager.AppSettings["ConfirmacionMultiplePorODP"].ToString() == "True")
                        {
                            //Al finalizar el ciclo envia la confirmacion de la ultima ODP que estaba procesando
                            //Cuando cambie de ODP debe cargar la estructura con la API -----

                            //Ubica el puntero en la ultima fila ---
                            i = myData.Tables[0].Rows.Count - 1;

                            if (var_Folio != "")
                            {
                                //Crea body para llamado con estructura de variable cargada ---
                                body = JsonConvert.SerializeObject(Drivin_Cab);

                                //Guarda JSON que se envia ------------------
                                LogInfo(NombreProceso, "JSON Enviado", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code, body.Trim());

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo(NombreProceso, myData.Tables[0].Rows[i]["NombreProceso"].ToString() + ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim(), true);

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                string Respuesta = "";

                                //Si finalizó OK --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK)) //Si la API destino retorna un status 200 marca como integrado OK ---
                                {
                                    //====================== ESPECIAL ==========================
                                    // si hay que esperar respuesta del Webhook ----------------
                                    //==========================================================
                                    if (ConfigurationManager.AppSettings["EsperaRespuestaWEBHOOK"].ToString() == "True")
                                    {
                                        //Respuesta OK:
                                        //{
                                        //    "success": true,
                                        //    "status": "OK",
                                        //    "response": 
                                        //    {
                                        //        "added": [],
                                        //        "edited": 
                                        //            [
                                        //                "100203955"
                                        //            ],
                                        //        "skipped": []
                                        //    }
                                        //}

                                        //Respuesta ERROR
                                        //{
                                        //    "success": false,
                                        //    "status": "Error",
                                        //    "response": 
                                        //    {
                                        //        "description": "Invalid address",
                                        //        "details": 
                                        //             [.... campo erroneo y json enviado....

                                        LogInfo(NombreProceso, "JSON respuesta recibido", true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code, response.Content.ToString());

                                        JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                        string Resultado;
                                        string Descripcion;

                                        try
                                        {
                                            Resultado = rss["status"].ToString(); //OK - Error
                                            Descripcion = ""; // rss["status"].ToString(); //OK - ERROR
                                        }
                                        catch (Exception ex)
                                        {
                                            Resultado = "ERROR";
                                            Descripcion = "Respuesta no retorna estructura definida";
                                        }

                                        if (Resultado.Trim() == "OK")
                                        {
                                            //Si es ok guardamos mensaje exitoso
                                            Descripcion = "Pedido integrado correctamente en Drivin";

                                            //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     2,
                                                                                                                                     ListaIdProcesados.Trim()); //Procesado

                                            Respuesta = "Integracion OK" +
                                                        ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                        ". Resultado: " + Resultado.Trim() +
                                                        ". Descripcion: " + Descripcion.Trim();

                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                        }
                                        else
                                        {
                                            //Si es error rescata descripcion error
                                            Descripcion = rss["Response"]["Description"].ToString();

                                            //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                            result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                     3,
                                                                                                                                     ListaIdProcesados.Trim()); //Procesado con error

                                            Respuesta = "Error" +
                                                        ". IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim() +
                                                        ". Resultado: " + Resultado.Trim() +
                                                        ". Descripcion: " + Descripcion.Trim();

                                            LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                        }

                                        //Guarda respuesta en Dato2 ODP procesada -------------
                                        result = WS_Integrador.Classes.model.InfF_Generador.InformaRespuestaWebhook(myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                    EmpIdGlobal,
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["Folio"].ToString()),
                                                                                                                    int.Parse(myData.Tables[0].Rows[i]["FolioRel"].ToString()),
                                                                                                                    "Resultado: " + Resultado.Trim() + " .Descripcion: " + Descripcion.Trim());

                                    } //FIN si hay que esperar respuesta del Webhook ================
                                    else
                                    { //llamado estandar, solo esperamos una ejecucion exitosa (status 200 - OK)
                                      //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado Procesado 
                                        result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                                 2,
                                                                                                                                 ListaIdProcesados.Trim()); //Procesado

                                        Respuesta = "Integracion OK. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                        LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                    }
                                }
                                else //status Error <> 200
                                {
                                    //Actualiza estado de L_IntegraConfirmacionesDet, deja en estado error 
                                    result = WS_Integrador.Classes.model.InfF_Generador.ActualizaEstadoIntegraConfirmaciones(int.Parse(myData.Tables[0].Rows[i]["IntId"].ToString()),
                                                                                                                             3,
                                                                                                                             ListaIdProcesados.Trim()); //Procesado con error

                                    Respuesta = "Error. IntId: " + myData.Tables[0].Rows[i]["IntId"].ToString().Trim();
                                    LogInfo(NombreProceso, Respuesta, true, true, myData.Tables[0].Rows[i]["NombreProceso"].ToString(), Drivin_orders.code);
                                }

                                //Inicializa la estructura principal
                                //CabJson = new Cab_Confirmacion_SDD();
                                Drivin_Cab = new PedidoDrivin_Cab();

                                //Inicializa lista de Id procesados
                                ListaIdProcesados = "";
                            }

                        } 
                        // FIN Si genera confirmaciones MULTIPLES por ODP ------------------------------------------------

                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo(NombreProceso, "Error: " + ex.Message.Trim(), true, true, NombreProceso.Trim());
            }
        }

        public static string GetGeneral(String URL, String Token, Int32 offset, string Llamado = "")
        {
            if (Llamado == "")
            {
                Llamado = "GetGeneral";
            }

            LogInfo(Llamado, "URL:" + URL + ";Token:" + Token + ";off:" + offset.ToString());

            if (offset > 0)
                URL = URL + @"&offset=" + offset.ToString();

            //if (MostrarURL)
            //    Func.log("->-> " + URL);
            var client = new RestClient(URL);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("access_token", Token);
            //request.AddParameter("application/json", json, ParameterType.RequestBody);

            //IRestResponse response;

            string respuesta = "";

            //llamado api variante BSale, se hace Control de error bad gateway desde BSale, se hacen 3 intentos de llamada ----
            for (Int32 intentos = 1; intentos <= 3; intentos++)
            {
                IRestResponse response = client.Execute(request);

                //Si la respuesta es un json válido y no un texto html
                if (response.Content.ToString().Contains("{") == true &&
                    response.Content.ToString().Contains("<html>") == false &&
                    response.Content.ToString().Contains("502 Bad Gateway") == false)
                {
                    //return response.Content;
                    respuesta = response.Content;
                    break;
                }
                else
                {
                    LogInfo(Llamado, "Error respuesta BSALE. N° intento: " + intentos.ToString().Trim() +
                                       ", URL: " + URL.Trim() +
                                       ", Respuesta BSALE: " + response.Content.ToString().Trim());
                }
            }

            return respuesta;
        }

        //Este mensaje depende de la configuracion para ocultar o no los mensajes de Log
        //Por defecto los mensajes se ocultaran cuando RegistroArchivoLog = "NO"
        //Si el mensaje se indica MostrarSiempre lo mostrara independiente lo que diga la Key RegistroArchivoLog 
        public static void LogInfo(string sMessage, string motivo, bool MostrarSiempre = false, bool GuardarEnBD = false, string NombreProceso ="", string Referencia = "", string EstructuraJSON = "")
        {
            try
            {
                //Si la key indica grabar log o los mensajes debe mostrarlos siempre, graba el log
                if (ConfigurationManager.AppSettings["RegistroArchivoLog"].ToString() == "SI" ||
                    ConfigurationManager.AppSettings["RegistroArchivoLog"].ToString() == "0" ||
                    MostrarSiempre == true)
                {
                    StringBuilder html = new StringBuilder();
                    string FilePath = ConfigurationManager.AppSettings["PathLogITEC"].ToString() + "\\Log_Integrador_" + DateTime.Now.ToString("MMdd") + ".txt";

                    if (EstructuraJSON.Trim() != "")
                    {
                        html.Append("[" + sMessage.ToString() + 
                                    "]. Fecha/Hora " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ". " + 
                                    motivo.Trim() + ". JSON= " + EstructuraJSON.Trim());
                    }
                    else
                    {
                        html.Append("[" + sMessage.ToString() + "]. Fecha/Hora " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ". " + motivo.Trim());
                    }
                                        
                    html.Append(Environment.NewLine);
                    StreamWriter strStreamWriter = File.AppendText(FilePath);
                    strStreamWriter.Write(html.ToString());
                    strStreamWriter.Close();

                    //Si la key indica grabar log en la base de datos y el mensaje tambien =================
                    if (ConfigurationManager.AppSettings["RegistroBDLog"].ToString() == "SI" && GuardarEnBD == true)
                    {
                        LogInfoBD(NombreProceso, motivo, Referencia, EstructuraJSON);
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo(e.Message, "Error creación de Log.");
            }
        }

        //Graba log en base de datos, depende de la key RegistroBDLog, si esta en SI, guardará el mensaje en el log
        #region Funcion LogInfoBD para grabar archivo de log en la Base de datos
        public static void LogInfoBD(string NombreProceso, string Mensaje, string Referencia, string EstructuraJSON = "")
        {
            try
            {
                string result;

                //Guarda registro en tabla L_LogAPI
                result = WS_Integrador.Classes.model.InfF_Generador.GuardaLogEnBaseDatos(NombreProceso,
                                                                                         Referencia,
                                                                                         Mensaje,
                                                                                         EstructuraJSON); //Procesado
            }
            catch (Exception e)
            {
                LogInfo(e.Message, "Creación de Log BD", true);
            }
        }
        #endregion

        //-------------------------------------------------------------------------------
        //ESPECIAL COAGRA: retorna Cookie y token para invocar WEBHOOK DE CONFIRMACION
        //-------------------------------------------------------------------------------
        private void CookieCOAGRA(string NombreProceso, ref string Token_, ref string NombreCookie1, ref string ValorCookie1, ref string NombreCookie2, ref string ValorCookie2)
        {
            try
            {
                LogInfo("CookieCOAGRA", NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae RDM (Recepciones de mercancia) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        string var_IntId = "";

                        //Recorre la confirmaciones de recepcion --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Cuando cambie de IntId debe cargar la estructura para enviar al Webhook -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                #region Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                var request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }
                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------
                                var body = @"";

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("CookieCOAGRA", "Ejecutara llamado obtener cookie y token");

                                HttpStatusCode CodigoRetorno = response.StatusCode;
                                //JObject rss = JObject.Parse(response.Content); //recupera json de retorno

                                //Si finalizó OK, retorna status 200 --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK))
                                {                        
                                    //recorre y concatena cookies generadas ------------------
                                    for (int m = 0; m < response.Cookies.Count; m++)
                                    {
                                        if (m == 0)
                                        {
                                            NombreCookie1 = response.Cookies[m].Name.Trim();
                                            ValorCookie1 = response.Cookies[m].Value.Trim();
                                        }

                                        if (m == 1)
                                        {
                                            NombreCookie2 = response.Cookies[m].Name.Trim();
                                            ValorCookie2 = response.Cookies[m].Value.Trim();
                                        }
                                    }

                                    //busca header token generado ------------
                                    for (int p = 0; p < response.Headers.Count; p++)
                                    {
                                        if (response.Headers[p].Name.Trim() == "x-csrf-token")
                                        {
                                            Token_ = response.Headers[p].Value.ToString().Trim();
                                        }
                                    }

                                    LogInfo("CookieCOAGRA", "Cookie Coagra OK. Cookie1= " + NombreCookie1.Trim() + "=" + ValorCookie1.Trim() + 
                                                                             ";Cookie2= " + NombreCookie2.Trim() + "=" + ValorCookie2.Trim() +
                                                            ", Token=" + Token_.Trim());
                                }
                                else
                                {

                                    LogInfo("CookieCOAGRA", "Error al obtener cookie y token coagra", true);
                                }
                            } //FIN si cambia de IntId

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo("CookieCOAGRA", NombreProceso.Trim() + " - Error: " + ex.Message, true);
            }
        }

        //-------------------------------------------------------------------------------
        //ESPECIAL HONDA: retorna token para invocar WEBHOOK DE CONFIRMACION
        //-------------------------------------------------------------------------------
        private void TokenHONDA(string NombreProceso, ref string Token_)
        {
            try
            {
                LogInfo("TokenHONDA", NombreProceso.Trim() + " - Inicio ejecucion", true);

                //para evitar error de seguridad en el llamado a la API ----------
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; //TLS 1.1 
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12; // para error No se puede crear un canal seguro SSL/TLS

                string stEmpId = ConfigurationManager.AppSettings["EmpId"].ToString();
                int EmpId;
                int EmpIdGlobal;

                Int32.TryParse(stEmpId, out EmpId);

                //Extrae RDM (Recepciones de mercancia) ----------
                DataSet myData = WS_Integrador.Classes.model.InfF_Generador.ShowList_IntegraConfirmacionesJson(EmpId,
                                                                                                               NombreProceso);
                if (myData.Tables.Count > 0)
                {
                    if (myData.Tables[0].Rows.Count > 0)
                    {
                        string var_IntId = "";

                        //Recorre la confirmaciones de recepcion --------------
                        for (int i = 0; i <= myData.Tables[0].Rows.Count - 1; i++)
                        {
                            //Cuando cambie de IntId debe cargar la estructura para enviar al Webhook -----
                            if (myData.Tables[0].Rows[i]["IntId"].ToString().Trim() != var_IntId || var_IntId == "")
                            {
                                //Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                #region Carga URL de la API Webhook del cliente correspondiente al proceso segun la empresa ----------
                                var client = new RestClient(myData.Tables[0].Rows[i]["URL_EndPoint"].ToString().Trim());

                                EmpIdGlobal = int.Parse(myData.Tables[0].Rows[i]["EmpIdGlobal"].ToString());

                                client.Timeout = -1;

                                //Indica el metodo de llamado de la API ----
                                var request = new RestRequest(Method.GET);
                                switch (myData.Tables[0].Rows[i]["Metodo"].ToString().Trim())
                                {
                                    case "GET":
                                        request = new RestRequest(Method.GET); //consulta
                                        break;
                                    case "POST":
                                        request = new RestRequest(Method.POST); //crea
                                        break;
                                    case "PUT":
                                        request = new RestRequest(Method.PUT); //modifica
                                        break;
                                }

                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsHeaders = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            2);

                                //Trae los headers (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsHeaders.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsHeaders.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddHeader(dsHeaders.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsHeaders.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }

                                //Trae informacion para headers segun el nombre proceso -------
                                DataSet dsParameters = WS_Integrador.Classes.model.InfF_Generador.ShowList_EndPointHeadersJson(EmpIdGlobal,
                                                                                                                            EmpId,
                                                                                                                            myData.Tables[0].Rows[i]["NombreProceso"].ToString(),
                                                                                                                            3);

                                //Trae los Parameters (atributo y valor) necesarios para realizar el llamado a la api segun nombre de proceso que esta integrando ----------------
                                if (dsParameters.Tables.Count > 0)
                                {
                                    for (int k = 0; k <= dsParameters.Tables[0].Rows.Count - 1; k++)
                                    {
                                        //agrega key y su valor -----------
                                        request.AddParameter(dsParameters.Tables[0].Rows[k]["myKey"].ToString().Trim(), dsParameters.Tables[0].Rows[k]["myValue"].ToString().Trim());
                                    }
                                }
                                #endregion

                                //Carga Variable para generar JSON ----------------------------------------------
                                var body = @"";

                                request.AddParameter("application/json", body, ParameterType.RequestBody);

                                //EJECUTA LLAMADO API ---------------------------
                                IRestResponse response = client.Execute(request);

                                LogInfo("TokenHONDA", "Ejecutara llamado obtener cookie y token");

                                HttpStatusCode CodigoRetorno = response.StatusCode;

                                //Si finalizó OK, retorna status 200 --------------------------
                                if (CodigoRetorno.Equals(HttpStatusCode.OK))
                                {
                                    JObject rss = JObject.Parse(response.Content); //recupera json de retorno
                                    string access_token;
                                    string token_type;

                                    try
                                    {
                                        access_token = rss["access_token"].ToString(); //OK - ERROR
                                        token_type = rss["token_type"].ToString(); //descripcion 

                                        Token_ = "Bearer " + access_token.Trim();
                                        LogInfo("TokenHONDA", "Token HONDA OK. Token=" + Token_.Trim());
                                    }
                                    catch (Exception ex)
                                    {
                                        LogInfo("TokenHONDA", "Error al obtener cookie y token coagra. Error:" + ex.Message.Trim(), true);
                                    }
                                }
                                else
                                {
                                    LogInfo("TokenHONDA", "Error al obtener cookie y token coagra", true);
                                }
                            } //FIN si cambia de IntId

                        } //FIN ciclo recorre Confirmaciones
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo("TokenHONDA", NombreProceso.Trim() + " - Error: " + ex.Message, true);
            }
        }
    }
}

