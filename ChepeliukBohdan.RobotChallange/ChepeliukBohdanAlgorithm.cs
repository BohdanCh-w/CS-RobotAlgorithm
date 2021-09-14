using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot.Common;

namespace ChepeliukBohdan.RobotChallange {
    public class ChepeliukBohdanAlgorithm : IRobotAlgorithm {
        public static HashSet<Position> prefferedStations = new HashSet<Position>();

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map) {
            Robot.Common.Robot movingRobot = robots[robotToMoveIndex];
            if ((movingRobot.Energy > 500) && (robots.Count < map.Stations.Count)) {
                return new CreateNewRobotCommand();
            }

            Position stationPosition = HelperMethods.FindNearestFreeStation(robots[robotToMoveIndex], map, robots);

            if (stationPosition == null)
                    return null;
            if (stationPosition == movingRobot.Position)
                return new CollectEnergyCommand();
            else
                return new MoveCommand() { NewPosition = stationPosition };
        }

        public string Author => "Chepeliuk Bohdan";
        
        public string Description => "Lab 1. Robots";
    }
}