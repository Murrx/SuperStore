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

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            if (AuthenticationWindow.customer != null)
            {
                statusLabel.Text = "Connected as: " + AuthenticationWindow.customer.credentials.username;
                statusLabel.Foreground = Brushes.Green;
            }
            else
            {
                statusLabel.Text = "Not connected";
                statusLabel.Foreground = Brushes.Red;
            }
        }
    }
}
