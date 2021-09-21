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

        public static EnergyStation FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots) {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations) {
                if (!ChepeliukBohdanAlgorithm.prefferedPositions.Values.Contains(station.Position) 
                    && IsStationFree(station, movingRobot, robots)) {
                    int d = FindDistance(station.Position, movingRobot.Position);
                    if (d < minDistance) {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }
            return nearest == null ? null : nearest;
        }

        public static int Closer(Position start, Position p1, Position p2) {
            int d1 = FindDistance(start, p1);
            int d2 = FindDistance(start, p2);
            return d1 > d2 ? 1 : d1 == d2 ? 0 : -1;
        }

        public static int StationEnergy(Position pos, Map map) {
            foreach (var station in map.Stations) { 
                if(station.Position == pos) {
                    return station.Energy;
                }
            }
            return -1;
        }

        public static bool IsStationFree(EnergyStation station, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots) {
            return IsCellFree(station.Position, movingRobot, robots);
        }

        public static bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots) {
            foreach (var robot in robots) {
                if (robot.Position == cell)
                    return false;
            }
            return true;
        }

        public static int AttackRevenue(Robot.Common.Robot my, Robot.Common.Robot target) {
            int cost = (int)(Math.Sqrt(FindDistance(my.Position, target.Position))) * 10 + 100;
            int gain = (int)(target.Energy * 0.05);
            return gain - cost;
        }

        public static Position MinimalMove(Position curr, Position target, int energy) {
            int steps = (int)Math.Ceiling(FindDistance(curr, target) / (double)energy);
            Position move = new Position(curr.X + (target.X - curr.X) / steps, curr.Y + (target.Y - curr.Y) / steps);
            return move;
        }

        public static Position OptimalMove(Position curr, Position target, int energy) {
            int dist = FindDistance(curr, target);
            if (dist <= 100) {
                if(energy >= dist)
                    return target;
                return MinimalMove(curr, target, energy);
            }

            int steps = (int)Math.Ceiling(Math.Sqrt(dist) / 10.0);
            Position move = new Position(curr.X + (target.X - curr.X) / steps, curr.Y + (target.Y - curr.Y) / steps);
            if (FindDistance(curr, move) * steps > energy) {
                return MinimalMove(curr, target, energy);
            }
            return move;
        }
    }
}
