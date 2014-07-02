using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SuperStoreLibrary.Model;

namespace SuperStoreLibrary.Communication
{
    public class SuperStoreServiceImplementation : SuperStoreServiceInterface
    {
        public AuthenticationCredentials RegisterNewUser(String Name, String Username)
        {
            SuperStoreModelContainer ModelContainer = ModelContainerProvider.GetInstance();

            //Check if username is still available
            Customer ExistingCustomer = ModelContainer.Customers.FirstOrDefault(user => user.Username == Username);
            if (ExistingCustomer == null){

                String Password = GeneratePassword(Username);

                Customer NewCustomer = new Customer{Name = Name, Username = Username, Password = HashPassword(Password)};

                ModelContainer.Customers.Add(NewCustomer);
                ModelContainer.SaveChanges();

                return new AuthenticationCredentials { password = Password, username = Username };
            }
            throw new FaultException("Username is already in use");
        }

        public static string GeneratePassword(string Username){
            string Password = "";
            foreach(char Char in Username){
                int C = Char;
                C++;
                Password += (char)C;
            }

            return Password;
        }

        private static string HashPassword(string Password)
        {
            return string.Format("{0:X}",Password.GetHashCode());
        }

        public CustomerContainer RetrieveUserInfo(AuthenticationCredentials Credentials)
        {
            SuperStoreModelContainer ModelContainer = ModelContainerProvider.GetInstance();

            Customer RequestedCustomer = ModelContainer.Customers.SingleOrDefault(user => user.Username == Credentials.username && user.Password == Credentials.password);

            if (RequestedCustomer == null)
            {
                throw new FaultException("not yet implemented");
            }
            return RequestedCustomer;
        }

        public List<ProductContainer> RetrieveAvailableProducts(int PageIndex, int PageSize)
        {
            throw new FaultException("not yet implemented");
        }

        public List<PurchaseContainer> RetrievePurchaseHistory(AuthenticationCredentials Credentials, int PageIndex, int PageSize)
        {
            throw new FaultException("not yet implemented");
        }

        public PurchaseContainer PurchaseProduct(AuthenticationCredentials Credentials, PurchaseContainer ProductToBuy)
        {
            throw new FaultException("not yet implemented");
        }
    }
}
