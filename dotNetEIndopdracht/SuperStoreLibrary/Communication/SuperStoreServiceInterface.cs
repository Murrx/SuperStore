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
        AuthenticationCredentials RegisterNewUser(string Name, string Username);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        CustomerContainer RetrieveUserInfo(AuthenticationCredentials Credentials);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<ProductContainer> RetrieveAvailableProducts(int PageIndex, int PageSize);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<PurchaseContainer> RetrievePurchaseHistory(AuthenticationCredentials Credentials, int PageIndex, int PageSize);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        PurchaseContainer PurchaseProduct(AuthenticationCredentials Credentials, PurchaseContainer ProductToBuy);
    }

    [DataContract]
    public class CustomerContainer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public AuthenticationCredentials credentials { get; set; }

        public CustomerContainer() { }
        public CustomerContainer(Customer Cust)
        {
            name = Cust.Name;
            credentials = new AuthenticationCredentials { password = Cust.Password, username = Cust.Username };
        }
    }
    [DataContract]
    public class AuthenticationCredentials
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
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
    }
}
