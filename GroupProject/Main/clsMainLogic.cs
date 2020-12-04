using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace GroupProject
{
	/// <summary>
	/// All logic for adding, deleting, saving, editing invoices/items will live here
	/// </summary>
	public class clsMainLogic
	{
		/// <summary>
		/// main sql class
		/// </summary>
		clsMainSQL mainSQL;

		/// <summary>
		/// Constructor
		/// </summary>
		public clsMainLogic()
		{
			mainSQL = new clsMainSQL();
		}

		/// <summary>
		/// returns an ObservableCollection of all items
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<Item> GetAllItems()
		{
			try
			{
				return mainSQL.SelectFromItemDesc();
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method gets the item info from the item descriptions
		/// </summary>
		/// <param name="itemDesc"></param>
		/// <returns></returns>
		public List<string> GetItemRow(string itemDesc)
		{
			try
			{
				DataSet itemRow = mainSQL.GetItemRow(itemDesc);
				List<string> itemRowList = new List<string>();

				for (int i = 0; i < itemRow.Tables[0].Columns.Count; i++)
				{
					itemRowList.Add(itemRow.Tables[0].Rows[0][i].ToString());
				}
				return itemRowList;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// saves the invoice
		/// </summary>
		/// <param name="invoiceDate"></param>
		/// <param name="totalCost"></param>
		/// <returns></returns>
		public void SaveInvoice(string invoiceDate, string totalCost)
		{ 
			try
			{
				//save invoice
				mainSQL.InsertInvoices(invoiceDate, totalCost);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// returns invoice number
		/// </summary>
		/// <returns></returns>
		public string GetInvoiceNumber()
		{
			try
			{
				//get invoice number
				DataSet invoiceNum = mainSQL.GetInvoiceNum();
				List<string> invoiceNumString = new List<string>();

				//extract invoice number and add to list bc idk how else to do this
				for (int i = 0; i < invoiceNum.Tables[0].Columns.Count; i++)
				{
					invoiceNumString.Add(invoiceNum.Tables[0].Rows[0][i].ToString());
				}
				return invoiceNumString[0];
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// updates the invoice
		/// </summary>
		/// <param name="totalCost"></param>
		public void UpdateInvoice(string totalCost)
		{
			try
			{
				//get invoice number
				string invoiceNum = GetInvoiceNumber();
				mainSQL.UpdateInvoices(invoiceNum, totalCost);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// calls sql statement to delete the invoice
		/// </summary>
		public void DeleteInvoice()
		{
			try
			{
				//get invoice number
				string invoiceNum = GetInvoiceNumber();
				mainSQL.DeleteInvoices(invoiceNum);
				mainSQL.DeleteLineItems(invoiceNum);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

        /// <summary>
		/// returns an ObservableCollection of all items
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<Item> GetItemsForInvoice(string invoiceNumber)
        {
            try
            {
                return mainSQL.SelectFromLineItems(invoiceNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}

