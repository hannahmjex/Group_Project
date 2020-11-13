using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace GroupProject.Main
{
	class clsMainSQL
	{
		/// <summary>
		/// Connection string to the database.
		/// </summary>
		private string sConnectionString;

		/// <summary>
		/// Constructor that sets the connection string to the database
		/// </summary>
		public clsMainSQL()
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
		public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
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

						//Add the information for the SelectCommand using the SQL statement and the connection object
						adapter.SelectCommand = new OleDbCommand(sSQL, conn);
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

		#region SQL Statement Methods
		/// <summary>
		/// Query to get all item info from Item Desc
		/// </summary>
		/// <returns></returns>
		public string SelectFromItemDesc()
		{
			return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
		}

		/// <summary>
		/// Query to get invoice number, invoice date, and cost in Invoices based on an invoice number
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string SelectFromInvoices(int invoiceNum)
		{
			return $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceNum}";
		}

		/// <summary>
		/// Query to get item code, description, and cost in line items based on item code and invoice number
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string SelectFromLineItems(int invoiceNum)
		{
			return $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = {invoiceNum}";
		}

		/// <summary>
		/// Query to add an Item to LineItems.
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <param name="lineItemNum"></param>
		/// <param name="itemCode"></param>
		/// <returns></returns>
		public string InsertLineItems(int invoiceNum, int lineItemNum, string itemCode)
		{
			return $"INSERT INTO LineItems (InvoiceNum, LineitemNum, ItemCode) VALUES ({invoiceNum}, {lineItemNum}, '{itemCode}')";
		}

		/// <summary>
		/// Query to add an Item to Invoices.
		/// </summary>
		/// <param name="invoiceDate"></param>
		/// <param name="totalCost"></param>
		/// <returns></returns>
		public string InsertInvoices(string invoiceDate, int totalCost)
		{
			return $"INSERT INTO Invoices (InvoiceDate, TotalCost) Values ('{invoiceDate}', {totalCost})";
		}

		/// <summary>
		/// Query to update an Item from Invoice.
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <param name="cost"></param>
		/// <returns></returns>
		public string UpdateInvoices(int invoiceNum, int cost)
		{
			return $"UPDATE Invoices SET TotalCost = {cost} WHERE InvoiceNum = {invoiceNum}";
		}

		/// <summary>
		/// Query to delete an Item from LineItems.
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string DeleteLineItems(int invoiceNum)
		{
			return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceNum}";
		}

		/// <summary>
		/// Query to delete an Item from Invoice.
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string DeleteInvoices(int invoiceNum)
		{
			return $"DELETE FROM Invoices WHERE InvoiceNum = {invoiceNum}";
		}
		#endregion
	}
}
