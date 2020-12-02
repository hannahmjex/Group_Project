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
