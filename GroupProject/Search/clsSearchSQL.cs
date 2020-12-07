using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;

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
                    DateTime invoiceDate = DateTime.Parse(ds.Tables[0].Rows[i][1].ToString());
                    int totalCost = (int)ds.Tables[0].Rows[i][2];

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

        /// <summary>
        /// Takes in invoice number, invoice date and total cost filters and gets invoices from the data base based on the filters that are passed in
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        internal ObservableCollection<Invoice> GetFilteredInvoices(string invoiceNumber, string invoiceDate, string totalCost)
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

                //list of string conditions to be concatenated to the sql statement 
                var conditions = new List<string>();

                //booleans that determine the existance of the filters passed in to this method
                var invoiceNumFilterExists = !string.IsNullOrWhiteSpace(invoiceNumber);
                var invoiceDateFilterExists = !string.IsNullOrWhiteSpace(invoiceDate);
                var totalCostFilterExists = !string.IsNullOrWhiteSpace(totalCost);

                if (invoiceNumFilterExists)
                    conditions.Add("InvoiceNum = @invoiceNumber");

                if (invoiceDateFilterExists)
                    conditions.Add("InvoiceDate = @invoiceDate");

                if (totalCostFilterExists)
                    conditions.Add("TotalCost = @totalCost");

                if (conditions.Any())
                    sql += " WHERE " + string.Join(" AND ", conditions);


                // Get all invoices from the database.
                ds = db.ExecuteSQLStatement(sql, ref iRet, (OleDbCommand cmd) => 
                {
                    if (invoiceNumFilterExists)
                        cmd.Parameters.AddWithValue("@invoiceNumber", invoiceNumber);

                    if (invoiceDateFilterExists)
                        cmd.Parameters.AddWithValue("@invoiceDate", invoiceDate);

                    if (totalCostFilterExists)
                        cmd.Parameters.AddWithValue("@totalCost", totalCost);
                });

                // Iterate over rows
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // Grab data from their columns for each row and create new local invoice object
                    Invoice invoice = new Invoice
                        (
                            invoiceNum: int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                            invoiceDate: DateTime.Parse(ds.Tables[0].Rows[i][1].ToString()),
                            totalCost: (int)ds.Tables[0].Rows[i][2]
                        );

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
    }
}
