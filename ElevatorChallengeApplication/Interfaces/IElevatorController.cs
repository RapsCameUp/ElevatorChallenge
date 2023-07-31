using ElevatorChallengeApplication.Enums;
using ElevatorChallengeApplication.Models;

namespace ElevatorChallengeApplication.Interfaces
{
    public interface IElevatorController
    {
        List<Floor> Floors { get; }
        List<Elevator> Elevators { get; }

        void CallElevator(int floorNumber);
        void SetNumberOfPeopleWaiting(int floorNumber, int numberOfPeopleWaiting);
        int GetElevatorIndex(Elevator elevator);
    }
}
