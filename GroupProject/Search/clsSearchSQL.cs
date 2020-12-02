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

namespace GroupProject.Search
{
	public class clsSearchSQL
	{
        /// <summary>
        /// Declare data access class property
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Constructs a new clsSearchSQL class that will communicate with the data base to make queries for the search screen
        /// </summary>
        public clsSearchSQL()
        {
            db = new clsDataAccess();
        }

        /// <summary>
        /// Returns select all invoices SQL Query
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Invoice> GetAllInvoices()
        {
            try
            {
                // Create local invoices collection
                ObservableCollection<Invoice> invoices = new ObservableCollection<Invoice>();

                // Create DataSet to hold the data
                DataSet ds;

                //Number of return values
                int iRet = 0;

                //Sql string to get all invoices
                string sql = "SELECT * FROM Invoices";

                // Get all invoices from the database.
                ds = db.ExecuteSQLStatement(sql, ref iRet);

                // Iterate over rows
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // Grab data from their columns for each row
                    int invoiceNum = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                    string invoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    string totalCost = ds.Tables[0].Rows[i][2].ToString();

                    // Create new local invoice object
                    Invoice invoice = new Invoice(invoiceNum, invoiceDate, totalCost);

                    // Add newly created invoice object to local collection of invoices.
                    invoices.Add(invoice);
                }

                // Return invoices
                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                       MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
