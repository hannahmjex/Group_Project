using System;
using System.Reflection;

namespace GroupProject
{
    /// <summary>
    /// class that represents a line item on an invoice
    /// </summary>
    public class LineItem
    {
        public LineItem(int lineItemNumber, string itemCode)
        {
            LineItemNumber = lineItemNumber;
            ItemCode = itemCode;
        }

        /// <summary>
        /// The invoice number of the invoice this line item belongs to
        /// </summary>
        public string InvoiceNumber { get; private set; }

        /// <summary>
        /// The number of the line on the invoice where this line item exists
        /// </summary>
        public int LineItemNumber { get; set; }

        /// <summary>
        /// The code of the item that this line item shows on the invoice
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Sets the InvoiceNumber property of the line item
        /// </summary>
        /// <param name="invoiceNumber"></param>
        public void AddInvoiceNumber(string invoiceNumber)
        {
            try
            {
                if(invoiceNumber == null)
                {
                    throw new Exception("Internal Error: provided invoice number must have value");
                }

                InvoiceNumber = invoiceNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}

