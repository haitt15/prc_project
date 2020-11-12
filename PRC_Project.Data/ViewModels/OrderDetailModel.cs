using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public  OrderModel Order { get; set; }
        public  ProductModel Product { get; set; }
    }
}
