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
using System.ServiceModel;
using WpfApplication2.SuperStoreServiceReference;

namespace WpfApplication2
{
    public partial class AuthenticationWindow : Window
    {
        public static CustomerContainer customer;
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SuperStoreServiceInterfaceClient client = new SuperStoreServiceInterfaceClient();
            try
            {
                //uftu
                customer = client.RetrieveUserInfo(new AuthenticationCredentials { username = usernameBox.Text, password = passwordBox.Text});
                statusLabel.Content = "Welcome " + customer.name;
                statusLabel.Foreground = Brushes.Green;
            }
            catch(FaultException ex)
            {
                customer = null;
                statusLabel.Content = ex.Message;
                statusLabel.Foreground = Brushes.Red;
            }
            
            client.Close();
        }
    }
}
