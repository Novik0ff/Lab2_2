using Lab2._2.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace Lab2._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ViewUser> viewUsers;
        List<Account.Account> accounts;
        public MainWindow()
        {
            ExceptionHandler.ExceptionHandler.AddUnhandledExceptionHandler();
            InitializeComponent();
            UpdateDataGrid();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewUser temp = (ViewUser)LabDataGrid.Items[LabDataGrid.SelectedIndex];

                accounts[LabDataGrid.SelectedIndex].Name = temp.Name;
                accounts[LabDataGrid.SelectedIndex].Address1City = temp.City;
                accounts[LabDataGrid.SelectedIndex].Address1Composite = temp.Region;
                accounts[LabDataGrid.SelectedIndex].Address1Line1 = temp.Address;
                accounts[LabDataGrid.SelectedIndex].UpdateAccountCRM(Service.Service.GetOrganization());

                UpdateDataGrid();

            }
            catch (Exception)
            {
                MessageBox.Show("Заполните все поля в строке");
            }
        }
        private void Show(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
        private void Create(object sender, RoutedEventArgs e)
        {
            //try
            //{
                ViewUser temp = (ViewUser)LabDataGrid.Items[LabDataGrid.SelectedIndex];
                new Account.Account
                {
                    Name = temp.Name.ToString(),
                    Address1City = temp.City.ToString(),
                    Address1Line1 = temp.Address.ToString(),
                    Address1Composite = temp.Region.ToString()
                }.ExportAccountToCRM(Service.Service.GetOrganization());
                UpdateDataGrid();
            //}
           //new throw;
            //catch (Exception)
            //{
                
            //    //MessageBox.Show("Заполните все поля в строке");
            //}
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                accounts[LabDataGrid.SelectedIndex].DeleteAccountFromCRM(Service.Service.GetOrganization());
            }
            catch (Exception)
            {
                MessageBox.Show("Select row empty");
            }
            Account.Account.ImportAccountFromCRM(Service.Service.GetOrganization(), out accounts);
            UpdateDataGrid();
        }

        private bool UpdateDataGrid()
        {
            try
            {
                Account.Account.ImportAccountFromCRM(Service.Service.GetOrganization(), out accounts);
                viewUsers = new ObservableCollection<ViewUser>();
                foreach (var item in accounts)
                {
                    viewUsers.Add(new ViewUser { Name = item.Name, Address = item.Address1Line1, City = item.Address1City, Region = item.Address1Composite });
                }
                LabDataGrid.ItemsSource = viewUsers;
                return true;
            }
            catch (Exception)
            {
                throw new ArgumentException($@"Error update datagrid");
            }
        }

    }
}
