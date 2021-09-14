using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange {
    public class HelperMethods {
        public static int FindDistance(Position a, Position b) {
            return (int)((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots) {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations) {
                if (!ChepeliukBohdanAlgorithm.prefferedStations.Contains(station.Position) && IsStationFree(station, movingRobot, robots)) {
                    int d = FindDistance(station.Position, movingRobot.Position);
                    if (d < minDistance) {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }
            return nearest == null ? null : nearest.Position;
        }

        public static bool IsStationFree(EnergyStation station, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots) {
            return IsCellFree(station.Position, movingRobot, robots);
        }

        public static bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots) {
            foreach (var robot in robots) {
                if (robot != movingRobot) {
                    if (robot.Position == cell)
                        return false;
                }
            }
            return true;
        }
    }
}
