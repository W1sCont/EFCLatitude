using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class PromotionalViewModel
    {
        public string GoodName { get; set; }
        public int DiscountPercent { get; set; }
        public string CountryName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
