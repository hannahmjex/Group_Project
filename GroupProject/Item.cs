using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
    public class Item
    {
        /// <summary>
        /// Constructs an item object that takes in an item code, description and cost in as parameters
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemCost"></param>
        public Item(char itemCode, string itemDesc, int itemCost)
        {
            try
            {
                ItemCode = itemCode;
                ItemDesc = itemDesc;
                ItemCost = itemCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Integer item code
        /// </summary>
        public char ItemCode { get; set; }

        /// <summary>
        /// item description
        /// </summary>
        public string ItemDesc { get; set; }

        /// <summary>
        /// item cost
        /// </summary>
        public int ItemCost { get; set; }

        //public char Code { get { return ItemCode; } }
        //public string Description { get { return ItemDesc; } }
        //public int Cost { get { return ItemCost; } }
    }
}
