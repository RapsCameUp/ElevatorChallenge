using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallengeApplication.Enums;

namespace ElevatorChallengeApplication.Models
{
    public class Elevator
    {
        // Properties
        public int CurrentFloor { get; private set; }
        public bool IsMoving { get; set; }
        public Direction CurrentDirection { get; set; }
        public int Capacity { get; private set; }
        public int NumberOfPeople { get; private set; }

        // Constructor
        public Elevator(int capacity)
        {
            CurrentFloor = 1;
            IsMoving = false;
            CurrentDirection = Direction.Stationary;
            Capacity = capacity;
            NumberOfPeople = 0;
        }

        // Methods
        public void MoveToFloor(int floor, int elevatorIndex = -1)
        {
            if (floor == CurrentFloor)
            {
                Console.WriteLine($"Elevator {(elevatorIndex != -1 ? elevatorIndex : "")} is already on floor {floor}.");
                return;
            }

            // Determine the direction to move
            CurrentDirection = floor > CurrentFloor ? Direction.Up : Direction.Down;

            // Simulate moving the elevator one floor at a time
            while (CurrentFloor != floor)
            {
                // Move the elevator one floor in the specified direction
                if (CurrentDirection == Direction.Up)
                {
                    CurrentFloor++;
                }
                else
                {
                    CurrentFloor--;
                }

                // Simulate the time it takes to move between floors (you can adjust this as needed)
                Thread.Sleep(1000); // Wait for 1 second to simulate movement

                // Display the current floor and elevator index while moving
                Console.WriteLine($"Elevator {(elevatorIndex != -1 ? elevatorIndex : "")} is moving {CurrentDirection.ToString().ToLower()} and is now on floor {CurrentFloor}.");
            }

            // Update the elevator state after reaching the requested floor
            CurrentDirection = Direction.Stationary;
            IsMoving = false;
            Console.WriteLine($"Elevator {(elevatorIndex != -1 ? elevatorIndex : "")} has arrived at floor {CurrentFloor}.");

        }

        // This method is used for adding a person into the elevator
        public bool AddPerson()
        {
            if (NumberOfPeople < Capacity)
            {
                NumberOfPeople++;
                return true;
            }
            return false;
        }

        // This method is used for offloading people from the elevator
        public void OffloadPeople()
        {
            if (NumberOfPeople > 0)
            {
                NumberOfPeople = 0;
                Console.WriteLine("Successfully Offloaded.");
            }
        }
    }
}
