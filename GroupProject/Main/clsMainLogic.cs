using System;
using System.Collections.Generic;
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
		/// class exception handling
		/// </summary>
		clsExceptionHandling ExceptionHandling;

		/// <summary>
		/// main SQL
		/// </summary>
		clsMainSQL mainSQL;

		/// <summary>
		/// List of items to be returned
		/// </summary>
		public List<string> Items;

		public string ItemCode;
		public string ItemDescription;
		public string ItemCost;
		
		/// <summary>
		/// Constructor
		/// </summary>
		public clsMainLogic()
		{
			ExceptionHandling = new clsExceptionHandling();
			mainSQL = new clsMainSQL();
			Items = new List<string>();
		}

		public List<string> GetItems() {
			try
			{
				return mainSQL.SelectFromItemDesc();
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}
	}
}

