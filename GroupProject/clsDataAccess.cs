using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
	class clsDataAccess
	{
		/// <summary>
		/// Connection string to the database.
		/// </summary>
		private string sConnectionString;

		/// <summary>
		/// Constructor that sets the connection string to the database
		/// </summary>
		public clsDataAccess()
		{
			sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
		}

		/// <summary>
		/// This method takes an SQL statment that is passed in and executes it.  The resulting values
		/// are returned in a DataSet.  The number of rows returned from the query will be put into
		/// the reference parameter iRetVal.
		/// </summary>
		/// <param name="sSQL">The SQL statement to be executed.</param>
		/// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
		/// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
		public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal, Action<OleDbCommand> callback = null)
		{
			try
			{
				//Create a new DataSet
				DataSet ds = new DataSet();

				using (OleDbConnection conn = new OleDbConnection(sConnectionString))
				{
					using (OleDbDataAdapter adapter = new OleDbDataAdapter())
					{

						//Open the connection to the database
						conn.Open();

						//Create an OleDbCommand object using the sql statement and the connection
						var cmd = new OleDbCommand(sSQL, conn);

						//Invoke callback in cmd object if callback is not null
						callback?.Invoke(cmd);

						//Add the information for the SelectCommand using the SQL statement and the connection object
						adapter.SelectCommand = cmd;
						adapter.SelectCommand.CommandTimeout = 0;

						//Fill up the DataSet with data
						adapter.Fill(ds);
					}
				}

				//Set the number of values returned
				iRetVal = ds.Tables[0].Rows.Count;

				//return the DataSet
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method takes an SQL statment that is passed in and executes it.  The resulting single 
		/// value is returned.
		/// </summary>
		/// <param name="sSQL">The SQL statement to be executed.</param>
		/// <returns>Returns a string from the scalar SQL statement.</returns>
		public string ExecuteScalarSQL(string sSQL)
		{
			try
			{
				//Holds the return value
				object obj;

				using (OleDbConnection conn = new OleDbConnection(sConnectionString))
				{
					using (OleDbDataAdapter adapter = new OleDbDataAdapter())
					{

						//Open the connection to the database
						conn.Open();

						//Add the information for the SelectCommand using the SQL statement and the connection object
						adapter.SelectCommand = new OleDbCommand(sSQL, conn);
						adapter.SelectCommand.CommandTimeout = 0;

						//Execute the scalar SQL statement
						obj = adapter.SelectCommand.ExecuteScalar();
					}
				}

				//See if the object is null
				if (obj == null)
				{
					//Return a blank
					return "";
				}
				else
				{
					//Return the value
					return obj.ToString();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method takes an SQL statment that is a non query and executes it.
		/// </summary>
		/// <param name="sSQL">The SQL statement to be executed.</param>
		/// <returns>Returns the number of rows affected by the SQL statement.</returns>
		public int ExecuteNonQuery(string sSQL)
		{
			try
			{
				//Number of rows affected
				int iNumRows;

				using (OleDbConnection conn = new OleDbConnection(sConnectionString))
				{
					//Open the connection to the database
					conn.Open();

					//Add the information for the SelectCommand using the SQL statement and the connection object
					OleDbCommand cmd = new OleDbCommand(sSQL, conn);
					cmd.CommandTimeout = 0;

					//Execute the non query SQL statement
					iNumRows = cmd.ExecuteNonQuery();
				}

				//return the number of rows affected
				return iNumRows;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}
		/// <summary>
		/// Executes a Scalar call to the DB
		/// </summary>
		/// <param name="cmd">OleDbCommand object</param>
		/// <returns>A string returned by DB</returns>
		public string ExecuteScalarSQL(OleDbCommand cmd)
		{
			try
			{
				object obj;

				using (OleDbConnection conn = new OleDbConnection(sConnectionString))
				{
					using (OleDbDataAdapter adapter = new OleDbDataAdapter())
					{

						conn.Open();

						cmd.Connection = conn;
						adapter.SelectCommand = cmd;
						adapter.SelectCommand.CommandTimeout = 0;

						obj = adapter.SelectCommand.ExecuteScalar();
					}
				}

				return obj == null ? "" : obj.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Execute a normal SQL statement
		/// </summary>
		/// <param name="cmd">OleDbCommand object</param>
		/// <param name="iRetVal">Reference to an integer to hold the returned vlue</param>
		/// <returns>A DataSet returned by the DB</returns>
		public DataSet ExecuteSQLStatement(OleDbCommand cmd, ref int iRetVal)
		{
			try
			{
				DataSet ds = new DataSet();

				using (OleDbConnection conn = new OleDbConnection(sConnectionString))
				{
					using (OleDbDataAdapter adapter = new OleDbDataAdapter())
					{

						conn.Open();

						cmd.Connection = conn;
						adapter.SelectCommand = cmd;
						adapter.SelectCommand.CommandTimeout = 0;

						adapter.Fill(ds);

					}
				}

				iRetVal = ds.Tables[0].Rows.Count;

				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Execute a Non Query to the DB
		/// </summary>
		/// <param name="SQLStatement">A string with the SQL Query</param>
		/// <returns>An integer returned by DB</returns>
		public int ExecuteNonQuery(OleDbCommand cmd)
		{
			try
			{

				int numRows;

				using (OleDbConnection conn = new OleDbConnection(sConnectionString))
				{

					conn.Open();

					cmd.Connection = conn;
					cmd.CommandTimeout = 0;

					numRows = cmd.ExecuteNonQuery();
				}

				return numRows;

			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}
	}
}