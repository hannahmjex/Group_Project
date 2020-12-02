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
		/// List of items to be returned
		/// </summary>
		public List<clsMainLogic> Items;

		/// <summary>
		/// Item code
		/// </summary>
		public string ItemCode;

		/// <summary>
		/// Item name
		/// </summary>
		public string ItemDescription;

		/// <summary>
		/// item cost
		/// </summary>
		public string ItemCost;

		/// <summary>
		/// Constructor
		/// </summary>
		public clsMainLogic()
		{
			Items = new List<clsMainLogic>();
			ItemCode = "";
			ItemDescription = "";
			ItemCode = "";
		}

		/// <summary>
		/// This method overrides the toString metho
		/// It just returns the item descriptions
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			try
			{
				return ItemDescription;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
			}
		}

	}
}

