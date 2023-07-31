using ElevatorChallengeApplication.Controllers;
using ElevatorChallengeApplication.Enums;
using ElevatorChallengeApplication.Interfaces;
using ElevatorChallengeApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorChallengeMSTests
{
    [TestClass]
    public class ElevatorControllerTests
    {
        [TestMethod]
        public void CallElevator_WhenNoPeopleWaiting_ShouldNotMoveAnyElevator()
        {
            int numberOfElevators = 2;
            int numberOfFloors = 10;
            int elevatorCapacity = 8;
            IElevatorController elevatorController = new ElevatorController(numberOfElevators, numberOfFloors, elevatorCapacity);

            // Act
            elevatorController.CallElevator(1);

            // Check that none of the elevators moved
            foreach (Elevator elevator in elevatorController.Elevators)
            {
                Assert.IsFalse(elevator.IsMoving);
            }
        }

        [TestMethod]
        public void SetNumberOfPeopleWaiting_ShouldUpdatePeopleWaitingOnFloor()
        {
            //Test setting the number of people waiting on a floor
            int numberOfElevators = 2;
            int numberOfFloors = 10;
            int elevatorCapacity = 8;
            IElevatorController elevatorController = new ElevatorController(numberOfElevators, numberOfFloors, elevatorCapacity);
            int floorNumber = 3;
            int numberOfPeopleWaiting = 7;

            // Act
            elevatorController.SetNumberOfPeopleWaiting(floorNumber, numberOfPeopleWaiting);

            // Check that the number of people waiting on the specified floor is updated correctly
            Floor floor = elevatorController.Floors[floorNumber - 1];
            Assert.AreEqual(numberOfPeopleWaiting, floor.NumberOfPeopleWaiting);
        }


        [TestMethod]
        public void FindNearestAvailableElevator_ElevatorsNotMoving_ReturnsNearestAvailableElevator()
        {
            int numberOfElevators = 3;
            int numberOfFloors = 10;
            int elevatorCapacity = 8;
            IElevatorController elevatorController = new ElevatorController(numberOfElevators, numberOfFloors, elevatorCapacity);

            // Set some people waiting on floor 3
            elevatorController.SetNumberOfPeopleWaiting(3, 2);

            // Act
            Elevator nearestElevator = ((ElevatorController)elevatorController).FindNearestAvailableElevator(3);

            // Check that the nearest available elevator is returned (elevator 1, as all elevators start at floor 1)
            Assert.AreEqual(elevatorController.Elevators[0], nearestElevator);
        }

        [TestMethod]
        public void SetNumberOfPeopleWaiting_InvalidFloorNumber_ThrowsArgumentOutOfRangeException()
        {
            int numberOfElevators = 2;
            int numberOfFloors = 10;
            int elevatorCapacity = 8;
            IElevatorController elevatorController = new ElevatorController(numberOfElevators, numberOfFloors, elevatorCapacity);

            // Try to set the number of people waiting on an invalid floor number (should throw an exception)
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => elevatorController.SetNumberOfPeopleWaiting(0, 5));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => elevatorController.SetNumberOfPeopleWaiting(numberOfFloors + 1, 3));
        }

    }
}