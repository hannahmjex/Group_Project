﻿using GroupProject.Main;
using GroupProject.Search;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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

        /// <summary>
        /// Declare a main window object
        /// </summary>
        MainWindow MainWindow;

        public SearchWindow(MainWindow mainWindow)
		{
            try
            {
                InitializeComponent();

                //Instantiate a new search logic class
                clsSearchLogic = new clsSearchLogic();
                MainWindow = mainWindow;

                //Fill up the invoices data grid using the search logic class
                InitializeInvoicesDataGrid();
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
        private void InitializeInvoicesDataGrid()
        {
            try
            {
                var allInvoices = clsSearchLogic.GetAllInvoices();

                //Fill search window UI components with invoice information 
                dataGridInvoices.ItemsSource = allInvoices;
                comboBoxInvoiceNum.ItemsSource = allInvoices.Select(x => x.InvoiceNum).Distinct();
                comboBoxInvoiceDate.ItemsSource = allInvoices.OrderByDescending(x => x.InvoiceDate).Select(x => x.InvoiceDate).Distinct();
                comboBoxInvoiceTotalCharge.ItemsSource = allInvoices.OrderBy(x => x.TotalCost).Select(x => x.TotalCost).Distinct();

                comboBoxInvoiceNum.SelectedItem = null;
                comboBoxInvoiceDate.SelectedItem = null;
                comboBoxInvoiceTotalCharge.SelectedItem = null;
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
            try
            {
                comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Reset what the Invoices Data Grid shows based on the invoice date selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Reset what the Invoices Data Grid shows based on the total charge selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxInvoiceTotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                comboBox_SelectionChanged();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method gets called whenever a combo box's selection gets changed. It resets the invoices data grid to show the filtered invoices based on the combobox selections
        /// </summary>
        private void comboBox_SelectionChanged()
        {
            try
            {
                //Get all combo box selected items
                string invoiceNumber = comboBoxInvoiceNum.SelectedItem?.ToString();
                string invoiceDate = comboBoxInvoiceDate.SelectedItem?.ToString();
                string totalCost = comboBoxInvoiceTotalCharge.SelectedItem?.ToString();

                //Get filtered invoices from the database
                ObservableCollection<Invoice> filteredInvoices = clsSearchLogic.GetFilteredInvoices(invoiceNumber, invoiceDate, totalCost);

                //Reset the Invoices data grid with the filtered invoices
                dataGridInvoices.ItemsSource = filteredInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                       MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resets the filtered data to its original state (all invoices)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetFilters_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InitializeInvoicesDataGrid();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Takes the selected invoice to the main screen to be edited or deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //clear out main window datagrid
                MainWindow.dgInvoice.ItemsSource = new ObservableCollection<Item>();

                var invoice = (Invoice)dataGridInvoices.SelectedItem;

                MainWindow.ShowSelectedInvoiceItems(invoice);

                this.Hide();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Ensures that the Select Invoice button is enabled at the appropriate times.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGridInvoices.SelectedItem != null)
                {
                    btnSelectInvoice.IsEnabled = true;
                }
                else
                {
                    btnSelectInvoice.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
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
