using Administracion.Classes.global;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace WS_Integrador.Classes.model
{
    public partial class InfF_Generador : Component
    {
        public InfF_Generador()
        {
            InitializeComponent();
        }

        public InfF_Generador(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //Trae los registros que debe enviar a los Webhooks
        public static DataSet ShowList_IntegraConfirmacionesJson(int EmpId,string NombreProceso)
        {
            DataSet myDataSet = new DataSet();
            OleDbConnection myConnection = DB.getOleDbConnection();

            OleDbCommand myCommand = new OleDbCommand("sp_sel_API_IntegraConfirmacionesJson", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@EmpId", OleDbType.Integer).Value = EmpId;
            myCommand.Parameters.Add("@NombreProceso", OleDbType.VarChar).Value = NombreProceso;
            myCommand.Parameters.Add("@Limit", OleDbType.Integer).Value = 100;
            myCommand.Parameters.Add("@Rowset", OleDbType.Integer).Value = 0;

            try
            {
                myCommand.CommandTimeout = 9999;
                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                OleDbDataAdapter myAdapter = new OleDbDataAdapter();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(myDataSet, "sp_sel_API_IntegraConfirmacionesJson");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return myDataSet;
        }

        //Trae los headers definidos para las rutas de los Webhooks
        public static DataSet ShowList_EndPointHeadersJson(int EmpidGlobal, int EmpId, string NombreProceso,int TipoParam)
        {
            DataSet myDataSet = new DataSet();
            OleDbConnection myConnection = DB.getOleDbConnection();

            OleDbCommand myCommand = new OleDbCommand("sp_sel_INT_EndPointHeadersJson", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@EmpidGlobal", OleDbType.Integer).Value = EmpidGlobal;
            myCommand.Parameters.Add("@EmpId", OleDbType.Integer).Value = EmpId;
            myCommand.Parameters.Add("@NombreProceso", OleDbType.VarChar).Value = NombreProceso;
            myCommand.Parameters.Add("@TipoParam", OleDbType.VarChar).Value = TipoParam;
            myCommand.Parameters.Add("@Limit", OleDbType.Integer).Value = 100;
            myCommand.Parameters.Add("@Rowset", OleDbType.Integer).Value = 0;

            try
            {
                myCommand.CommandTimeout = 9999;
                myConnection.Open();
                //myCommand.ExecuteNonQuery();
                OleDbDataAdapter myAdapter = new OleDbDataAdapter();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(myDataSet, "sp_sel_API_IntegraConfirmacionesJson_Headers");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return myDataSet;
        }

        //Marca estado de los detalles de la integracion, si todos los id detalle asociados a un id cabecera estan marcados, marca la cabecera como procesada
        public static string ActualizaIntegraConfirmacionesDet(int Estado,int Intid,int IdDet)
        {
            OleDbConnection myConnection = DB.getConnection();
            OleDbCommand myCommand = new OleDbCommand("sp_upd_API_IntegraConfirmacionesDet", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@Estado", OleDbType.Numeric).Value = Estado;
            myCommand.Parameters.Add("@IntId", OleDbType.Numeric).Value = Intid;
            myCommand.Parameters.Add("@IdDet", OleDbType.Numeric).Value = IdDet;

            string result;
            try
            {
                myCommand.CommandTimeout = 99999;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception ex)
            {
                result = "Error";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }

        //Cambia estado de cabecera de integracion 
        public static string ActualizaEstadoIntegraConfirmaciones(int Id, int Estado, string ListaIdProcesados)
        {
            OleDbConnection myConnection = DB.getConnection();
            OleDbCommand myCommand = new OleDbCommand("sp_upd_API_EstadoIntegraConfirmaciones", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@Id", OleDbType.Numeric).Value = Id;
            myCommand.Parameters.Add("@Estado", OleDbType.Numeric).Value = Estado;
            myCommand.Parameters.Add("@ListaIdProcesados", OleDbType.VarChar).Value = ListaIdProcesados;

            string result;
            try
            {
                myCommand.CommandTimeout = 99999;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception ex)
            {
                result = "Error";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }

        public static string InformaRespuestaWebhook(string NombreProceso, int EmpIdGlobal, int Folio, int FolioRel, string Descripcion)
        {
            OleDbConnection myConnection = DB.getConnection();
            OleDbCommand myCommand = new OleDbCommand("sp_upd_API_InformaRespuestaWebhook", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@NombreProceso", OleDbType.VarChar).Value = NombreProceso;
            myCommand.Parameters.Add("@EmpIdGlobal", OleDbType.Integer).Value = EmpIdGlobal;
            myCommand.Parameters.Add("@Folio", OleDbType.Numeric).Value = Folio;
            myCommand.Parameters.Add("@FolioRel", OleDbType.Numeric).Value = FolioRel;
            myCommand.Parameters.Add("@Descripcion", OleDbType.VarChar).Value = Descripcion;

            string result;
            try
            {
                myCommand.CommandTimeout = 99999;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception ex)
            {
                result = "Error";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }

        public static string GuardaLogEnBaseDatos(string NombreProceso, string Referencia, string Mensaje, string JSON)
        {
            OleDbConnection myConnection = DB.getConnection();
            OleDbCommand myCommand = new OleDbCommand("sp_in_API_LogAPI", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@NombreProceso", OleDbType.VarChar).Value = NombreProceso;
            myCommand.Parameters.Add("@Referencia", OleDbType.VarChar).Value = Referencia;
            myCommand.Parameters.Add("@Mensaje", OleDbType.VarChar).Value = Mensaje;
            myCommand.Parameters.Add("@JSON", OleDbType.VarChar).Value = JSON;

            string result;
            try
            {
                myCommand.CommandTimeout = 99999;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception ex)
            {
                result = "Error";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }


        //Asocia archivo adjunto a SDD
        public static string GuardaAdjuntoSDD_SolImportDespachoAdj(int SolDespId, DateTime FechaDigitacion, string UsuarioDig, string Path)
        {
            OleDbConnection myConnection = DB.getConnection();
            OleDbCommand myCommand = new OleDbCommand("sp_in_SolImportDespachoAdj", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@SolDespId", OleDbType.Numeric).Value = SolDespId;
            myCommand.Parameters.Add("@FechaDigitacion", OleDbType.Date).Value = FechaDigitacion;
            myCommand.Parameters.Add("@UsuarioDig", OleDbType.VarChar).Value = UsuarioDig;
            myCommand.Parameters.Add("@Path", OleDbType.VarChar).Value = Path;
            //myCommand.Parameters.Add("@SolDespAdjId              int output

            string result;
            try
            {
                OleDbParameter SolDespAdjId = new OleDbParameter("@SolDespAdjId", OleDbType.Numeric);
                SolDespAdjId.Direction = ParameterDirection.Output;
                myCommand.Parameters.Add(SolDespAdjId);

                myCommand.CommandTimeout = 99999;
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                if (SolDespAdjId.Value.ToString() != "0")
                {
                    result = "OK";
                }
                else 
                {
                    result = "Error";
                }
            }
            catch (Exception ex)
            {
                result = "Error";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }

        public static string ActualizaSolDespachoReferenciaEnvioEcommerce(int SolDespId, string Dato4, string ZPL, string RutaPDF)
        {
            OleDbConnection myConnection = DB.getConnection();
            OleDbCommand myCommand = new OleDbCommand("sp_upd_INT_SolDespachoRefEnvioToEcomm", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@SolDespId", OleDbType.Numeric).Value = SolDespId;
            myCommand.Parameters.Add("@Dato4", OleDbType.VarChar, 250).Value = Dato4;
            myCommand.Parameters.Add("@ZPL", OleDbType.VarChar).Value = ZPL;
            myCommand.Parameters.Add("@RutaPDF", OleDbType.VarChar, 250).Value = RutaPDF;

            string result;
            try
            {
                myCommand.CommandTimeout = 99999;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception ex)
            {
                result = "Error";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }

        public static string GeneraConfirmacionRevision(int RevisionId)
        {
            OleDbConnection myConnection = DB.getConnection();
            OleDbCommand myCommand = new OleDbCommand("sp_proc_INT_ConfirmacionREV", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add("@REV", OleDbType.Numeric).Value = RevisionId;

            string result;
            try
            {
                myCommand.CommandTimeout = 99999;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                result = "OK";
            }
            catch (Exception ex)
            {
                result = "Error";
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
            return result;
        }
    }
}


