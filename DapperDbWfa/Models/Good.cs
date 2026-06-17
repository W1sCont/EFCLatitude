using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Good
    {
        public int GoodID { get; set; }
        public string GoodName { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
    }
}
