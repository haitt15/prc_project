using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class BaseModel
    {
        public bool DelFlg { get; set; }
        public string InsBy { get; set; }
        public DateTime InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime UpdDatetime { get; set; }
    }
}
