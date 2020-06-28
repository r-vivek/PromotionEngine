using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    public class PromotionEngine
    {
        private readonly IRuleReader _ruleReader;

        public PromotionEngine(IRuleReader ruleReader)
        {
            _ruleReader = ruleReader;
        }
        public int Run(List<CartItem> cartItems, List<ItemPrice> itemPriceList)
        {
            int runningTotal = 0;
            var rules = _ruleReader.ReadRules();
            return runningTotal;
        }
    }
}
