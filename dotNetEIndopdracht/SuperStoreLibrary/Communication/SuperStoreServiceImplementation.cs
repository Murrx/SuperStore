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
            //validate parameters
            if (Name == null || Name == "") { throw new FaultException("Name cannot be empty");}
            if (Username == null || Username == "") { throw new FaultException("Username cannot be empty"); }

            SuperStoreModelContainer ModelContainer = ModelContainerProvider.GetInstance();

            //Check if username is still available
            Customer ExistingCustomer = ModelContainer.Customers.FirstOrDefault(user => user.Username == Username);
            if (ExistingCustomer == null){

                String Password = GeneratePassword(Username);

                //Create new customer and add it to the db
                Customer NewCustomer = new Customer{Name = Name, Username = Username, Password = HashPassword(Password)};
                ModelContainer.Customers.Add(NewCustomer);
                ModelContainer.SaveChanges();

                return new AuthenticationCredentials { password = Password, username = Username };
            }
            throw new FaultException("Username is already in use");
        }
        public CustomerContainer RetrieveUserInfo(AuthenticationCredentials Credentials)
        {
            Credentials.Validate();
            SuperStoreModelContainer ModelContainer = ModelContainerProvider.GetInstance();

            string HashedPassword = HashPassword(Credentials.password);
            Customer RequestedCustomer = ModelContainer.Customers.SingleOrDefault(user => user.Username == Credentials.username && user.Password == HashedPassword);

            if (RequestedCustomer == null)
            {
                throw new FaultException("Invallid user credentials");
            }
            return new CustomerContainer(RequestedCustomer);
        }

        public List<ProductContainer> RetrieveAvailableProducts(int PageIndex, int PageSize)
        {
            //validate the parameters
            PageIndex = (PageIndex > 0) ? PageIndex : 1;
            PageSize = (PageSize > 0) ? PageSize : 20;

            SuperStoreModelContainer ModelContainer = ModelContainerProvider.GetInstance();

            var products = (from p in ModelContainer.Products
                            where p.Stock.Amount > 0
                            orderby p.Name
                            select p).
                            Skip((PageIndex- 1) * PageSize).
                            Take(PageSize);

            List<ProductContainer> result = new List<ProductContainer>();
            foreach (Product p in products){
                result.Add(new ProductContainer(p));
            }

            return result;
        }

        public List<PurchaseContainer> RetrievePurchaseHistory(AuthenticationCredentials Credentials, int PageIndex, int PageSize)
        {
            throw new FaultException("not yet implemented");
        }

        public PurchaseContainer PurchaseProduct(AuthenticationCredentials Credentials, PurchaseContainer ProductToBuy)
        {
            throw new FaultException("not yet implemented");
        }

        public static string GeneratePassword(string Username)
        {
            string Password = "";
            foreach (char Char in Username)
            {
                int C = Char;
                C++;
                Password += (char)C;
            }

            return Password;
        }
        public static string HashPassword(string Password)
        {
            return string.Format("{0:X}", Password.GetHashCode());
        }
    }
}
