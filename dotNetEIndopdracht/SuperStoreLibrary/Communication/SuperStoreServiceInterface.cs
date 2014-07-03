using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SuperStoreLibrary.Model;

namespace SuperStoreLibrary.Communication
{
    [ServiceContract]
    public interface SuperStoreServiceInterface
    {
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        AuthenticationCredentials RegisterNewUser(RegistrationContainer userDetails);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        CustomerContainer RetrieveUserInfo(AuthenticationCredentials credentials);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<ProductContainer> RetrieveAvailableProducts(Pagination pagination);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<PurchaseContainer> RetrievePurchaseHistory(AuthenticationCredentials credentials, Pagination pagination);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        PurchaseContainer PurchaseProduct(AuthenticationCredentials credentials, PurchaseContainer productToBuy);

        [OperationContract]
        void AddProduct(ProductContainer product, int amount);
    }

    [DataContract]
    public class CustomerContainer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public AuthenticationCredentials credentials { get; set; }

        public CustomerContainer() { }
        public CustomerContainer(Customer customer)
        {
            name = customer.name;
            credentials = new AuthenticationCredentials { password = customer.password, username = customer.username };
        }
    }
    [DataContract]
    public class AuthenticationCredentials
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }

        public Customer ValidateAndRetrieve()
        {
            if (username == null || username == "") { throw new FaultException("Username cannot be empty"); }
            if (password == null || password == "") { throw new FaultException("Password cannot be empty"); }
            //Check if the username/password combination is valid
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();
            string hashedPassword = SuperStoreServiceImplementation.HashPassword(password);
            Customer requestedCustomer = modelContainer.Customers.SingleOrDefault(user => user.username == this.username && user.password == hashedPassword);
            if (requestedCustomer == null)
            {
                throw new FaultException("Invalid username password combination");
            }
            return requestedCustomer;
        }
    }

    [DataContract]
    public class PurchaseContainer
    {
        [DataMember]
        public ProductContainer product { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public DateTime date { get; set; }
    }
    [DataContract]
    public class ProductContainer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public double price { get; set; }

        public ProductContainer() { }
        public ProductContainer(Product product)
        {
            name = product.name;
            price = product.price;
        }
    }
    [DataContract]
    public class Pagination
    {
        [DataMember]
        public int pageIndex { get; set; }
        [DataMember]
        public int pageSize { get; set; }

        public void Validate()
        {
            pageIndex = (pageIndex > 0) ? pageIndex : 1;
            pageSize = (pageSize > 0) ? pageSize : 20;
        }
    }
    [DataContract]
    public class RegistrationContainer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string username { get; set; }

        public void Validate()
        {
            if (name == null || name == "") { throw new FaultException("Name cannot be empty"); }
            if (username == null || username == "") { throw new FaultException("Username cannot be empty"); }
            //check availability of username
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();
            Customer existingCustomer = modelContainer.Customers.FirstOrDefault(user => user.username == username);
            if (existingCustomer != null)
            {
                throw new FaultException("Username already in use.");
            }
        }
    }
}
