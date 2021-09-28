using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class HelperMethodsTestPositions {
        [TestMethod]
        public void TestFreeCell() {
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(12, 2)
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(2, 3)
                },
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(5, 1)
                },
                currRobot
            };

            Assert.IsTrue(HelperMethods.IsCellFree(new Position(1, 1), currRobot, robots));
        }

        [TestMethod]
        public void TestOccupiedCell() {
            var pos = new Position(2, 3);
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(12, 2)
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = pos
                },
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(5, 1)
                },
                currRobot
            };

            Assert.IsFalse(HelperMethods.IsCellFree(pos, currRobot, robots));
        }

        [TestMethod]
        public void TestOccupiedByCurrCell() {
            var pos = new Position(2, 3);
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = pos
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(5, 8)
                },
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(5, 1)
                },
                currRobot
            };

            Assert.IsFalse(HelperMethods.IsCellFree(pos, currRobot, robots));
        }
    }
}