﻿using System;
using System.Reflection;

namespace GroupProject
{
    /// <summary>
    /// Class that represents an Invoice object
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Constructs an invoice object that takes in an invoice number, date and total cost in as parameters
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        public Invoice(int invoiceNum, DateTime invoiceDate, int totalCost)
        {
            try
            {
                InvoiceNum = invoiceNum;
                InvoiceDate = invoiceDate;
                TotalCost = totalCost;
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
        public int InvoiceNum { get; set; }

        /// <summary>
        ///  Date on the invoice object
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// int of the total cost of the invoice
        /// </summary>
        public int TotalCost { get; set; }
    }
}