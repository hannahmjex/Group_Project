using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Search
{
	public class clsSearchLogic
	{

        /// <summary>
        /// Create an object of type clsDataAccess to access the database
        /// </summary>
        clsSearchSQL searchSQL;

        /// <summary>
        /// Constructs a clsSearchLogic object which will contain methods for searching for invoices.
        /// </summary>
        public clsSearchLogic()
        {
            //Instantiate the data access class
            searchSQL = new clsSearchSQL();
        }

        /// <summary>
        /// returns an ObservableCollection of all invoices in the database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Invoice> GetAllInvoices()
        {
            try
            {
                return searchSQL.GetAllInvoices();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        internal ObservableCollection<Invoice> GetFilteredInvoices(string invoiceNumber, string invoiceDate, string totalCost)
        {
            try
            {
                return searchSQL.GetFilteredInvoices(invoiceNumber, invoiceDate, totalCost);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
