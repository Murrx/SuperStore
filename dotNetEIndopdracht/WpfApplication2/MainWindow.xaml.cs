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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication2.SuperStoreServiceReference;
using System.ServiceModel;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        public static CustomerContainer user;
        public ProductResponseContainer selectedProduct;
        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            SuperStoreServiceInterfaceClient client = ServiceClientProvider.GetInstance();
            Refresh();
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            ServiceClientProvider.CloseClient();
        }

        private void Authenticate_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow window = new AuthenticationWindow();
            window.Show();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            SuperStoreServiceInterfaceClient client = ServiceClientProvider.GetInstance();
            try
            {
                availableProducts.ItemsSource = client.RetrieveAvailableProducts(new Pagination { pageIndex = 1, pageSize = 100 });
            }
            catch (FaultException ex)
            {
                availableProducts.ItemsSource = null;
            }
            if (user != null)
            {
                user = client.RetrieveUserInfo(new AuthenticationCredentials { username = user.credentials.username, password = user.credentials.password });
                statusLabel.Text = "Connected as: " + user.credentials.username + ". Your current saldo = " + user.saldo;
                statusLabel.Foreground = Brushes.Green;
            }
            else
            {
                statusLabel.Text = "Not connected";
                statusLabel.Foreground = Brushes.Red;
            }
        }

        private void availableProducts_Selected(object sender, RoutedEventArgs e)
        {
            if (availableProducts.SelectedItem != null)
            {
                selectedProduct = availableProducts.SelectedItem as ProductResponseContainer;
                List<int> comboFiller = new List<int>();
                for (int i = 1; i <= selectedProduct.stock; i++ )
                {
                    comboFiller.Add(i);
                }
                amountBox.ItemsSource = comboFiller;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                AuthenticationCredentials credentials = user.credentials;
                ProductRequestContainer product = new ProductRequestContainer { id = selectedProduct.id };
                int selectedAmount = 0;
                if (amountBox.SelectedItem != null)
                {
                    selectedAmount = (int)amountBox.SelectedItem;
                }
                SuperStoreServiceInterfaceClient client = ServiceClientProvider.GetInstance();
                try
                {
                    PurchaseResponseContainer purchase = client.PurchaseProduct(credentials, new PurchaseRequestContainer { amount = selectedAmount, product = product });
                    MessageBoxResult result = MessageBox.Show("You succesfully purchased " + purchase.amount + " " + purchase.product.name);
                    Refresh();
                }
                catch (FaultException ex)
                {
                    MessageBoxResult result = MessageBox.Show(ex.Message);   
                }
                
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("You have to be logged in to use buy products");   
            }
            
        }

        private void RefreshMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void RegisterMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().Show();
        }
    }
}
