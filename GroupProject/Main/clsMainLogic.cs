using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}
	}
}

