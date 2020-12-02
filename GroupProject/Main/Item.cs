using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
	class Item
	{
        /// <summary>
        /// Constructs an item object that takes in an item code, description and cost in as parameters
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        public Item(int itemCode, string itemDesc, string itemCost)
        {
            try
            {
                ItemCode = itemCode;
                ItemDesc = itemDesc;
                ItemCost = itemCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Integer invoice number for invoice
        /// </summary>
        public int ItemCode { get; set; }

        /// <summary>
        /// String Date on the invoice object
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        /// String of the total cost of the invoice
        /// </summary>
        public string ItemCost { get; set; }
    }
}
