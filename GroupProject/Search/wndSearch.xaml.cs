using GroupProject.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupProject
{
	/// <summary>
	/// Interaction logic for SearchWindow.xaml
	/// </summary>
	public partial class SearchWindow : Window
	{
        /// <summary>
        /// Declare clsSearchLogic property to call search logic methods
        /// </summary>
        clsSearchLogic clsSearchLogic;

		public SearchWindow()
		{
            try
            {
                InitializeComponent();

                //Instantiate a new search logic class
                clsSearchLogic = new clsSearchLogic();

                //Fill up the invoices data grid using the search logic class
                FillInvoicesDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
		}

        /// <summary>
        /// Fills up the Invoices data grid initially with all invoices and also fills the search combo boxes appropriately.
        /// </summary>
        private void FillInvoicesDataGrid()
        {
            try
            {
                var allInvoices = clsSearchLogic.GetAllInvoices();

                //Fill search window UI components with invoice information 
                dataGridInvoices.ItemsSource = allInvoices;
                comboBoxInvoiceNum.ItemsSource = allInvoices;
                comboBoxInvoiceDate.ItemsSource = allInvoices;
                comboBoxInvoiceTotalCharge.ItemsSource = allInvoices;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Reset what the Invoices Data Grid shows based on the invoice number selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxInvoiceNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Reset what the Invoices Data Grid shows based on the invoice date selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Reset what the Invoices Data Grid shows based on the total charge selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxInvoiceTotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// exception handler that shows the error
        /// </summary>
        /// <param name="sClass">the class</param>
        /// <param name="sMethod">the method</param>
        /// <param name="sMessage">the error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
