using System;
using System.Collections.Generic;
using System.Text;

namespace urfurniture.Models
{
    public class InvoiceModel
    {
        public string LogoImage { get; set; }
        public string OrderDate {get;set;}
        public string InvoiceDate {get;set;}
        public string  InvoiceNo{ get; set; }
        public float  AmountDue{ get; set; }
        public float Subtotal { get; set; }
        public int ShippingCharge { get; set; }

        public Userdetails Userdetail { get; set; }
        public List<ProductOrderDetails> Products { get; set; }

        public class ProductOrderDetails { 
                               public string Name { get; set; }
                               public int Quantity { get; set; }
                               public float Price { get; set; }
                               public float Discount { get; set; }
                               public int Tax { get; set; }
                               public float LineTotal { get; set; }

                             }
        public class Userdetails
        {
            public string City { get; set; }
            public string State { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Zipcode { get; set; }
            public string Landmark { get; set; }

            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNo { get; set; }
        }

        public InvoiceModel(List<ProductOrderDetails> productOrderDetails,Userdetails user)
        {
            Products = productOrderDetails;
            Userdetail = user;
            float totalAmount = 0;
            foreach (var total in productOrderDetails)
            {
                totalAmount += total.Price*total.Quantity;
            }
            Subtotal = totalAmount;

        }
    }
}
