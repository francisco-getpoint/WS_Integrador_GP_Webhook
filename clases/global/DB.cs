using System;
using System.Data.OleDb;
using System.Configuration;

namespace Administracion.Classes.global 
{
	/// <summary>
	/// Descripci�n breve de DB.
	/// </summary>
	public class DB : System.ComponentModel.Component
	{
		/// <summary>
		/// Variable del dise�ador requerida.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DB(System.ComponentModel.IContainer container)
		{
			///
			/// Requerido para la compatibilidad con el Dise�ador de composiciones de clases Windows.Forms
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: agregar c�digo de constructor despu�s de llamar a InitializeComponent
			//
		}

		public DB()
		{
			///
			/// Requerido para la compatibilidad con el Dise�ador de composiciones de clases Windows.Forms
			///
			InitializeComponent();

			//
			// TODO: agregar c�digo de constructor despu�s de llamar a InitializeComponent
			//
		}

		/// <summary> 
		/// Limpiar los recursos que se est�n utilizando.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public static OleDbConnection getConnection() 
		{
			try 
			{
				OleDbConnection mySqlConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"]);
				return mySqlConnection;
			}
			catch (Exception e) 
			{
				throw new Exception (e.Message.ToString()) ;
			}
		}
		
		public static OleDbConnection getOleDbConnection()
		{
			try 
			{
				OleDbConnection mySqlConnection = new OleDbConnection(ConfigurationManager.AppSettings["ConnectionString"]);
				return mySqlConnection;
			}
			catch (Exception e) 
			{
				throw new Exception (e.Message.ToString()) ;
			}
		}

		#region C�digo generado por el Dise�ador de componentes
		/// <summary>
		/// M�todo necesario para admitir el Dise�ador. No se puede modificar
		/// el contenido del m�todo con el editor de c�digo.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
