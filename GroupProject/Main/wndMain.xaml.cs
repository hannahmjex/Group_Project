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
		/// Constructor for Main Window
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			wndItems = new wndItems();
			wndSearch = new SearchWindow();
		}

		/// <summary>
		/// This method is called when the new invoice button is clicked
		/// it enables the rest of the screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newButton_Click(object sender, RoutedEventArgs e)
		{

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
	}
}
