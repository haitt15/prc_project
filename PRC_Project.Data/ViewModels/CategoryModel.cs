using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class CategoryModel : BaseModel
    {
        public int CategoryId { get; set; }
        public string CategoryNm { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
