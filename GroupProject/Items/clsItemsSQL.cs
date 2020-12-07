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

namespace GroupProject.Items
{
	public class clsItemsSQL
	{
		/// <summary>
		/// Values to be returned
		/// </summary>
		int returnValues;

		/// <summary>
		/// DataAccess Object Declaration
		/// </summary>
		private clsDataAccess db;

		/// <summary>
		/// DataSet Object Declaration
		/// </summary>
		DataSet ds;

		/// <summary>
		/// class item
		/// </summary>
		Item Item;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public clsItemsSQL()
		{
			try
			{
				db = new clsDataAccess();
				ds = new DataSet();
				Item = new Item();
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Retrieves all Items 
		/// </summary>
		/// <returns>An ObservableCollection of all Items</returns>
		public ObservableCollection<Item> GetAllItems()
		{
			try
			{
				ObservableCollection<Item> items = new ObservableCollection<Item>();
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
				return items;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Checks if the provided Item Code does not exist
		/// </summary>
		/// <param name="Code">Item's Code</param>
		/// <returns>boolean representing uniqueness of the code</returns>
		public DataSet CheckUniqueCode(string Code)
		{
			try
			{
				string sql = "SELECT ItemCode FROM ItemDesc WHERE ItemCode = @Code";

				return db.ExecuteSQLStatement(sql, ref returnValues, (OleDbCommand cmd) =>
				{
					cmd.Parameters.AddWithValue("@Code", Code);
				});
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Inserts a new Item
		/// </summary>
		/// <param name="Code">Item's Code</param>
		/// <param name="Desc">Item's Description</param>
		/// <param name="Cost">Item's Cost</param>
		public void InsertNewItem(string Code, string Desc, string Cost)
		{
			try
			{
				string sql = @"INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) VALUES ('" + Code + "', '" + Desc + "', '" + Cost + "')";
				db.ExecuteNonQuery(sql);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Deletes an Item 
		/// </summary>
		/// <param name="Code">Item to delete's code</param>
		public void DeleteItem(string Code)
		{
			try
			{
				string sql = "DELETE FROM ItemDesc WHERE ItemCode = '" + Code + "'";
				db.ExecuteNonQuery(sql);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Updates an Item
		/// </summary>
		/// <param name="currentItemCode">Item's Code</param>
		/// <param name="desc">Item's Description</param>
		/// <param name="cost">Item's Cost</param>
		public void UpdateItem(string code, string desc, string cost)
		{
			try
			{
				var sql = @"UPDATE ItemDesc SET ItemDesc = '" + desc + "', Cost = '" + cost + "' WHERE ItemCode = '" + code + "'";
				
				db.ExecuteNonQuery(sql);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Checks if an Item exists on any Invoice
		/// </summary>
		/// <param name="SelectedItem">Item to check for</param>
		/// <returns>A List of Invoice Numbers on which the Item exists</returns>
		public List<int> CheckItemOnInvoice(Item SelectedItem)
		{
			try
			{
				List<int> invoiceNums = new List<int>();

				var sql = "SELECT DISTINCT InvoiceNum FROM LineItems WHERE ItemCode = '" + SelectedItem.Code + "'";

				int retVal = 0;

				ds = db.ExecuteSQLStatement(sql, ref retVal);

				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					invoiceNums.Add(Convert.ToInt32(dr[0]));
				}

				return invoiceNums;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
					MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}
	}
}

