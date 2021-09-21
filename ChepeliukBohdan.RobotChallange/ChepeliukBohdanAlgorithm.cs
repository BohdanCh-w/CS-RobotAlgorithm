using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange {

    public class ChepeliukBohdanAlgorithm : IRobotAlgorithm {
        public static Dictionary<int, Position> prefferedPositions = new Dictionary<int, Position>();
        public static int roundNumber = 0;
        private const int maxRoundNumber = 50;

        public ChepeliukBohdanAlgorithm() => Logger.OnLogRound += new LogRoundEventHandler(this.Logger_OnLogRound);

        private void Logger_OnLogRound(object sender, LogRoundEventArgs e) => ++roundNumber;

        public void incrementRound(object sender, Robot.Tournament.LogRoundEventArgs e) {
            roundNumber++;
        }

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map) {
            int robotCount = robots.Where(n => n.OwnerName == Author).Count();
            Robot.Common.Robot movingRobot = robots[robotToMoveIndex];

            EnergyStation nearestStation = HelperMethods.FindNearestFreeStation(movingRobot, map, robots);
            if (nearestStation != null) {
                if ((movingRobot.Energy > 600) && roundNumber < maxRoundNumber - 20 &&
                    (robots.Count < map.Stations.Count || (movingRobot.Energy > 1000) &&
                    HelperMethods.FindDistance(nearestStation.Position, movingRobot.Position) < 300))
                {
                    return new CreateNewRobotCommand();
                }
            }


            int nearestStationEnergy = HelperMethods.StationEnergy(movingRobot.Position, map);
            if (nearestStationEnergy != -1) // && (nearestStationEnergy > 0 || movingRobot.Energy < 500)
                return new CollectEnergyCommand();

            Position move = NextTarget(movingRobot, map, robots);
            //prefferedPositions[robotToMoveIndex] = move;
            move = HelperMethods.OptimalMove(movingRobot.Position, move, movingRobot.Energy);

            if (move == movingRobot.Position) { 
                prefferedPositions[robotToMoveIndex] = null;
                move = HelperMethods.MinimalMove(movingRobot.Position, nearestStation.Position, movingRobot.Energy);
            } 

            return new MoveCommand() { NewPosition = move };
        } 

        public Position NextTarget(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots) {
            const double stationCoef = 1000.0;
            const double proximityCoef = 100.0;
            const double attackCoef = 10000;
            var targets = new Dictionary<Position, double> { };

            foreach (var station in map.Stations) {
                if (!prefferedPositions.Values.Contains(station.Position)
                    && HelperMethods.IsStationFree(station, movingRobot, robots)) {
                    int dist = HelperMethods.FindDistance(station.Position, movingRobot.Position);
                    targets[station.Position] = stationCoef / dist * station.Energy;
                    if (targets.Count != 0) {
                        var keys = new List<Position>(targets.Keys);
                        foreach (var otherPos in keys) {
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
                int profit = HelperMethods.AttackRevenue(movingRobot, robot);
                double prior = profit * attackCoef / HelperMethods.FindDistance(movingRobot.Position, robot.Position) * movingRobot.Energy;
                if (targets.ContainsKey(robot.Position))
                    targets[robot.Position] += prior;
                else
                    targets[robot.Position] = prior;
            }
            return targets.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        public string Author => "Chepeliuk Bohdan";
        
        public string Description => "Lab 1. Robots";
    }
}