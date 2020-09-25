using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class ProductModel : BaseModel
    {
        public string ProductId { get; set; }
        public string ProductNm { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
    }
}
