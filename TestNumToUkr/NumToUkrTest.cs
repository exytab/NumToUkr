using Exytab.Ukrainian;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestNumToUkr
{
    
    
    /// <summary>
    ///This is a test class for NumToUkrTest and is intended
    ///to contain all NumToUkrTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NumToUkrTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
       
        /// <summary>
        ///A test for FromString
        ///</summary>
        [TestMethod()]
        public void FromStringTest1()
        {
            string val = "  123.33"; 
            string expected = "сто двадцять три гривні 33 копійки"; 
            string actual;
            actual = NumToUkr.FromString(val);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FromString
        ///</summary>
        [TestMethod()]
        public void FromStringTest2()
        {
            string val = "  987654321,3  ";
            string expected = "дев'ятсот вісімдесят сім мільйонів шістсот п'ятдесят чотири тисячі триста двадцять одна гривня 30 копійок";
            string actual;
            actual = NumToUkr.FromString(val);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FromString
        ///</summary>
        [TestMethod()]
        public void FromStringTest3()
        {
            string val = "311512516";
            string expected = "триста одинадцять мільйонів п'ятсот дванадцять тисяч п'ятсот шістнадцять гривень 00 копійок";
            string actual;
            actual = NumToUkr.FromString(val);
            Assert.AreEqual(expected, actual);
        }

    }
}
