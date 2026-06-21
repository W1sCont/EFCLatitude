using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class PromotionIdViewModel
    {
        public int PromotionID { get; set; }
        public int GoodID { get; set; }
        public string GoodName { get; set; } 
        public decimal Price { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; } 
        public int? DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
