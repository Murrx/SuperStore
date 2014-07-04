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
}
