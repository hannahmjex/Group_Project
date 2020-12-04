using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
		/// dataset ds
		/// </summary>
		DataSet ds;

		/// <summary>
		/// observable collection of items
		/// </summary>
		List<string> items;

		List<string> addedItems;
		/// <summary>
		/// boolean to tell if the invoice is being edited
		/// </summary>
		bool editing;

		List<string> itemInfo;

		int total;



		/// <summary>
		/// Constructor for Main Window
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			wndItems = new wndItems();
			wndSearch = new SearchWindow();
			mainLogic = new clsMainLogic();
			addedItems = new List<string>();
			itemInfo = new List<string>();
			items = new List<string>();
			ds = new DataSet();
			addedItems = new List<string>();
			total = 0;
			FillItemSelectionBox();
		}

		/// <summary>
		/// Called when the form is loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WindowBinding_Loaded(object sender, RoutedEventArgs e)
		{
			//Bind the DataGrids to the ObservableCollections
			dgInvoice.ItemsSource = items;
		}

		/// <summary>
		/// this method fills the item selection combo box
		/// </summary>
		private void FillItemSelectionBox()
		{
			var items = mainLogic.GetAllItems();
			cboItemSelection.ItemsSource = items;
		}

		/// <summary>
		/// This method is called when the new invoice button is clicked
		/// it enables the rest of the screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newButton_Click(object sender, RoutedEventArgs e)
		{
			//enable other features
			cboItemSelection.IsEnabled = true;
			addItemButton.IsEnabled = true;
			editButton.IsEnabled = true;
			saveButton.IsEnabled = true;
			total = 0;
		}

		/// <summary>
		/// This method is called when the edit invoice button is clicked
		/// it allows the user to edit the current invoice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editButton_Click(object sender, RoutedEventArgs e)
		{
			//make invoice not read only
			dgInvoice.IsReadOnly = false;
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
			//Make sure the current row is not null
			if (dgInvoice.SelectedItems != null)
			{
				//remove selected item
				dgInvoice.Items.Remove(dgInvoice.SelectedItems[0]);
				UpdateTotalCost(false);
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
			//if invoice is being edited/added
			//DO NOTHING

			if (editing)
			{
				MessageBox.Show("Please finish editing before moving on");
			}
			//else
			else
			{
				this.Hide();
				wndItems.ShowDialog();
				this.Show();
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
			this.Hide();
			wndSearch.ShowDialog();
			this.Show();
		}

		/// <summary>
		/// This method is called when the add item button is clicked
		/// it adds the item selected in the combo box to the datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addButton_Click(object sender, RoutedEventArgs e)
		{
			//get item info
			var selectedItem = (Item)cboItemSelection.SelectedItem;
			itemInfo = mainLogic.GetItemRow(selectedItem.ItemDesc);

			//add items to data grid
			dgInvoice.Items.Add(new Item(itemInfo[0], itemInfo[1], itemInfo[2]));

			//add description to added items
			addedItems.Add(itemInfo.ToString());

			//Update total cost 
			UpdateTotalCost(true);
		}

		/// <summary>
		/// This method updates the total cost when a new item is added to the invoice
		/// </summary>
		private void UpdateTotalCost(bool added)
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

		/// <summary>
		/// This method is called when the save invoice button is clicked
		/// After a date is entered for the invoice in the datagrid, it is saved
		/// Invoice number label gets filled in
		/// Invoice goes into read only mode 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveButton_Click(object sender, RoutedEventArgs e)
		{
			//if no date entered
			if (invoiceDateTextbox.Text == "DD/MM/YYYY" || invoiceDateTextbox.Text == "")
			{
				MessageBox.Show("Please enter a date.");
			}
			//if nothing added to invoice
			else if (dgInvoice.Items.Count == 0)
			{
				MessageBox.Show("Please add an item");
			}
			else
			{
				//fill in invoice label
				//call sql to get invoice num

				//change invoice to read only mode
				dgInvoice.IsReadOnly = true;
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
			dgInvoice.Items.Clear();
			//call sql statement to delete invoice
		}

		/// <summary>
		/// This method is called when the item selection is changed
		/// It dispalys the cost of the selected item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboItemSelection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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

		/// <summary>
		/// This method enablese the remove item button after a row is selected in the datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgInvoice_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			removeItemButton.IsEnabled = true;
		}
	}
}
