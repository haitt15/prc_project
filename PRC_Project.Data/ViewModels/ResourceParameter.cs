using System;
using System.Collections.Generic;
using System.Text;

namespace PRC_Project.Data.ViewModels
{
    public class ResourceParameter
    {
        private const int MaxSize = 5;
        public int PageIndex { get; set; } = 1;
        private int _size = MaxSize;
        public int PageSize
        {
            get { return _size; }
            set { _size = (value > MaxSize) ? MaxSize : value; }
        }
        public int SortBy { get; set; }
    }
}
