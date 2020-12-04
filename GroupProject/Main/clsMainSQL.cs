using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace GroupProject
{
	public class clsMainSQL
	{
		/// <summary>
		/// Values to be returned
		/// </summary>
		int returnValues;

		/// <summary>
		/// database class
		/// </summary>
		clsDataAccess db;

		/// <summary>
		/// data set class
		/// </summary>
		DataSet ds;

		/// <summary>
		/// constructor
		/// </summary>
		public clsMainSQL()
		{
			returnValues = 0;
			db = new clsDataAccess();
			ds = new DataSet();
		}

		#region SQL Statement Methods
		/// <summary>
		/// Query to get all item info from Item Desc
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<Item> SelectFromItemDesc()
		{
			try
			{
				// Create local invoices collection
				ObservableCollection<Item> items = new ObservableCollection<Item>();

				// Get all items from the database.
				ds = db.ExecuteSQLStatement("SELECT ItemCode, ItemDesc, Cost FROM ItemDesc", ref returnValues);

				// Iterate over rows
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					// Grab data from their columns for each row
					string itemCode = ds.Tables[0].Rows[i][0].ToString();
					string itemDescription = ds.Tables[0].Rows[i][1].ToString();
					string itemCost = ds.Tables[0].Rows[i][2].ToString();

					// Create new local item object
					Item item = new Item(itemCode, itemDescription, itemCost);

					// Add newly created item object to local collection of items.
					items.Add(item);
				}

				// Return items
				return items;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// returns item code and cost based on description
		/// </summary>
		/// <param name="itemDesc"></param>
		/// <returns></returns>
		public DataSet GetItemRow(string itemDesc)
		{
			try
			{
				var sql = @"SELECT ItemCode, ItemDesc, Cost FROM ItemDesc WHERE ItemDesc = @itemDesc";
				
				return ds = db.ExecuteSQLStatement(sql, ref returnValues, (OleDbCommand cmd) =>
				{
					cmd.Parameters.AddWithValue("@itemDesc", itemDesc);
				});
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Query to get invoice number, invoice date, and cost in Invoices based on an invoice number
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string SelectFromInvoices(int invoiceNum)
		{
			try
			{
				return $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceNum}";
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Query to get item code, description, and cost in line items based on item code and invoice number
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string SelectFromLineItems(int invoiceNum)
		{
			try
			{
				return $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = {invoiceNum}";
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
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
			try
			{
				return $"INSERT INTO LineItems (InvoiceNum, LineitemNum, ItemCode) VALUES ({invoiceNum}, {lineItemNum}, '{itemCode}')";
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Query to add an Item to Invoices.
		/// </summary>
		/// <param name="invoiceDate"></param>
		/// <param name="totalCost"></param>
		/// <returns></returns>
		public DataSet InsertInvoices(string invoiceDate, int totalCost)
		{
			try
			{
				var sql = @"INSERT INTO Invoices(InvoiceDate, TotalCost) Values(@invoiceDate}, { @totalCost})";

				return ds = db.ExecuteSQLStatement(sql, ref returnValues, (OleDbCommand cmd) =>
				{
					cmd.Parameters.AddWithValue("@invoiceDate", invoiceDate);
					cmd.Parameters.AddWithValue("@totalCost", totalCost);
				});
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// gets invoice number based on invoice date and total cost
		/// </summary>
		/// <param name="invoiceDate"></param>
		/// <param name="totalCost"></param>
		/// <returns></returns>
		public DataSet GetInvoiceNum(string invoiceDate, int totalCost)
		{
			try
			{
				var sql = @"SELECT InvoiceNum FROM Invoices WHERE InvoiceDate = @invoiceDate AND TotalCost = @totalCost";

				return ds = db.ExecuteSQLStatement(sql, ref returnValues, (OleDbCommand cmd) =>
				{
					cmd.Parameters.AddWithValue("@invoiceDate", invoiceDate);
					cmd.Parameters.AddWithValue("@totalCost", totalCost);
				});
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Query to update an Item from Invoice.
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <param name="cost"></param>
		/// <returns></returns>
		public string UpdateInvoices(int invoiceNum, int cost)
		{
			try
			{
				return $"UPDATE Invoices SET TotalCost = {cost} WHERE InvoiceNum = {invoiceNum}";
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Query to delete an Item from LineItems.
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string DeleteLineItems(int invoiceNum)
		{
			try
			{
				return $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceNum}";
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Query to delete an Item from Invoice.
		/// </summary>
		/// <param name="invoiceNum"></param>
		/// <returns></returns>
		public string DeleteInvoices(int invoiceNum)
		{
			try
			{
				return $"DELETE FROM Invoices WHERE InvoiceNum = {invoiceNum}";
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}
		#endregion
	}
}
