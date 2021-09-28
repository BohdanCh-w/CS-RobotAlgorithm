using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class HelperMethodsTestStations {
        [TestMethod]
        public void TestFreeStation() {
            var pos = new Position(1, 1);
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(2, 3)
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
            

            var map = new Map();
            var station = new EnergyStation() { Energy = 1000, Position = pos, RecoveryRate = 2 };
            map.Stations.Add(station);

            Assert.IsTrue(HelperMethods.IsStationFree(station, currRobot, robots));
        }

        [TestMethod]
        public void TestOccupiedStation() {
            var pos = new Position(1, 1);
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(2, 3)
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


            var map = new Map();
            var station = new EnergyStation() { Energy = 1000, Position = pos, RecoveryRate = 2 };
            map.Stations.Add(station);

            Assert.IsFalse(HelperMethods.IsStationFree(station, currRobot, robots));
        }

        [TestMethod]
        public void TestNearestOneFreeStation() {
            var pos = new Position(1, 1);
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(2, 3)
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(3, 7)
                },
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(5, 1)
                },
                currRobot
            };

            var map = new Map();
            var station = new EnergyStation() { Energy = 1000, Position = pos, RecoveryRate = 2 };
            map.Stations.Add(station);
            Assert.AreEqual(station, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
        }

        [TestMethod]
        public void TestNearestOneOccupiedStation() {
            var pos = new Position(1, 1);
            var currRobot = new Robot.Common.Robot()
            {
                Energy = 200,
                Position = new Position(2, 3)
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

            var map = new Map();
            var station = new EnergyStation() { Energy = 1000, Position = pos, RecoveryRate = 2 };
            map.Stations.Add(station);
            Assert.AreEqual(null, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
        }

        [TestMethod]
        public void TestNearestSeveralBothFreeStation() {
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(2, 3)
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(3, 7)
                },
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = new Position(5, 1)
                },
                currRobot
            };

            var map = new Map();
            var nearest_station = new EnergyStation() { Energy = 1000, Position = new Position(3, 3), RecoveryRate = 2 };
            map.Stations.Add(nearest_station);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(10, 2), RecoveryRate = 2 });
            Assert.AreEqual(nearest_station, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
        }

        [TestMethod]
        public void TestNearestSeveralOneFreeStation() {
            var pos = new Position(3, 3);
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(2, 3)
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

            var map = new Map();
            var free_station = new EnergyStation() { Energy = 1000, Position = new Position(10, 2), RecoveryRate = 2 };
            map.Stations.Add(free_station);
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = pos, RecoveryRate = 2 });
            Assert.AreEqual(free_station, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
        }

        [TestMethod]
        public void TestNearestSeveralOccupiedStation() {
            var pos1 = new Position(1, 1);
            var pos2 = new Position(1, 1);
            var currRobot = new Robot.Common.Robot() {
                Energy = 200,
                Position = new Position(2, 3)
            };

            var robots = new List<Robot.Common.Robot>() {
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = pos1
                },
                new Robot.Common.Robot() {
                    Energy = 200,
                    Position = pos2
                },
                currRobot
            };

            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = pos1, RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = pos2, RecoveryRate = 2 });
            Assert.AreEqual(null, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
        }

        [TestMethod]
        public void TestStationEnergyPositive() {
            int energy = 1337;
            var pos = new Position(22, 8);
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(0, 0), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = energy, Position = pos, RecoveryRate = 2 });
            Assert.AreEqual(energy, HelperMethods.StationEnergy(pos, map));
        }

        [TestMethod]
        public void TestStationEnergyMiss() {
            int energy = 1337;
            var pos = new Position(22, 8);
            var map = new Map();
            map.Stations.Add(new EnergyStation() { Energy = 1000, Position = new Position(0, 0), RecoveryRate = 2 });
            map.Stations.Add(new EnergyStation() { Energy = energy, Position = new Position(pos.X, pos.Y + 1), RecoveryRate = 2 });
            Assert.AreEqual(-1, HelperMethods.StationEnergy(pos, map));
        }

        [TestMethod]
        public void TestStationEnergyNoStations() {
            var map = new Map();
            Assert.AreEqual(-1, HelperMethods.StationEnergy(new Position(22, 8), map));
        }
    }
}