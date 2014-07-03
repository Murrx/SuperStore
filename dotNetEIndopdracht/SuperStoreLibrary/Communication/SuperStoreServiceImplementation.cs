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
        public AuthenticationCredentials RegisterNewUser(RegistrationContainer userDetails)
        {
            userDetails.Validate();
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            //Create new customer and add it to the db
            string password = GeneratePassword(userDetails.username);
            Customer newCustomer = new Customer{name = userDetails.name, username = userDetails.username, password = HashPassword(password)};
            modelContainer.Customers.Add(newCustomer);
            modelContainer.SaveChanges();

            return new AuthenticationCredentials { password = password, username = userDetails.username };
        }
        public CustomerContainer RetrieveUserInfo(AuthenticationCredentials credentials)
        {
            Customer requestedCustomer = credentials.ValidateAndRetrieve();
            return new CustomerContainer(requestedCustomer);
        }

        public List<ProductContainer> RetrieveAvailableProducts(Pagination pagination)
        {
            pagination.Validate();
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            var products = (from product in modelContainer.Products
                            where product.stock.amount > 0
                            orderby product.name
                            select product).
                            Skip((pagination.pageIndex- 1) * pagination.pageSize).
                            Take(pagination.pageSize);

            List<ProductContainer> result = new List<ProductContainer>();
            foreach (Product product in products){
                result.Add(new ProductContainer(product));
            }
            return result;
        }

        public List<PurchaseContainer> RetrievePurchaseHistory(AuthenticationCredentials credentials, Pagination pagination)
        {
            Customer requestedCustomer = credentials.ValidateAndRetrieve();
            pagination.Validate();
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            var purchases = (from purchase in modelContainer.Purchases
                            where purchase.customer.username == credentials.username
                            orderby purchase.date
                            select purchase).
                            Skip((pagination.pageIndex - 1) * pagination.pageSize).
                            Take(pagination.pageSize);

            throw new FaultException("not yet implemented");
        }

        public PurchaseContainer PurchaseProduct(AuthenticationCredentials credentials, PurchaseContainer productToBuy)
        {
            

            throw new FaultException("not yet implemented");
        }

        public static string GeneratePassword(string username)
        {
            string password = "";
            foreach (char c in username)
            {
                password += (char)c+1;
            }
            return password;
        }
        public static string HashPassword(string password)
        {
            return string.Format("{0:X}", password.GetHashCode());
        }

        public void AddProduct(ProductContainer product, int amount)
        {
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            Product newProduct = new Product { name = product.name, price = product.price, stock = new Stock { amount = amount } };
            modelContainer.Products.Add(newProduct);
            modelContainer.SaveChanges();
        }
    }
}
