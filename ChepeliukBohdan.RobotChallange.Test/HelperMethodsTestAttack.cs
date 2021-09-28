using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class HelperMethodsTestAttack {
        [TestMethod]
        public void TestAttackRevenuePositive() {
            var curr = new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(4, 2)
                };
            var target = new Robot.Common.Robot() {
                    Energy = 5000,
                    Position = new Position(5, 7)
                };
            Assert.IsTrue(HelperMethods.AttackRevenue(curr, target) > 0);
        }
        
        [TestMethod]
        public void TestAttackRevenueNegative() {
            var curr = new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(4, 2)
                };
            var target = new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(5, 7)
                };
            Assert.IsTrue(HelperMethods.AttackRevenue(curr, target) < 0);
        }
        
        [TestMethod]
        public void TestAttackRevenuePositiveFar() {
            var curr = new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(4, 2)
                };
            var target = new Robot.Common.Robot() {
                    Energy = 5000,
                    Position = new Position(50, 71)
                };
            Assert.IsTrue(HelperMethods.AttackRevenue(curr, target) < 0);
        }
    }
}