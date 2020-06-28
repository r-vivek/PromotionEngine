using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class CartItem
    {
        public string Item { get; set; }
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }

        public bool IsProcessed { get; set; }
    }
}
