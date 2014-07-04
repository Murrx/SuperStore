using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SuperStoreLibrary.Model;

namespace SuperStoreLibrary.Communication
{
    [DataContract]
    public class CustomerContainer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public double saldo { get; set; }
        [DataMember]
        public AuthenticationCredentials credentials { get; set; }

        public CustomerContainer() { }
        public CustomerContainer(Customer customer)
        {
            name = customer.name;
            saldo = customer.saldo;
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
    public class PurchaseRequestContainer
    {
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public ProductRequestContainer product { get; set; }

        public PurchaseRequestContainer() { }
        public PurchaseRequestContainer(Purchase purchase)
        {
            amount = purchase.amount;
            product = new ProductRequestContainer(purchase.product);
        }
        public void Validate()
        {
            if (product == null) { throw new FaultException("product cannot be null"); }
            else { product.Validate(); }
            if (amount < 1) { throw new FaultException("Amount must be larger than 0"); }
        }
    }
    [DataContract]
    public class PurchaseResponseContainer
    {
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        new public ProductResponseContainer product { get; set; }

        public PurchaseResponseContainer() { }
        public PurchaseResponseContainer(Purchase purchase)
        {
            amount = purchase.amount;
            product = new ProductResponseContainer(purchase.product);
            date = purchase.date;
        }
    }

    [DataContract]
    public class ProductResponseContainer : ProductRequestContainer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public int stock { get; set; }
        public ProductResponseContainer() { }
        public ProductResponseContainer(Product product) : base (product)
        {
            name = product.name;
            price = product.price;
            stock = product.stock.amount;
        }
    }
    [DataContract]
    public class ProductRequestContainer
    {
        [DataMember]
        public int id { get; set; }

        public ProductRequestContainer() { }
        public ProductRequestContainer(Product product) 
        {
            id = product.id;
        }
        public void Validate()
        {
            if (id < 1) { throw new FaultException("id must be bigger than 0"); }
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
