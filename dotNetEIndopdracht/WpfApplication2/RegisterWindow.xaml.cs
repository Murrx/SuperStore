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
using WpfApplication2.SuperStoreServiceReference;
using System.ServiceModel;

namespace WpfApplication2
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SuperStoreServiceInterfaceClient client = ServiceClientProvider.GetInstance();
            RegistrationContainer newUser = new RegistrationContainer{name = nameBox.Text, username=usernameBox.Text};
            AuthenticationCredentials credentials = null;
            try{
                credentials = client.RegisterNewUser(newUser);
                MessageBox.Show("Successfully registered with username \"" + credentials.username + "\" your password is \"" + credentials.password + "\"");
                MainWindow.user = client.RetrieveUserInfo(new AuthenticationCredentials { username = credentials.username, password = credentials.password});
            }
            catch(FaultException ex)
            {
                MessageBox.Show(ex.Message);   
            }
        }
    }
}
