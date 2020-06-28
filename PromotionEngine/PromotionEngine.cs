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

            // get all rules
            var rules = _ruleReader.ReadRules();

            // merge itemprice into cartItems
            foreach (var cartItem in cartItems)
            {
                foreach (var itemPrice in itemPriceList)
                {
                    if (cartItem.Item == itemPrice.Item)
                    {
                        cartItem.ItemPrice = itemPrice.Price;
                    }
                }
            }


            return runningTotal;
        }
    }
}
