using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class HelperMethodsTestMovement {
        [TestMethod]
        public void TestOptimalMoveCloseFullEnergy() {
            var currPos = new Position(0, 0);
            var target = new Position(2, 2);
            var robot = new Robot.Common.Robot() {
                Energy = 400,
                Position = currPos
            };
            Assert.AreEqual(target, HelperMethods.OptimalMove(currPos, target, robot.Energy));
        }
        
        [TestMethod]
        public void TestOptimalMoveCloseLowEnergy() {
            var currPos = new Position(0, 0);
            var target = new Position(2, 2);
            var robot = new Robot.Common.Robot() {
                Energy = 4,
                Position = currPos
            };
            Assert.AreEqual(new Position(1, 1), HelperMethods.OptimalMove(currPos, target, robot.Energy));
        }
        
        [TestMethod]
        public void TestOptimalMoveFarFullEnergy() {
            var currPos = new Position(0, 0);
            var target = new Position(50, 0);
            var robot = new Robot.Common.Robot() {
                Energy = 10000,
                Position = currPos
            };
            Assert.AreEqual(new Position(10, 0), HelperMethods.OptimalMove(currPos, target, robot.Energy));
        }
        
        [TestMethod]
        public void TestOptimalMoveFarLowEnergy() {
            var currPos = new Position(0, 0);
            var target = new Position(40, 0);
            var robot = new Robot.Common.Robot() {
                Energy = 200,
                Position = currPos
            };
            Assert.AreEqual(new Position(5, 0), HelperMethods.OptimalMove(currPos, target, robot.Energy));
        }
    }
}