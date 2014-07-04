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
        List<ProductResponseContainer> RetrieveAvailableProducts(Pagination pagination);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        List<PurchaseResponseContainer> RetrievePurchaseHistory(AuthenticationCredentials credentials, Pagination pagination);
        
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        PurchaseResponseContainer PurchaseProduct(AuthenticationCredentials credentials, PurchaseRequestContainer productToBuy);

        [OperationContract]
        void AddProduct(ProductResponseContainer product);
    }
}
