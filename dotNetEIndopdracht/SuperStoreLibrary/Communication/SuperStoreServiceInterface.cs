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

        public void Validate()
        {
            if (username == null || username == "") { throw new FaultException("Username cannot be empty"); }
            if (password == null || password == "") { throw new FaultException("Password cannot be empty"); }
        }
    }

    [DataContract]
    public class PurchaseContainer
    {
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public int productId { get; set; }

        public PurchaseContainer() { }
        public PurchaseContainer(Purchase purchase) {
            amount = purchase.amount;
            date = purchase.date;
            productId = purchase.product.id;
        }
        public void Validate()
        {
            if (productId < 1) { throw new FaultException("ProductId must be larger than 0"); }
            if (amount < 1) { throw new FaultException("Amount must be larger than 0"); }
            if (date < DateTime.Now.AddHours(-1)) { throw new FaultException("Date cannot be in the past"); }
        }
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
        }
    }
}
