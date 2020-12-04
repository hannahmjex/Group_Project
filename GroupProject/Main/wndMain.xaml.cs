using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace GroupProject
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// wndItems class
		/// </summary>
		wndItems wndItems;

		/// <summary>
		/// wndSearch class
		/// </summary>
		SearchWindow wndSearch;

		/// <summary>
		/// main window sql class
		/// </summary>
		clsMainLogic mainLogic;

		/// <summary>
		/// exception handling class
		/// </summary>
		clsExceptionHandling exceptionHandling;

		/// <summary>
		/// list of added items
		/// </summary>
		List<string> addedItems;

		/// <summary>
		/// boolean to tell if the invoice is being edited
		/// </summary>
		bool editing;

		/// <summary>
		/// bools to tell if the invoice has been saved
		/// </summary>
		bool saved;

		/// <summary>
		/// list of item information
		/// </summary>
		List<string> itemInfo;

		/// <summary>
		/// total cost
		/// </summary>
		int total;

		/// <summary>
		/// Constructor for Main Window
		/// </summary>
		public MainWindow()
		{
			try
			{
				InitializeComponent();
				Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
				wndItems = new wndItems();
				wndSearch = new SearchWindow(this);
				mainLogic = new clsMainLogic();
				exceptionHandling = new clsExceptionHandling();
				addedItems = new List<string>();
				itemInfo = new List<string>();
				addedItems = new List<string>();
				total = 0;
				FillItemSelectionBox();
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is called when the new invoice button is clicked
		/// it enables the rest of the screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//if there are items in the datagrid
				if (dgInvoice.Items.Count != 0)
				{
					dgInvoice.Items.Clear();
				}
				//enable other features
				SetIsEnabled(true);

				//set total text box to 0
				total = 0;

				//set saved to false
				saved = false;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is called when the edit invoice button is clicked
		/// it allows the user to edit the current invoice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//enable datagrid
				dgInvoice.IsEnabled = true;
				//set global editing to true
				editing = true;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is only enabled  when an item is selected from the datagrid
		/// It is called when the delete invoice button is selected
		/// It removes the selected item from the datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void removeItemButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//Make sure the current row is not null
				if (dgInvoice.SelectedItems != null)
				{
					//remove selected item
					dgInvoice.Items.Remove(dgInvoice.SelectedItems[0]);
					UpdateTotalCost(false);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is called when the Update menu option is clicked
		/// It allows the user to update a def table that contains the items
		/// This button can only be clicked when an invoice is not being edited
		/// or if a new invoice is not being entered
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpdateMenuItem_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//if invoice is being edited/added
				//DO NOTHING

				if (editing)
				{
					MessageBox.Show("Please finish editing before moving on");
				}
				else if (!saved)
				{
					MessageBox.Show("Please save the invoice before moving on");
				}
				//else
				else
				{
					this.Hide();
					wndItems.ShowDialog();
					this.Show();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is called when the search menu option is clicked
		/// It opens the search window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchMenuItem_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.Hide();

				if (wndSearch != null)
				{
					wndSearch = null;
				}

				wndSearch = new SearchWindow(this);

				wndSearch.ShowDialog();

				this.Show();
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is called when the add item button is clicked
		/// it adds the item selected in the combo box to the datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//get item info
				var itemDescription = cboItemSelection.SelectedItem.ToString();
				itemInfo = mainLogic.GetItemRow(itemDescription);

				//add items to data grid
				dgInvoice.Items.Add(new Item(itemInfo[0], itemInfo[1], itemInfo[2]));

				//add description to added items
				addedItems.Add(itemInfo.ToString());

				//Update total cost 
				UpdateTotalCost(true);
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// Shows items from selected invoice from wndSearch
		/// </summary>
		/// <param name="invoiceNum"></param>
		public void ShowSelectedInvoiceItems(int invoiceNum)
		{
			var items = mainLogic.GetItemsForInvoice(invoiceNum.ToString());

			SetIsEnabled(true);
			editButton.IsEnabled = true;
			deleteButton.IsEnabled = true;
			dgInvoice.IsEnabled = false;

			dgInvoice.ItemsSource = items;
		}

		/// <summary>
		/// This method is called when the save invoice button is clicked
		/// After a date is entered for the invoice, it is saved
		/// Invoice number label gets filled in
		/// Invoice goes into read only mode 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//if the data is not being edited
				if (!editing)
				{
					//if no date entered
					if (invoiceDate.Text == "")
					{
						MessageBox.Show("Please select a date.");
					}
					//if nothing added to invoice
					else if (dgInvoice.Items.Count == 0)
					{
						MessageBox.Show("Please add an item");
					}
					else
					{
						//save invoice
						mainLogic.SaveInvoice(invoiceDate.SelectedDate.ToString(), costTextbox.Text);
						//set invoice number
						invoiceNum.Content = mainLogic.GetInvoiceNumber();

						//change invoice to read only mode
						dgInvoice.IsEnabled = false;
						//enable delete and edit buttons
						deleteButton.IsEnabled = true;
						editButton.IsEnabled = true;
					}
				}
				//if the data is being edited
				else
				{
					//update invoice
					mainLogic.UpdateInvoice(costTextbox.Text);
					//end editing
					editing = false;
				}
				//set saved to true
				saved = true;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is called when the delete invoice button is clicked
		/// It removes the data in the current invoice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				dgInvoice.Items.Clear();
				//call sql statement to delete invoice
				mainLogic.DeleteInvoice();

				//disable features
				cboItemSelection.IsEnabled = false;
				addItemButton.IsEnabled = false;
				saveButton.IsEnabled = false;
				invoiceDate.IsEnabled = false;
				editButton.IsEnabled = false;
				invoiceDate.IsEnabled = false;

				//reset features
				invoiceDate.SelectedDate = null;
				cboItemSelection.SelectedItem = "";
				costTextbox.Text = "";

			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method is called when the item selection is changed
		/// It dispalys the cost of the selected item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboItemSelection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			try
			{
				var items = mainLogic.GetAllItems();

				for (int i = 0; i < items.Count; i++)
				{
					if (cboItemSelection.SelectedIndex == i)
					{
						costTextbox.Text = items[i].ItemCost;
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// This method enablese the remove item button after a row is selected in the datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgInvoice_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			try
			{
				removeItemButton.IsEnabled = true;
			}
			catch (Exception ex)
			{
				throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
									   MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
			}
		}

		/// <summary>
		/// this method fills the item selection combo box
		/// </summary>
		private void FillItemSelectionBox()
		{
			try
			{
				var items = mainLogic.GetAllItems();
				cboItemSelection.ItemsSource = items.Select(x => x.Description).Distinct();
			}
			catch (Exception ex)
			{
				exceptionHandling.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
							MethodInfo.GetCurrentMethod().Name, ex.Message);
			}
		}

		/// <summary>
		/// This method updates the total cost when a new item is added to the invoice
		/// </summary>
		private void UpdateTotalCost(bool added)
		{
			try
			{
				//if jtem was added
				if (added)
				{
					//add most recently added item
					total += Int32.Parse(itemInfo[2]);
				}
				//if item was removed
				else
				{
					total -= Int32.Parse(itemInfo[2]);
				}
				//make sure total doesn't go less than 0
				if (total < 0)
				{
					totalTextbox.Text = 0.ToString();
				}
				else
				{
					totalTextbox.Text = total.ToString();
				}
			}
			catch (Exception ex)
			{
				exceptionHandling.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
							MethodInfo.GetCurrentMethod().Name, ex.Message);
			}
		}

		/// <summary>
		/// Set IsEnabled property for contol elements
		/// </summary>
		/// <param name="isEnabled"></param>
		private void SetIsEnabled(bool isEnabled)
		{
			cboItemSelection.IsEnabled = isEnabled;
			addItemButton.IsEnabled = isEnabled;
			saveButton.IsEnabled = isEnabled;
			invoiceDate.IsEnabled = isEnabled;
		}
	}
}
