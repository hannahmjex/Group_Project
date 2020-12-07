using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GroupProject.Items
{
    /// <summary>
    /// All items logic for pulling in the database and editing all fields appropiately
    /// </summary>
    class clsItemsLogic
    {
        /// <summary>
        /// Items Sql Object Declaration
        /// </summary>
        private clsItemsSQL SQL;

        /// <summary>
        /// Private Items Observable Collection Declaration
        /// </summary>
        ObservableCollection<Item> _Items;

        /// <summary>
        /// Public getter/setter for the _Items collection
        /// </summary>
        public ObservableCollection<Item> Items
        {
            get
            {
                return this._Items;
            }
            set
            {
                if (value != this._Items)
                    this._Items = value;
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public clsItemsLogic()
        {
            try
            {
                SQL = new clsItemsSQL();
                //Initialize the items collection to all the items
                _Items = SQL.GetAllItems();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Goes to the database and gets all items
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Item> GetAllItems()
        {
            try
            {
                return SQL.GetAllItems();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Checking the uniqueness of the Item Code
        /// </summary>
        /// <param name="codeCandidate">Item's Code</param>
        /// <returns>boolean representing uniqueness</returns>
        public bool IsValidItemCode(string codeCandidate)
        {
            try
            {
                //get ds with parameter code
                var itemDataSet = SQL.CheckUniqueCode(codeCandidate);

                //if there is nothing in the ds, then the code can be used
                if (itemDataSet.Tables[0].Rows.Count == 0)
				{
                    return true;
				}
				else
				{
                    return false;
				}
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Inserts a new Item into the DB
        /// </summary>
        /// <param name="Code">Item's Code</param>
        /// <param name="Desc">Item's Description</param>
        /// <param name="Cost">Item's Cost</param>
        /// <returns>The inserted Item</returns>
        public Item InsertNewItem(string Code, string Desc, string Cost)
        {
            try
            {
                SQL.InsertNewItem(Code, Desc, Cost);
                Item newItem = new Item(Code, Desc, Cost);
                
                _Items.Add(newItem);

                return newItem;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the provided Item
        /// </summary>
        /// <param name="SelectedItem">Item To Delete</param>
        public void DeleteItem(object SelectedItem)
        {
            try
            {
                SQL.DeleteItem(((Item)SelectedItem).Code.ToString());
                _Items.Remove((Item)SelectedItem);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Update provided item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Desc"></param>
        /// <param name="Cost"></param>
        public void UpdateItem(Item item, string Desc, string Cost)
        {
            try
            {
                SQL.UpdateItem(item.Code, Desc, Cost);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Refreshes the items in the Item Collection
        /// </summary>
        public void RefreshItems()
        {
            try
            {
                _Items.Clear();
                foreach (Item i in SQL.GetAllItems())
                {
                    _Items.Add(i);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Checks that the provided text is a number
        /// </summary>
        /// <param name="InputText">string to be checked</param>
        /// <returns>boolean representing if the text is numeric</returns>
        public bool IsNumericInput(string InputText)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+");
                return regex.IsMatch(InputText);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Checks whether the item is included in an invoice
        /// </summary>
        /// <param name="SelectedItem">Item to check for</param>
        /// <returns>string of Invoices the item is included in</returns>
        public string CheckItemOnInvoice(object SelectedItem)
        {
            try
            {
                List<int> invoices = SQL.CheckItemOnInvoice((Item)SelectedItem);

                if (invoices.Count == 0)
                    return "";

                string message = "That item exists on the following invoices and cannot be deleted: \n\n";
                foreach (int i in invoices)
                {
                    message += "\t" + i + "\n";
                }

                return message;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
