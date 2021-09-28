using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class AlgorithmTestNextTarget {
        [TestMethod]
        public void TestChooseNearestStation() {         
            var algorithm = new ChepeliukBohdanAlgorithm();

            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(0, 0)
            };

            var robots = new List<Robot.Common.Robot>() {
                currRobot
            };

            var pos1 = new Position(0, 5);
            var pos2 = new Position(0, 10);
            int energy = 1000;
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = energy, Position = pos1, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = energy, Position = pos2, RecoveryRate = 2 });

            Assert.AreEqual(pos1, algorithm.NextTarget(currRobot, map, robots));
        }
        
        [TestMethod]
        public void TestChooseMoreEnergyStation() {         
            var algorithm = new ChepeliukBohdanAlgorithm();

            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(0, 0)
            };

            var robots = new List<Robot.Common.Robot>() {
                currRobot
            };

            var pos1 = new Position(0, 5);
            var pos2 = new Position(pos1.Y+2, pos1.X);
            int energy = 1000;
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = energy, Position = pos1, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = energy * 2, Position = pos2, RecoveryRate = 2 });

            Assert.AreEqual(pos2, algorithm.NextTarget(currRobot, map, robots));
        }
        
        [TestMethod]
        public void TestChooseEnemyRobot() {         
            var algorithm = new ChepeliukBohdanAlgorithm();

            var pos1 = new Position(0, 5);
            var pos2 = new Position(pos1.Y, pos1.X);

            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(0, 0)
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 5000,
                    Position = pos2,
                    OwnerName = "Other"
                },
                currRobot
            };
            int energy = 1000;
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = energy, Position = pos1, RecoveryRate = 2 });

            Assert.AreEqual(pos2, algorithm.NextTarget(currRobot, map, robots));
        }
        
        [TestMethod]
        public void TestChooseEnemyRobotLowEnergy() {         
            var algorithm = new ChepeliukBohdanAlgorithm();

            var pos1 = new Position(0, 5);
            var pos2 = new Position(pos1.Y, pos1.X);

            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(0, 0)
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = pos2,
                    OwnerName = "Other"
                },
                currRobot
            };
            int energy = 1000;
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = energy, Position = pos1, RecoveryRate = 2 });

            Assert.AreNotEqual(pos2, algorithm.NextTarget(currRobot, map, robots));
        }
    }
}