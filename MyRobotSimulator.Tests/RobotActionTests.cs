using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyRobotSimulator.Implementation;
using MyRobotSimulator.Interface;


namespace MyRobotSimulator.Tests
{
    [TestClass]
    public class RobotActionTests
    {
        private IRobotAction robotAction;
        [TestInitialize]
        public void Initialise()
        {
            var mockLogger = new Mock<ILogger>();
            robotAction = new RobotAction(mockLogger.Object);

        }
        [TestMethod]
        public void Robot_NotPlaced_AndTryToMove()
        {
            
            var result = robotAction.Move();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot move until it has been placed on the table.", robotAction.Error);
        }

        [TestMethod]
        public void Robot_NotPlaced_AndTryToTurn()
        {
            var result = robotAction.Left();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot turn until it has been placed on the table.", robotAction.Error);
        }

        [TestMethod]
        public void Robot_NotPlaced_TryToReportItsPosition()
        {
            var result = robotAction.Report();
            Assert.AreEqual("Robot cannot report it's position until it has been placed on the table.", robotAction.Error);
        }

        [TestMethod]
        public void Robot_PlacedOffTable_CannotBePlaced()
        {
            var result = robotAction.Place(-1, 0, Facing.North);
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot be placed there.", robotAction.Error);

            result = robotAction.Place(0, 6, Facing.North);
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot be placed there.", robotAction.Error);
        }

        [TestMethod]
        public void Robot_Placed_CanReportItsPosition()
        {
            var result = robotAction.Place(3, 2, Facing.East);
            var position = robotAction.Report();
            Assert.IsTrue(result);
            Assert.AreEqual("", robotAction.Error);
            Assert.AreEqual("3,2,EAST", position);
        }

        [TestMethod]
        public void Robot_PlacedAndTurnedLeft_ReportsCorrectPosition()
        {
            robotAction.Place(1, 1, Facing.North);
            robotAction.Left();
            Assert.AreEqual("1,1,WEST", robotAction.Report());
        }

        [TestMethod]
        public void Robot_PlacedAndTurnedRight_ReportsCorrectPosition()
        {
            robotAction.Place(1, 1, Facing.North);
            robotAction.Right();
            Assert.AreEqual("1,1,EAST", robotAction.Report());
        }

        [TestMethod]
        public void Robot_PlacedAndMovedOffTable_CannotBeMoved()
        {
            robotAction.Place(5, 5, Facing.North);
            var result = robotAction.Move();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot be moved there.", robotAction.Error);
            Assert.AreEqual("5,5,NORTH", robotAction.Report());
        }

        [TestMethod]
        public void Robot_PlacedAndMoved_ReportsCorrectPosition()
        {
            robotAction.Place(1, 1, Facing.North);
            robotAction.Move();
            Assert.AreEqual("1,2,NORTH", robotAction.Report());
        }

        [TestMethod]
        public void Robot_PlacedMovedAndTurned_ReportsCorrectPosition()
        {
            robotAction.Place(1, 2, Facing.East);
            robotAction.Move();
            robotAction.Move();
            robotAction.Left();
            robotAction.Move();
            Assert.AreEqual("3,3,NORTH", robotAction.Report());
        }
    }
}
