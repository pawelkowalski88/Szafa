using FilteringEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;

namespace FilteringServiceModuleUnitTests
{
    [TestClass]
    public class FilteringSortingConditionsTests
    {
        List<PieceOfClothing> mockInputList = new List<PieceOfClothing>()
        {
            new PieceOfClothing()
            {
                Name = "Zas",
                InUse = true
            },

            new PieceOfClothing()
            {
                Name = "bbbb",
                InUse = true
            },

            new PieceOfClothing()
            {
                Name = "Afg",
                InUse = true
            },

            new PieceOfClothing()
            {
                Name = "GFK",
                InUse = false
            }
        };

        [TestMethod]
        public void ApplyFilters_mockInputList_Expect3elements()
        {
            Predicate<PieceOfClothing> test = new Predicate<PieceOfClothing>(x => x.InUse == true);
            FilteringSortingConditions sorting = new FilteringSortingConditions()
            {
                Name = "new",
                Conditions = test,
                Sorting = new SortingConditions()
                {
                    Name = "test",
                    Condition = new Func<PieceOfClothing, object>(x => x.Name)
                },
                Order = new SortingOrder()
                {
                    Name = "asc",
                    Direction = true
                }
            };

            var result = sorting.Sort(mockInputList);

            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("Afg", result[0].Name);
            Assert.AreEqual("bbbb", result[1].Name);
            Assert.AreEqual("Zas", result[2].Name);
        }
    }
}
