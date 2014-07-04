using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace SuperStoreLibrary.Model
{
    partial class Product
    {
        public static Product Retrieve(int productId)
        {
            SuperStoreModelContainer modelContainer = ModelContainerProvider.GetInstance();
            Product retrievedProduct = modelContainer.Products.FirstOrDefault(product => product.id == productId);
            if (retrievedProduct != null) { return retrievedProduct; }
            else
            {
                throw new FaultException("Product with id " + productId + " not found");
            }
        }

        public bool IsInStock(int amount)
        {
            return (stock.amount - amount >= 0);
        }
    }
}