using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyRobotSimulator.Implementation;

namespace MyRobotSimulator.Tests
{
    [TestClass]
    public class RobotDriverTests
    {
        [TestMethod]
        public void RobotCommander_InitialisedRobotCommander_ControlsRobot()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            Assert.IsNotNull("");
        }

        [TestMethod]
        public void RobotCommander_EmptyCommand_ReportsInvalid()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            var response = commander.Command("");
            Assert.AreEqual(@"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.", response);
        }

        [TestMethod]
        public void RobotCommander_UnrecognisedCommand_ReportsInvalid()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            var response = commander.Command("XXXX");
            Assert.AreEqual(@"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.", response);
        }

        [TestMethod]
        public void RobotCommander_RecognisedCommand_ReportsValid()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            var response = commander.Command("MOVE");
            Assert.AreEqual("Robot cannot move until it has been placed on the table.", response);
        }

        [TestMethod]
        public void RobotCommander_PlaceCommandWithNoArguments_ReportsInvalid()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            var response = commander.Command("PLACE");
            Assert.AreEqual(@"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.", response);
        }

        [TestMethod]
        public void RobotCommander_PlaceCommandWithInvalidArguments_ReportsInvalid()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            var response = commander.Command("PLACE XXX");
            Assert.AreEqual(@"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.", response);
            response = commander.Command("PLACE 1,X,NORTH");
            Assert.AreEqual(@"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.", response);
            response = commander.Command("PLACE X,1,NORTH");
            Assert.AreEqual(@"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.", response);
            response = commander.Command("PLACE 1,1,XXX");
            Assert.AreEqual(@"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.", response);
        }

        [TestMethod]
        public void RobotCommander_PlacedAndTurnedLeft_ReportsCorrectPosition()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            commander.Command("PLACE 1,1,NORTH");
            commander.Command("LEFT");
            Assert.AreEqual("1,1,WEST", commander.Command("REPORT"));
        }

        [TestMethod]
        public void RobotCommander_PlacedAndTurnedRight_ReportsCorrectPosition()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            commander.Command("PLACE 1,1,NORTH");
            commander.Command("RIGHT");
            Assert.AreEqual("1,1,EAST", commander.Command("REPORT"));
        }

        [TestMethod]
        public void RobotCommander_PlacedAndMovedOffTable_CannotBeMoved()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            commander.Command("PLACE 5,5,NORTH");
            commander.Command("MOVE");
            Assert.AreEqual("5,5,NORTH", commander.Command("REPORT"));
        }

        [TestMethod]
        public void RobotCommander_PlacedAndMoved_ReportsCorrectPosition()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            commander.Command("PLACE 1,1,NORTH");
            commander.Command("MOVE");
            Assert.AreEqual("1,2,NORTH", commander.Command("REPORT"));
        }

        public void RobotCommander_PlacedMovedAndTurned_ReportsCorrectPosition()
        {
            var commander = new RobotCommander(new Mock<ILogger>().Object,new RobotAction(new Mock<ILogger>().Object));
            commander.Command("PLACE 1,2,EAST");
            commander.Command("MOVE");
            commander.Command("MOVE");
            commander.Command("LEFT");
            commander.Command("MOVE");
            Assert.AreEqual("3,3,NORTH", commander.Command("REPORT"));
        }
    }
}
