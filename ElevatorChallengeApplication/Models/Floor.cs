using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallengeApplication.Models
{
    public class Floor
    {
        // Properties
        public int FloorNumber { get; set; }
        public int NumberOfPeopleWaiting { get; set; }

        // Constructor
        public Floor(int floorNumber, int capacity)
        {
            FloorNumber = floorNumber;
            NumberOfPeopleWaiting = 0;
        }

        // Method to remove a person from the floor (after a person gets picked up by the elevator)
        public void RemovePerson()
        {
            if (NumberOfPeopleWaiting > 0)
            {
                NumberOfPeopleWaiting--;
            }
        }
    }
}
