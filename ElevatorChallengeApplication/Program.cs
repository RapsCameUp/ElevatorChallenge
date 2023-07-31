using ElevatorChallengeApplication;
using ElevatorChallengeApplication.Controllers;
using ElevatorChallengeApplication.Interfaces;

try
{
    // Initialize elevator controller with 2 elevators, 10 floors, and an elevator capacity of 8
    IElevatorController elevatorController = new ElevatorController(2, 10, 8);

    ElevatorSimulator simulator = new ElevatorSimulator(elevatorController);
    simulator.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}