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
                /*
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    items.Add(new Item(Convert.ToChar(dr[0]), dr[1].ToString(), Convert.ToInt32(dr[2])));
                }*/

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
                //string sql = "SELECT ItemCode FROM ItemDesc WHERE ItemCode = " + Code;

                //Cmd = new OleDbCommand("SELECT ItemCode FROM ItemDesc WHERE ItemCode = @Code");
                //Cmd.Parameters.Add("@Code", OleDbType.WChar).Value = Code;

                //return db.ExecuteScalarSQL(Cmd) == "";
                //string code = db.ExecuteScalarSQL(sql, (OleDbCommand cmd) => { cmd.Parameters.AddWithValue("@Code", Code); });
                return db.ExecuteSQLStatement(sql, ref returnValues, (OleDbCommand cmd) =>
                {
                    cmd.Parameters.AddWithValue("@Code", Code);
                });
                 
                //Do logic for boolean in itemLogic
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
                string sql = @"INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) VALUES ('"+Code+"', '"+Desc+"', '"+Cost+"')";
                //Cmd = new OleDbCommand("INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) VALUES (@Code, @Desc, @Cost)");
                //Cmd.Parameters.Add("@Code", OleDbType.WChar).Value = Code;
                //Cmd.Parameters.Add("@Desc", OleDbType.WChar).Value = Desc;
                //Cmd.Parameters.Add("@Cost", OleDbType.Integer).Value = Cost;

                //db.ExecuteNonQuery(Cmd);

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
                string sql = "DELETE FROM ItemDesc WHERE ItemCode = '"+Code+"'";

               // Cmd = new OleDbCommand("DELETE FROM ItemDesc WHERE ItemCode = @Code");
                //Cmd.Parameters.Add("@Code", OleDbType.WChar).Value = Code;

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
        /// <param name="Code">Item's Code</param>
        /// <param name="Desc">Item's Description</param>
        /// <param name="Cost">Item's Cost</param>
        public void UpdateItem(string Code, string Desc, string Cost)
        {
            try
            {
                //string sql = @"UPDATE ItemDesc SET ItemDesc = '"+Desc+"' AND Cost = '"+Cost+"' AND ItemCode = '"+Code.ToString()+"' WHERE ItemCode = '"+Code.ToString()+"'";

                var code = Code.ToString();

                var sql = @"UPDATE ItemDesc SET ItemDesc = '" + Desc + "', Cost = '" + Cost + "' WHERE ItemCode = '" + Code + "'";
                // Cmd = new OleDbCommand("UPDATE ItemDesc SET ");

                ////If a new description is provided then add it to the SET clause
                //if (Desc != null)
                //{
                //    Cmd.CommandText += "ItemDesc = '" + Desc + "'";
                //}
                //if (Cost != null)
                //{
                //    //If a new cost is provided as well as desc then add a comma, otherwise just add cost to SET clause
                //    if (Desc != null)
                //    {
                //        Cmd.CommandText += ", Cost = '" + Cost + "'";
                //    }
                //    else
                //    {
                //        Cmd.CommandText += "Cost = '" + Cost + "'";
                //    }
                //}
                //Cmd.CommandText += " WHERE ItemCode = '" + Code + "'";

                //db.ExecuteNonQuery(Cmd);
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
                //Cmd = new OleDbCommand("SELECT DISTINCT InvoiceNum FROM LineItems WHERE ItemCode = @ItemCode");
                //Cmd.Parameters.Add("@ItemCode", OleDbType.WChar).Value = SelectedItem.Code;

                var sql = "SELECT DISTINCT InvoiceNum FROM LineItems WHERE ItemCode = '"+ SelectedItem.Code+"'";

                int retVal = 0;

                //ds = db.ExecuteSQLStatement(Cmd, ref retVal);

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

