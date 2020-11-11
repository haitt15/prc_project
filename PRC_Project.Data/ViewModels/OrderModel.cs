using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class OrderModel : BaseModel
    {
        public string OrderId { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string  Phone { get; set; }
        public double Total { get; set; }
        public IEnumerable<ProductModel> ListProductModels { get; set; }
        public  List<OrderDetailModel> OrderDetail { get; set; }
    }
}
