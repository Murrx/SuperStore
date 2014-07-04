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
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SuperStoreServiceInterfaceClient client = ServiceClientProvider.GetInstance();
                MainWindow.user = client.RetrieveUserInfo(new AuthenticationCredentials { username = usernameBox.Text, password = passwordBox.Text});
                statusLabel.Content = "Welcome " + MainWindow.user.name;
                statusLabel.Foreground = Brushes.Green;
            }
            catch(FaultException ex)
            {
                MainWindow.user = null;
                statusLabel.Content = ex.Message;
                statusLabel.Foreground = Brushes.Red;
            }
        }
    }
}
