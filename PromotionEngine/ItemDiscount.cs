using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class ItemDiscount
    {
        public int ItemQuantityInCart { get; set; }

        public int DiscountQuantity { get; set; }

        public int PerItemPrice { get; set; }

        public int RelativeQuantityRule { get; set; }

        public int GrandTotal => (ItemQuantityInCart - (DiscountQuantity * RelativeQuantityRule)) * PerItemPrice;
    }
}
