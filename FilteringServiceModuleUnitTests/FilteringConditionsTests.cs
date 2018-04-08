using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SzafaEntities;
using System.Windows;
using System.Collections.Generic;
using FilteringEntities;

namespace FilteringServiceModuleUnitTests
{
    [TestClass]
    public class FilteringConditionsTests
    {
        List<PieceOfClothing> mockInputList = new List<PieceOfClothing>()
        {
            new PieceOfClothing()
            {
                Name = "ABC",
                InUse = false
            },

            new PieceOfClothing()
            {
                Name = "ABCD",
                InUse = true
            }
        };

        List<PieceOfClothing> mockNullNameList = new List<PieceOfClothing>()
        {
            new PieceOfClothing()
            {
                Name = null
            }
        };

        [TestMethod]
        public void ApplyFilters_NullInput_ExpectFalse()
        { 
            Predicate<PieceOfClothing> test = new Predicate<PieceOfClothing>(x => x.Name == "ABC");
            FilteringConditions filtering = new FilteringConditions() { Name = "new", Conditions = test };

            var result = filtering.ApplyFilters(null);

            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void ApplyFilters_Name_ABC_ExpectTrue()
        {
            Predicate<PieceOfClothing> test = new Predicate<PieceOfClothing>(x => x.Name == "ABC");
            FilteringConditions filtering = new FilteringConditions() { Name = "new", Conditions = test };

            var result = filtering.ApplyFilters(mockInputList);

            Assert.AreEqual(mockInputList[0].Name, result[0].Name);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void ApplyFilters_Name_ABCD_ExpectFalse()
        {
            Predicate<PieceOfClothing> test = new Predicate<PieceOfClothing>(x => x.Name == "ABCDE");
            FilteringConditions filtering = new FilteringConditions() { Name = "new", Conditions = test };

            var result = filtering.ApplyFilters(mockInputList);

            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void ApplyFilters_ParameterNotExistent_ExpectFalse()
        {
            Predicate<PieceOfClothing> test = new Predicate<PieceOfClothing>(x => x.Name == "ABC");
            FilteringConditions filtering = new FilteringConditions() { Name = "new", Conditions = test };

            var result = filtering.ApplyFilters(mockNullNameList);

            Assert.AreEqual(result.Count, 0);
        }
    }
}
