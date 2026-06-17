using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public int GoodID { get; set; }
        public int CountryID { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
