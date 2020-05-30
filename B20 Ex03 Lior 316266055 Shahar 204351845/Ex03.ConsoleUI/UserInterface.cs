using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly Garage r_Garage;

        public enum eGarageActions
        {
            InsertNewVehicle = 1,
            ShowAllVehiclesInGarage,
            ChangeVehicleStatus,
            InflateWheels,
            FuelVehicle,
            ChargeVehicle,
            DisplayVehicleInformation
        }

        public UserInterface()
        {
            r_Garage = new Garage();
        }

        public void SelectFromUserMenu()
        {
            presentUserMenu();
            bool isValidInput = false;

            do
            {
                try
                {
                    string userSelection = Console.ReadLine();
                    int menuOption = int.Parse(userSelection);
                    isValidInput = isValidGarageAction(menuOption);
                }
                catch(FormatException formatException)
                {
                    Console.Write("You must enter a number, please try again: ");
                }
                catch(ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.Write("You must select an option from the menu, please try again: ");
                }
            }
            while(!isValidInput);
        }

        private bool isValidGarageAction(int i_GarageAction)
        {
            if(!Enum.IsDefined(typeof(eGarageActions), i_GarageAction))
            {
                throw new ValueOutOfRangeException(Enum.GetValues(typeof(eGarageActions)).Length, 1);
            }

            return true;
        }

        private void presentUserMenu()
        {
            Console.Write(
@"Welcome to the Garage!
1   Insert a new vehicle
2   Show all vehicles
3   Change vehicle status
4   Inflate tires
5   Refuel vehicle
6   Charge vehicle
7   Display vehicle information
What would you like to do? ");
        }
    }
}
