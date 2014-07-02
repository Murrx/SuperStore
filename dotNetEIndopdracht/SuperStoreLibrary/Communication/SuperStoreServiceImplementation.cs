using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SuperStoreLibrary.Communication
{
    public class SuperStoreServiceImplementation : SuperStoreServiceInterface
    {

        public AuthenticationCredentials RegisterNewUser(CustomerContainer NewUser)
        {
            throw new FaultException("not yet implemented");
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
