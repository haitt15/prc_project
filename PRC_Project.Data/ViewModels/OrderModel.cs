using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class OrderModel : BaseModel
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public  List<OrderDetailModel> OrderDetail { get; set; }
    }
}
