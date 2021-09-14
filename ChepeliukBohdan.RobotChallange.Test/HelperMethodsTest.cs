using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange.Test {
    [TestClass]
    public class HelperMethodsTest {
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

            Assert.IsTrue(HelperMethods.IsCellFree(pos, currRobot, robots));
        }

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
            Assert.AreEqual(station.Position, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
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
            Assert.AreEqual(nearest_station.Position, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
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
            Assert.AreEqual(free_station.Position, HelperMethods.FindNearestFreeStation(currRobot, map, robots));
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
    }
}
