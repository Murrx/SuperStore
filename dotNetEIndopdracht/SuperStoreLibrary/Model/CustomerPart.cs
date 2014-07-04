using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperStoreLibrary.Communication;
using System.ServiceModel;
using SuperStoreLibrary.Util;

namespace SuperStoreLibrary.Model
{
    partial class Customer
    {
        public static Customer Retrieve(AuthenticationCredentials credentials){
            //Check if the username/password combination is valid
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();
            string hashedPassword = PasswordUtils.HashPassword(credentials.password);
            Customer requestedCustomer = modelContainer.Customers.SingleOrDefault(user => user.username == credentials.username && user.password == hashedPassword);
            if (requestedCustomer == null)
            {
                throw new FaultException("Invalid username password combination");
            }
            return requestedCustomer;
        }

        public Purchase BuyProduct(Product product, int amount)
        {
            double totalPrice = product.price * amount;
            if (product.IsInStock(amount) && this.HasSaldo(totalPrice)) 
            {
                product.stock.amount -= amount;
                saldo -= totalPrice;
                Purchase purchase = new Purchase { product = product, amount = amount, customer = this, date = DateTime.Now };
                purchases.Add(purchase);

                SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();
                modelContainer.SaveChanges();
                return purchase;
            }
            else if (!product.IsInStock(amount))
            {
                throw new FaultException("Requested amount of this product not in stock. Only " + product.stock.amount + " left");
            }
            else if (!HasSaldo(totalPrice))
            {
                throw new FaultException("You dont have enough saldo. For this transaction you need " + totalPrice + " and you only have " + saldo);
            }
            else
            {
                throw new FaultException("An unexpected error occured. Please contact support");
            }
        }
        private bool HasSaldo(double totalPrice)
        {
            return (saldo - totalPrice >= 0);
        }

        public static void CheckUsernameAvailability(string username)
        {
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();
            Customer existingCustomer = modelContainer.Customers.FirstOrDefault(user => user.username == username);
            if (existingCustomer != null)
            {
                throw new FaultException("Username already in use.");
            }
        }
  
    }
}
