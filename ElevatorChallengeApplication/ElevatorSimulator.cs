using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorChallengeApplication.Enums;
using ElevatorChallengeApplication.Interfaces;

namespace ElevatorChallengeApplication
{
    public class ElevatorSimulator
    {
        private readonly IElevatorController elevatorController;

        public ElevatorSimulator(IElevatorController elevatorController)
        {
            this.elevatorController = elevatorController;
        }

        public void Run()
        {
            try
            {
                // Keep the program running to simulate the elevators' movements and user interactions
                while (true)
                {
                    Console.WriteLine();
                    // Show the status of elevators and floors
                    ShowElevatorStatus();
                    ShowFloorStatus();

                    // Display the menu options
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Call an elevator");
                    Console.WriteLine("2. Set the number of people waiting on a floor");
                    Console.WriteLine("3. Quit the application");
                    Console.WriteLine();

                    var input = Console.ReadKey(intercept: true).Key;

                    switch (input)
                    {
                        case ConsoleKey.D1: // Call an elevator to a specific floor
                        case ConsoleKey.NumPad1:
                            CallElevator();
                            break;

                        case ConsoleKey.D2: // Set the number of people waiting on a floor
                        case ConsoleKey.NumPad2:
                            SetNumberOfPeopleWaiting();
                            break;

                        case ConsoleKey.D3: // Quit the application
                        case ConsoleKey.NumPad3:
                            return;

                        default:
                            DisplayErrorMessage("\nInvalid input. Please choose a valid option (1, 2, or 3).");
                            break;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void CallElevator()
        {
            // get input from the user, if valid input - call the elevator
            int floorNumber;
            if (ReadInt("Enter the floor number to call the elevator: ", out floorNumber, 1, elevatorController.Floors.Count))
            {
                Console.Clear();
                elevatorController.CallElevator(floorNumber);
            }
            else
            {
                DisplayErrorMessage("\nInvalid floor number. Please try again.");
            }
        }

        private void SetNumberOfPeopleWaiting()
        {
            //get input from the user - if valid input, set the number of people waiting in the specified floor
            int floorNumberToSet;
            if (ReadInt("Enter the floor number to set the number of people waiting: ", out floorNumberToSet, 1, elevatorController.Floors.Count))
            {
                int numberOfPeopleWaiting;
                if (ReadInt("Enter the number of people waiting on this floor: ", out numberOfPeopleWaiting, 0, int.MaxValue))
                {
                    elevatorController.SetNumberOfPeopleWaiting(floorNumberToSet, numberOfPeopleWaiting);
                    Console.Clear(); // Clear the console to show updated status
                }
                else
                {
                    DisplayErrorMessage("\nInvalid number of people. Please try again.");
                }
            }
            else
            {
                DisplayErrorMessage("\nInvalid floor number. Please try again.");
            }
        }

        private void ShowElevatorStatus()
        {
            Console.WriteLine("Elevator Status:");
            foreach (var elevator in elevatorController.Elevators)
            {
                Console.WriteLine($"Elevator {elevatorController.GetElevatorIndex(elevator) + 1} - Floor: " +
                    $"{elevator.CurrentFloor}, Direction: {elevator.CurrentDirection}, People: {elevator.NumberOfPeople}");
            }
            Console.WriteLine();
        }

        private void ShowFloorStatus()
        {
            Console.WriteLine("Floor Status:");
            foreach (var floor in elevatorController.Floors)
            {
                Console.WriteLine($"Floor {floor.FloorNumber} - People Waiting: {floor.NumberOfPeopleWaiting}");
            }
            Console.WriteLine();
        }

        // Helper method to read an integer input and validate its range
        private static bool ReadInt(string message, out int result, int minValue, int maxValue)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out result))
            {
                if (result >= minValue && result <= maxValue)
                {
                    return true;
                }
            }
            return false;
        }

        private static void DisplayErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red; // Set the text color to red
            Console.WriteLine(message);
            Console.ResetColor();  // Reset the text color to default   
        }
    }
}
