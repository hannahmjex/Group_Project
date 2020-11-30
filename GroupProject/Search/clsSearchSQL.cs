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
    }
}
