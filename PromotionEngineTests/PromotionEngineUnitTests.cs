using System.Collections.Generic;
using System.IO;
using Autofac.Extras.Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using PromotionEngine;

namespace PromotionEngineTests
{
    public class PromotionEngineUnitTests
    {
        #region Init

        public static List<ItemPrice> ItemPriceList = new List<ItemPrice> {
            new ItemPrice
            {
                Item = "A",
                Price = 50
            },
            new ItemPrice
            {
                Item = "B",
                Price = 30
            },
            new ItemPrice
            {
                Item = "C",
                Price = 20
            },
            new ItemPrice
            {
                Item = "D",
                Price = 15
            },
            new ItemPrice
            {
                Item = "E",
                Price = 100,
            }
            };

        // 100
        public static List<CartItem> CartItemsA = new List<CartItem>
        {
            new CartItem
            {
                Item = "A",
                Quantity = 1
            },
            new CartItem
            {
                Item = "B",
                Quantity = 1
            },
            new CartItem
            {
                Item = "C",
                Quantity = 1
            }
        };

        // 370
        public static List<CartItem> CartItemsB = new List<CartItem>
        {
            new CartItem
            {
                Item = "A",
                Quantity = 5
            },
            new CartItem
            {
                Item = "B",
                Quantity = 5
            },
            new CartItem
            {
                Item = "C",
                Quantity = 1
            }
        };

        // 280
        public static List<CartItem> CartItemsC = new List<CartItem>
            {
                new CartItem
                {
                    Item = "A",
                    Quantity = 3
                },
                new CartItem
                {
                    Item = "B",
                    Quantity = 5
                },
                new CartItem
                {
                    Item = "C",
                    Quantity = 1
                },
                new CartItem
                {
                    Item = "D",
                    Quantity = 1
                }

            };

        public static List<CartItem> CartItemsD = new List<CartItem>
        {
            new CartItem
            {
                Item = "A",
                Quantity = 3
            },
            new CartItem
            {
                Item = "B",
                Quantity = 1
            },
            new CartItem
            {
                Item = "E",
                Quantity = 1
            }
        };
        private static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData(CartItemsA, 100);
                yield return new TestCaseData(CartItemsB, 370);
                yield return new TestCaseData(CartItemsC, 280);
                yield return new TestCaseData(CartItemsD, 260);
            }
        }

        public static List<Rule> ReadDataFromJson;

        #endregion
        [SetUp]
        public void Setup()
        {
            ReadDataFromJson =
                JsonConvert.DeserializeObject<List<Rule>>(
                    File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"Rules.json")));
        }

        [Test, TestCaseSource(nameof(TestData))]
        [Category("PromotionEngine")]
        public void Test1(List<CartItem> cartItems, int expectedResult)
        {
            using(var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRuleReader>().Setup(x => x.ReadRules()).Returns(ReadDataFromJson);
                var sut = mock.Create<PromotionEngine.PromotionEngine>();
                var total = sut.Run(cartItems, ItemPriceList);
                Assert.AreEqual(expectedResult, total);
            }
        }
    }
}