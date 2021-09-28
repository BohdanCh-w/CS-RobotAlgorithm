using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class TestAlgorithm {
        [TestMethod]
        public void TestMoveCommand() {
            var algorithm = new ChepeliukBohdanAlgorithm();
            var map = new Map();
            var staticposition = new Position(1, 1);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = staticposition, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200, Position = new Position(2, 3)
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOfType(command, typeof(MoveCommand));
            Assert.AreEqual(((MoveCommand)command).NewPosition, staticposition);
        }

        [TestMethod]
        public void TestCollectFullCommand() {
            var algorithm = new ChepeliukBohdanAlgorithm();
            var map = new Map();
            var pos = new Position(0, 0);

            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = pos, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200, Position = pos
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOfType(command, typeof(CollectEnergyCommand));
        }

        [TestMethod]
        public void TestCollectEmptyCommand() {
            var algorithm = new ChepeliukBohdanAlgorithm();
            var map = new Map();
            var pos = new Position(0, 0);

            map.Stations.Add(new EnergyStation() { Energy = 0, Position = pos, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200, Position = pos
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOfType(command, typeof(CollectEnergyCommand));
        }

        [TestMethod]
        public void TestCreateRobotCommandNoFreeStations() {
            var algorithm = new ChepeliukBohdanAlgorithm();
            var map = new Map();
            var pos = new Position(0, 0);

            map.Stations.Add(new EnergyStation() { Energy = 0, Position = pos, RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000, Position = pos
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsNotInstanceOfType(command, typeof(CreateNewRobotCommand));
        }

        [TestMethod]
        public void TestCreateRobotCommandFreeStationsAvailable() {
            var algorithm = new ChepeliukBohdanAlgorithm();
            var map = new Map();
            var pos = new Position(0, 0);

            map.Stations.Add(new EnergyStation() { Energy = 0, Position = pos, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 0, Position = new Position(2, 8), RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000, Position = pos
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsInstanceOfType(command, typeof(CreateNewRobotCommand));
        }

        [TestMethod]
        public void TestCreateRobotCommandFreeStationsFarAway() {
            var algorithm = new ChepeliukBohdanAlgorithm();
            var map = new Map();
            var pos = new Position(0, 0);

            map.Stations.Add(new EnergyStation() { Energy = 0, Position = pos, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 0, Position = new Position(48, 56), RecoveryRate = 2 });
            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 1000, Position = pos
                }
            };

            var command = algorithm.DoStep(robots, 0, map);

            Assert.IsNotInstanceOfType(command, typeof(CreateNewRobotCommand));
        }
    }
}