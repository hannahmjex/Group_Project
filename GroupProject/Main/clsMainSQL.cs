﻿using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Reflection;


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
		/// <param name="invoiceNumber"></param>
		/// <returns></returns>
		public ObservableCollection<Item> SelectFromLineItems(string invoiceNumber)
		{
			try
			{
                // Create local invoices collection
                ObservableCollection<Item> items = new ObservableCollection<Item>();

                var sql =  $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = " + invoiceNumber;

                var ds = db.ExecuteSQLStatement(sql, ref returnValues);

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
        /// Query to add an Item to LineItems.
        /// </summary>
        /// <param name="lineItem"></param>
        public void InsertLineItems(LineItem lineItem)
		{
			try
			{
                // Set local variables
                var invoiceNum = (lineItem.InvoiceNumber != null) ? lineItem.InvoiceNumber : throw new Exception("Internal Error: cannot insert new line item without invoice number.");
                var lineItemNum = lineItem.LineItemNumber;
                var itemCode = lineItem.ItemCode;

                string sql = "INSERT INTO LineItems (InvoiceNum, LineitemNum, ItemCode) VALUES ('"+invoiceNum+"', '"+lineItemNum+"', '"+itemCode+"')";
				db.ExecuteNonQuery(sql);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
													   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

        /// <summary>
		/// Query to get line items for the specified invoice from the database
		/// </summary>
		/// <param name="invoiceNumber"></param>
		/// <returns></returns>
        internal List<LineItem> GetLineItemsForInvoice(int invoiceNum)
        {
            try
            {
                // Create local invoices collection
                List<LineItem> lineItems = new List<LineItem>();

                //var sql = $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = " + invoiceNumber;
                var sql = $"SELECT * FROM LineItems WHERE InvoiceNum = " + invoiceNum;

                var ds = db.ExecuteSQLStatement(sql, ref returnValues);

                // Iterate over rows
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // Grab data from their columns for each row
                    string invoiceNumber = ds.Tables[0].Rows[i][0].ToString();
                    string lineItemNum = ds.Tables[0].Rows[i][1].ToString();
                    string itemCode = ds.Tables[0].Rows[i][2].ToString();

                    // Create new local lineItem object
                    LineItem lineItem = new LineItem(int.Parse(lineItemNum), itemCode);
                    lineItem.SetInvoiceNumber(invoiceNumber);

                    // Add newly created lineItem object to local collection of lineItems.
                    lineItems.Add(lineItem);
                }
                return lineItems;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                                       MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// gets item 
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public DataSet GetItemCode(string itemDesc)
		{
			try
			{
				string sql = "SELECT ItemCode FROM ItemDesc WHERE ItemDesc = @itemDesc";
				return db.ExecuteSQLStatement(sql, ref returnValues, (OleDbCommand cmd) =>
				{
					cmd.Parameters.AddWithValue("@itemCode", itemDesc);
				});
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
		public void InsertInvoices(string invoiceDate, string totalCost)
		{
			try
			{
				var sql = "INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#" +invoiceDate + "#," +totalCost + ")";
				db.ExecuteNonQuery(sql);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// gets invoice number based on invoice
		/// </summary>
		/// <returns></returns>
		public DataSet GetInvoiceNum()
		{
			try
			{
				string sql = "SELECT MAX(InvoiceNum) FROM Invoices";
				return db.ExecuteSQLStatement(sql, ref returnValues);
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
		public void UpdateInvoices(string invoiceNum, string cost)
		{
			try
			{
				string sql = "UPDATE Invoices SET TotalCost =" + cost +" WHERE InvoiceNum = " + invoiceNum;
				db.ExecuteNonQuery(sql);
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
		public void DeleteLineItems(string invoiceNum, int? lineItemNum = null)
		{
			try
			{
                string sql = (lineItemNum == null) ? "DELETE FROM LineItems WHERE InvoiceNum = " + invoiceNum : "DELETE FROM LineItems WHERE InvoiceNum = "+invoiceNum+" AND LineItemNum = "+lineItemNum;
				db.ExecuteNonQuery(sql);
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
		public void DeleteInvoices(string invoiceNum)
		{
			try
			{
                string sql = "DELETE FROM Invoices WHERE InvoiceNum = "+invoiceNum;
				db.ExecuteNonQuery(sql);
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
