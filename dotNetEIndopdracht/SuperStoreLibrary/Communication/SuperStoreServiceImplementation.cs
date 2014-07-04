using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SuperStoreLibrary.Model;
using SuperStoreLibrary.Util;

namespace SuperStoreLibrary.Communication
{
    public class SuperStoreServiceImplementation : SuperStoreServiceInterface
    {
        public AuthenticationCredentials RegisterNewUser(RegistrationContainer userDetails)
        {
            userDetails.Validate();
            Customer.CheckUsernameAvailability(userDetails.username);
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            //Create new customer and add it to the db
            string password = PasswordUtils.GeneratePassword(userDetails.username);
            Customer newCustomer = new Customer{name = userDetails.name, username = userDetails.username, password = PasswordUtils.HashPassword(password), saldo = 200};
            modelContainer.Customers.Add(newCustomer);
            modelContainer.SaveChanges();

            return new AuthenticationCredentials { password = password, username = userDetails.username };

        }
        public CustomerContainer RetrieveUserInfo(AuthenticationCredentials credentials)
        {
            credentials.Validate();
            Customer requestedCustomer = Customer.Retrieve(credentials);
            return new CustomerContainer(requestedCustomer);
        }

        public List<ProductResponseContainer> RetrieveAvailableProducts(Pagination pagination)
        {
            pagination.Validate();
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            var products = (from product in modelContainer.Products
                            where product.stock.amount > 0
                            orderby product.name
                            select product).
                            Skip((pagination.pageIndex- 1) * pagination.pageSize).
                            Take(pagination.pageSize);

            List<ProductResponseContainer> result = new List<ProductResponseContainer>();
            foreach (Product product in products){
                result.Add(new ProductResponseContainer(product));
            }
            if (result.Any()) { return result; }
            else throw new FaultException("No products found");
        }

        public List<PurchaseResponseContainer> RetrievePurchaseHistory(AuthenticationCredentials credentials, Pagination pagination)
        {
            credentials.Validate();
            pagination.Validate();
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            var purchases = (from purchase in modelContainer.Purchases
                            where purchase.customer.username == credentials.username
                            orderby purchase.date
                            select purchase).
                            Skip((pagination.pageIndex - 1) * pagination.pageSize).
                            Take(pagination.pageSize);

            List<PurchaseResponseContainer> result = new List<PurchaseResponseContainer>();
            foreach (Purchase purchase in purchases)
            {
                result.Add(new PurchaseResponseContainer(purchase));
            }
            if (result.Any()) { return result; }
            else throw new FaultException("No purchases found");
        }

        public PurchaseResponseContainer PurchaseProduct(AuthenticationCredentials credentials, PurchaseRequestContainer purchase)
        {
            credentials.Validate();
            purchase.Validate();

            Customer customer = Customer.Retrieve(credentials);
            Product product = Product.Retrieve(purchase.product.id);

            Purchase purchaseResult = customer.BuyProduct(product, purchase.amount);

            return new PurchaseResponseContainer(purchaseResult);
        }

        public void AddProduct(ProductResponseContainer product)
        {
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();

            Product newProduct = new Product { name = product.name, price = product.price, stock = new Stock { amount = product.stock } };
            modelContainer.Products.Add(newProduct);
            modelContainer.SaveChanges();
        }
    }
}
