using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using zLib;

namespace TestzLib
{
    [TestClass]
    public class TestAddress
    {
        [TestMethod]
        public void simple1()
        {
            Address a = new AddressParser("18332 28th NE Redmond WA 98052").parseAddress();
            Assert.AreEqual(a.City, "Redmond");
            Assert.AreEqual(a.Zip, "98052");
            Assert.AreEqual(a.AddressLine, "18332 28th NE");
            Assert.AreEqual(a.State, "WA");
        }

        [TestMethod]
        public void simple2()
        {
            Address a = new AddressParser("18332 28th NE Redmond 98052 WA").parseAddress();
            Assert.AreEqual("Redmond", a.City);
            Assert.AreEqual("98052", a.Zip);
            Assert.AreEqual("18332 28th NE", a.AddressLine);
            Assert.AreEqual("WA", a.State);

        }

        [TestMethod]
        public void medium2()
        {
            Address a = new AddressParser("18332 28th NE, Redmond 98052 WA").parseAddress();
            Assert.AreEqual("Redmond", a.City);
            Assert.AreEqual("98052", a.Zip);
            Assert.AreEqual("18332 28th NE", a.AddressLine);
            Assert.AreEqual("WA", a.State);

        }

        [TestMethod]
        public void simple3()
        {
            Address a = new AddressParser("18332 28th NE Redmond 98052 ").parseAddress();
            Assert.AreEqual("Redmond", a.City);
            Assert.AreEqual("18332 28th NE", a.AddressLine);
            Assert.AreEqual(null, a.State);
        }

        [TestMethod]
        public void simple4()
        {
            Address a = new AddressParser("18332 28th NE Redmond  WA ").parseAddress();
            Assert.AreEqual("Redmond", a.City);
            Assert.AreEqual(null, a.Zip);
            Assert.AreEqual("18332 28th NE", a.AddressLine);
            Assert.AreEqual("WA", a.State);
        }


    }
}
