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

        public AuthenticationCredentials RegisterNewUser(CustomerContainer RegisterCustomer)
        {
            SuperStoreModelContainer ModelContainer = ModelContainerProvider.GetInstance();

            //Check if username is still available
            Customer ExistingCustomer = ModelContainer.Customers.FirstOrDefault(user => user.Username == RegisterCustomer.credentials.username);
            if (ExistingCustomer == null){
                Customer NewCustomer = new Customer{Name = RegisterCustomer.name, Username = RegisterCustomer.credentials.username, Password = GeneratePassword(RegisterCustomer.credentials.password)};
                //add something to generate&encrypt password

                ModelContainer.Customers.Add(NewCustomer);
                ModelContainer.SaveChanges();

                return RegisterCustomer.credentials;
            }
            throw new FaultException("not yet implemented");
        }

        private string GeneratePassword(String Username){
            string Password = "";

            foreach(char Char in Username){
                int C = Char;
                Password = C++.ToString();
            }

            return Password;
        }

        public CustomerContainer RetrieveUserInfo(AuthenticationCredentials Credentials)
        {
            throw new FaultException("not yet implemented");
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
