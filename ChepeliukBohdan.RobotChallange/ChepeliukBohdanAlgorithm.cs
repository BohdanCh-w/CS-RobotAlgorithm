using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot.Common;
using Robot.Tournament;

namespace ChepeliukBohdan.RobotChallange {

    public class ChepeliukBohdanAlgorithm : IRobotAlgorithm {
        public static HashSet<Position> prefferedPositions = new HashSet<Position>();
        private static int roundNumber = 0;
        private const int maxRoundNumber = 50;
        //private void Logger_OnLogRound(object sender, LogRoundEventArgs e) => ++this.RoundCount;

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map) {
            int robotCount = robots.Where(n => n.OwnerName == Author).Count();
            Robot.Common.Robot movingRobot = robots[robotToMoveIndex];
            prefferedPositions.Remove(movingRobot.Position);

            if ((movingRobot.Energy > 600) && (robots.Count < map.Stations.Count) && roundNumber < maxRoundNumber - 5) {
                return new CreateNewRobotCommand();
            }

            EnergyStation nearestStation = HelperMethods.FindNearestFreeStation(robots[robotToMoveIndex], map, robots);
            if (nearestStation.Position == movingRobot.Position && nearestStation.Energy > 0)
                return new CollectEnergyCommand();

            Position move = NextTarget(movingRobot, map, robots);
            prefferedPositions.Add(move);
            move = HelperMethods.OptimalMove(movingRobot.Position, move, movingRobot.Energy);

            if (move == movingRobot.Position) {
                move = HelperMethods.MinimalMove(movingRobot.Position, nearestStation.Position, movingRobot.Energy);
                prefferedPositions.Remove(move);
            }


            return new MoveCommand() { NewPosition = move };
        }

        public Position NextTarget(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots) {
            const double stationCoef = 1000.0;
            const double proximityCoef = 100.0;
            const double attackCoef = 10.0;
            var targets = new Dictionary<Position, double> { };

            foreach (var station in map.Stations) {
                if (!prefferedPositions.Contains(station.Position)
                    && HelperMethods.IsStationFree(station, movingRobot, robots)) {
                    int dist = HelperMethods.FindDistance(station.Position, movingRobot.Position);
                    targets[station.Position] = stationCoef / dist * station.Energy;
                    if (targets.Count != 0) {
                        foreach (var otherPos in targets.Keys) {
                            if (otherPos != station.Position) {
                                double proximity = proximityCoef / HelperMethods.FindDistance(station.Position, otherPos);
                                targets[station.Position] += proximity;
                                targets[otherPos] += proximity;
                            }
                        }
                    }
                }
            }
            foreach (var robot in robots.Where(n => n.OwnerName != Author).ToList()) {
                int profit = 0;
                if ((profit = HelperMethods.AttackRevenue(movingRobot, robot)) > 0) {
                    double prior = profit * attackCoef / HelperMethods.FindDistance(movingRobot.Position, robot.Position);
                    if (targets.ContainsKey(robot.Position))
                        targets[robot.Position] += prior;
                    else
                        targets[robot.Position] = prior;
                }
            }
            return targets.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        public string Author => "Chepeliuk Bohdan";
        
        public string Description => "Lab 1. Robots";
    }
}