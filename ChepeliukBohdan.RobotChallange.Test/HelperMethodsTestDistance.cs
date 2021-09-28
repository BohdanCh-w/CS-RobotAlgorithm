using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class HelperMethodsTestDistance {
        [TestMethod]
        public void TestZeroDistance() {
            var p1 = new Position(2, 5);
            var p2 = new Position(2, 5);

            Assert.AreEqual(0, HelperMethods.FindDistance(p1, p2));
        }

        [TestMethod]
        public void TestAvrgDistance() {
            var p1 = new Position(2, 5);
            var p2 = new Position(3, 12);

            Assert.AreEqual(50, HelperMethods.FindDistance(p1, p2));
        }

        [TestMethod]
        public void TestCloserFirst() {
            var curr = new Position(2, 2);
            var p1 = new Position(2, 5);
            var p2 = new Position(3, 12);

            Assert.AreEqual(1, HelperMethods.Closer(curr, p1, p2));
        }

        [TestMethod]
        public void TestCloserSecond() {
            var curr = new Position(2, 11);
            var p1 = new Position(2, 5);
            var p2 = new Position(3, 12);

            Assert.AreEqual(-1, HelperMethods.Closer(curr, p1, p2));
        }        

        [TestMethod]
        public void TestCloserEqueal() {
            var curr = new Position(6, 10);
            var p1 = new Position(9, 8);
            var p2 = new Position(3, 12);

            Assert.AreEqual(0, HelperMethods.Closer(curr, p1, p2));
        }
    }
}
