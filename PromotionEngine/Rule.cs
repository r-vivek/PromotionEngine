using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public class Rule
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Item { get; set; }

        public List<Rule> Dependency { get; set; }
    }
}
