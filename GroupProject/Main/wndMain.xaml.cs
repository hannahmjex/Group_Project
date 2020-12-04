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
		List<string> AddedItems;

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
			AddedItems = new List<string>();
			ds = new DataSet();
			FillItemSelectionBox();
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
		}

		/// <summary>
		/// This method is called when the edit invoice button is clicked
		/// it allows the user to edit the current invoice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editButton_Click(object sender, RoutedEventArgs e)
		{

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

			//else
			this.Hide();
			wndItems.ShowDialog();
			this.Show();
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
			//make temporary table
			DataTable table;
			if (dgInvoice.Items.Count == 0)
			{
				table = ds.Tables.Add("Items");
				table.Columns.Add("Code");
				table.Columns.Add("Description");
				table.Columns.Add("Cost");
			}

			table = ds.Tables[0];
			List<string> itemInfo = mainLogic.GetItemRow(cboItemSelection.SelectedItem.ToString());
			table.Rows.Add(itemInfo[0], itemInfo[1], itemInfo[2]);
			dgInvoice.DataContext = table;

			//add description to added items
			AddedItems.Add(itemInfo[0]);

			//update cost textbox 
			//UpdateTotalCost();
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

		}

		/// <summary>
		/// This method is called when the delete invoice button is clicked
		/// It removes the data in the current invoice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteButton_Click(object sender, RoutedEventArgs e)
		{

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
	}
}
