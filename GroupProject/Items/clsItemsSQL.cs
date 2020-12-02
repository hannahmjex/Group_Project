using System;
using System.Collections.Generic;
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
        /// Query to get all item info from the database.
        /// </summary>
        /// <returns></returns>
        public string GetItemsDesc()
        {
            return "SELECT * FROM ItemDesc";
        }

        /// <summary>
        /// Query to select count of invoices in LineItems based on a ItemCode.
        /// </summary>
        /// <param name="itemCode">The ItemCode of the Item to check.</param>
        /// <returns></returns>
        public string ItemsInInvoiceCount(string itemCode)
        {
            return $"SELECT distinctCOUNT(InvoiceNum) FROM LineItems WHERE ItemCode = {itemCode}";
        }

        /// <summary>
        /// Query to add an Item to the database.
        /// </summary>
        /// <param name="description">The Item description</param>
        /// <param name="cost">The Item cost</param>
        /// <returns></returns>
        public string AddItems(string description, string cost)
        {
            return $"INSERT INTO ItemDesc (ItemDesc, Cost) VALUES ('{description}', {cost})";
        }

        /// <summary>
        /// Query to update an Item from the database.
        /// </summary>
        /// <param name="itemCode">The itemcode of the Item to update</param>
        /// <param name="description">The description of the Item to update</param>
        /// <param name="cost">The cost of the Item to update</param>
        /// <returns></returns>
        public string UpdateItems(string itemCode, string description, string cost)
        {
            return $"UPDATE ItemDesc SET ItemDesc = '{description}', Cost = {cost} WHERE ItemCode = {itemCode}";
        }

        /// <summary>
        /// Query to delete an Item from the database.
        /// </summary>
        /// <param name="itemCode">The ItemCode of the Item to delete</param>
        /// <returns></returns>
        public string DeleteItems(string itemCode)
        {
            return $"DELETE FROM ItemDesc WHERE ItemCode = {itemCode}";
        }
    }
}

