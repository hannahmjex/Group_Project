using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Search
{
	public class clsSearchSQL
	{
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;

        /// <summary>
        /// Constructor that sets the connection string to the database
        /// </summary>
		public clsSearchSQL()
        {
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Books.mdb";
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
		public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
        {
            try
            {
                //Create a new DataSet
                DataSet ds = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                iRetVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
		public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns select all invoices SQL Query
        /// </summary>
        /// <returns></returns>
        public string GetAllInvoices()
        {
            return "SELECT * FROM Invoices";
        }

        /// THIS MAY NOT BE NEEDED
        /// <summary>
        /// Returns query string that will get an invoice out of the database by its invoice number.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string GetInvoiceByInvoiceNum(string invoiceNum)
        {
            return $"SELECT * FROM Invoices WHERE InvoicNum = {invoiceNum}";
        }

        /// <summary>
        /// Get invoices by filtered by invoice number and invoice date
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public string GetInvoicesByInvoiceNumAndInvoiceDate(string invoiceNum, string invoiceDate)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceDate} AND InvoiceDate = {invoiceDate}";
        }

        /// <summary>
        /// Get invoices filtered by invoice number, invoice date, and total cost.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public string GetInvoicesByInvoiceNumInvoiceDateAndTotalCost(string invoiceNum, string invoiceDate, string totalCost)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = {invoiceDate} AND TotalCost = {totalCost}";
        }

        /// <summary>
        /// Get invoices filtered by total cost
        /// </summary>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public string GetInvoicesByTotalCost(string totalCost)
        {
            return $"SELECT * FROM Invoices WHERE TotalCost = {totalCost}";
        }

        /// <summary>
        /// Get invoices filtered by total cost and invoice date
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public string GetInvoicesByTotalCostAndInvoiceDate(string totalCost, string invoiceDate)
        {
            return $"SELECT * FROM Invoices WHERE TotalCost = {totalCost} and InvoiceDate = {invoiceDate}";
        }

        /// <summary>
        /// Get invoices filtered by invoice date
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public string GetInvoicesByInvoiceDate(string invoiceDate)
        {
            return $"SELECT * FROM Invoices WHERE InvoiceDate = {invoiceDate}";
        }
    }
}
