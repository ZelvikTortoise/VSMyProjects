using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Test1;    // Not needed though.

namespace UnitTestRemoveAt
{
    // Adding:  Solution Explorer -> Add -> New Project -> Tests -> MSTest
    // Running: Test -> Run -> All Tests
    // Display: Test -> Windows -> Test Explorer

    // classes: public, atribute [TestClass]
    // methods: public, atribute [TestMethod] -> Assert, CollectionAssert classes
    // more possible atributes: [ExpectedException(typeof(...)), ...]

    // Possible to create mockup (more complicated)

    // Connecting to our project:
    // References -> Add reference -> our project tick -> Add
    // //our test project's name// -> References -> Add reference -> our project tick -> Add
    // [assembly:System.Runtime.CompilerServices.InternalsVisibleTo(//our test project's name (in Properties)//)] ... NOT IN NAMESPACE!
    // using //our project's name (in Properties)//;


    [TestClass]
    public class TestingListRemoveAt
    {
        // Test 1:
        [TestMethod]
        public void TestRemovingAt0()
        {
            // Arrange:
            List<int> list = new List<int> { 8, -1, 0, int.MaxValue };
            int index = 0;
            List<int> expected = new List<int> { -1, 0, int.MaxValue };


            // Act:
            list.RemoveAt(index);

            // Assert:
            CollectionAssert.AreEqual(expected, list);
        }

        // Test 2:
        [TestMethod]
        public void TestOutOfRange1()
        {
            // Arrange:
            List<int> list = new List<int> { 8, -1, 0, int.MaxValue };
            int index = list.Count;
            bool excAooRE = false;

            // Act:
            try
            {
                list.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                excAooRE = true;
            }

            // Assert:
            Assert.IsTrue(excAooRE);
        }

        // Test 2 other way:
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOutOfRange2()
        {
            // Arrange:
            List<int> list = new List<int> { 8, -1, 0, int.MaxValue };
            int index = list.Count;

            // Act:
            list.RemoveAt(index);   // Expected exception.

            // Arrange:
            // empty
        }

        // Test 3:
        [TestMethod]
        public void TestRemoveAllElements()
        {
            // Arrange:
            List<int> list = new List<int> { 8, -1, 0, int.MaxValue };
            int index;
            int listCountBefore = list.Count;
            List<int> expected = new List<int> { };

            // Act:
            for (index = 0; index < listCountBefore; index++)
                list.RemoveAt(0);   // Removing always at first index.

            // Assert:
            CollectionAssert.AreEqual(expected, list);
            Assert.AreEqual(listCountBefore, index);
        }

        // Test 4:
        [TestMethod]
        public void TestCheckingCorrectLeftShift()
        {
            // Arrange:
            List<int> list = new List<int> { 8, -1, 0, int.MaxValue };
            int index = 1;
            int? itemOnIndex2AfterRemoving = null;

            // Act:
            list.RemoveAt(index);
            itemOnIndex2AfterRemoving = list[2];

            // Assert:
            Assert.AreEqual(int.MaxValue, itemOnIndex2AfterRemoving);
        }
    }

    [TestClass]
    public class TestingListSort
    {
        [TestMethod]
        public void TestAlreadySorted()
        {
            // Arrange:
            List<int> actual = new List<int> { -1, 0, 8, int.MaxValue };
            List<int> expected = new List<int> { -1, 0, 8, int.MaxValue };
           
            // Act:
            actual.Sort();

            // Assert:
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
