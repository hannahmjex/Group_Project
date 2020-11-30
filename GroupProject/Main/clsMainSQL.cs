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
