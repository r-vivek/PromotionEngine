using System;
using System.Collections.Generic;
using System.Linq;

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

            foreach (var rule in rules)
            {
                // Simple Discounts
                if (rule.Dependency == null)
                {
                    var simpleItem = cartItems.First((x => x.Item == rule.Item));
                    var simpleItemTotal = (simpleItem.Quantity / rule.Quantity) * rule.Price +
                                          (simpleItem.Quantity % rule.Quantity) * simpleItem.ItemPrice;
                    runningTotal += simpleItemTotal;
                }
                else
                {
                    // Dependent Discounts
                    var discountedQuantityList = new List<int>();
                    var itemDiscounts = new List<ItemDiscount>();
                    foreach (var dRule in rule.Dependency)
                    {
                        var itemD = cartItems.Where(x => x.Item == dRule.Item).FirstOrDefault();
                        if (itemD == null)
                        {
                            discountedQuantityList.Add(0);
                            break;
                        }

                        var discountedQuantity = itemD.Quantity / dRule.Quantity;
                        discountedQuantityList.Add(discountedQuantity);
                        itemDiscounts.Add(new ItemDiscount
                        {
                            RelativeQuantityRule = dRule.Quantity,
                            PerItemPrice = itemD.ItemPrice,
                            DiscountQuantity = 0,
                            ItemQuantityInCart = itemD.Quantity

                        });
                    }

                    var commonMultiplier = discountedQuantityList.Min();

                    var discountedTotal = commonMultiplier * rule.Price;
                    runningTotal += discountedTotal;

                    foreach (var itemDiscount in itemDiscounts)
                    {
                        itemDiscount.DiscountQuantity = commonMultiplier;
                        runningTotal += itemDiscount.GrandTotal;
                    }
                }
            }

            return runningTotal;
        }
    }
}
