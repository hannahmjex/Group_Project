using System;
using System.Collections.Generic;
using System.Linq;
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
		public SearchWindow()
		{
			InitializeComponent();
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
    }
}
