using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallengeApplication.Enums;
using ElevatorChallengeApplication.Interfaces;
using ElevatorChallengeApplication.Models;

namespace ElevatorChallengeApplication.Controllers
{
    public class ElevatorController : IElevatorController
    {
        private readonly List<Elevator> allElevators;
        public List<Floor> Floors { get; private set; }
        public List<Elevator> Elevators => allElevators;

        public ElevatorController(int numberOfElevators, int numberOfFloors, int elevatorCapacity)
        {
            if (numberOfElevators <= 0 || numberOfFloors <= 0 || elevatorCapacity <= 0)
            {
                throw new ArgumentException("Number of elevators, floors, and capacity must be greater than zero.");
            }

            // Initialize the list of elevators
            allElevators = new List<Elevator>();
            for (int i = 0; i < numberOfElevators; i++)
            {
                allElevators.Add(new Elevator(elevatorCapacity));
            }

            // Initialize the list of floors
            Floors = Enumerable.Range(1, numberOfFloors).Select(floorNumber => new Floor(floorNumber, elevatorCapacity)).ToList();
        }

        // Method to call an elevator to a specific floor
        public void CallElevator(int floorNumber)
        {
            if (floorNumber < 1 || floorNumber > Floors.Count)
            {
                Console.WriteLine($"Invalid floor number: {floorNumber}. Please try again.");
                return;
            }

            Floor floor = Floors[floorNumber - 1];

            // Check if there are people waiting on the floor
            if (floor.NumberOfPeopleWaiting == 0)
            {
                Console.WriteLine($"No one is waiting on Floor {floorNumber}.");
                return;
            }

            // Find the nearest available elevator to the floor
            Elevator nearestAvailableElevator = FindNearestAvailableElevator(floorNumber);

            if (nearestAvailableElevator == null)
            {
                Console.WriteLine("All elevators are currently busy or at maximum capacity. Please wait.");
                return;
            }

            // Move the nearest available elevator to the floor
            nearestAvailableElevator.MoveToFloor(floorNumber, GetElevatorIndex(nearestAvailableElevator) + 1);

            int capacity = 0;
            while (floor.NumberOfPeopleWaiting > 0 && nearestAvailableElevator.NumberOfPeople < nearestAvailableElevator.Capacity)
            {
                // Add a person to the elevator and remove them from the floor
                if (nearestAvailableElevator.AddPerson())
                {
                    floor.RemovePerson();
                    capacity++;
                }
                else
                {
                    Console.WriteLine("Elevator is at maximum capacity. Cannot add more people.");
                    break;
                }
            }

            // If passengers were added to the elevator, inform the user
            if (capacity > 0)
            {
                Console.WriteLine($"Elevator {GetElevatorIndex(nearestAvailableElevator) + 1} picked up {capacity} people from Floor {floorNumber}.");
            }

            bool validInput = false;

            do
            {
                // Get the destination floor from the user's input
                if (TryGetValidDestinationFloor(out int destinationFloor))
                {
                    // Move the elevator to the destination floor and offload
                    nearestAvailableElevator.MoveToFloor(destinationFloor, GetElevatorIndex(nearestAvailableElevator) + 1);
                    nearestAvailableElevator.OffloadPeople();
                    validInput = true;
                }
            } while (!validInput);
        }

        // Method to set the number of people waiting on a specific floor
        public void SetNumberOfPeopleWaiting(int floorNumber, int numberOfPeopleWaiting)
        {
            if (floorNumber < 1 || floorNumber > Floors.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(floorNumber), "Invalid floor number.");
            }

            Floors[floorNumber - 1].NumberOfPeopleWaiting = numberOfPeopleWaiting;
        }

        // Method to get the index of an elevator
        public int GetElevatorIndex(Elevator elevator)
        {
            return allElevators.IndexOf(elevator);
        }

        // helper method to find the nearest available elevator to a specific floor
        public Elevator FindNearestAvailableElevator(int floorNumber)
        {
            return allElevators
                .Where(elevator => !elevator.IsMoving && elevator.NumberOfPeople < elevator.Capacity)
                .OrderBy(elevator => Math.Abs(elevator.CurrentFloor - floorNumber))
                .FirstOrDefault();
        }

        // Private helper method to get a valid destination floor from user input
        private bool TryGetValidDestinationFloor(out int destinationFloor)
        {
            Console.Write("Enter the destination floor: ");
            if (int.TryParse(Console.ReadLine(), out destinationFloor) && destinationFloor >= 1 && destinationFloor <= Floors.Count)
            {
                return true;
            }

            // If the input is not valid, inform the user and ask again
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid floor number. Please try again.");
            Console.ResetColor();
            return false;
        }
    }
}
