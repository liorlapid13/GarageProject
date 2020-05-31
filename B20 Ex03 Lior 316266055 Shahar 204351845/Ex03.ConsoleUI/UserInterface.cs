using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
            RefuelVehicle,
            RechargeVehicle,
            DisplayVehicleInformation
        }

        public UserInterface()
        {
            r_Garage = new Garage();
        }

        public void SelectFromUserMenu()
        {
            presentUserMenu();
            
            int menuOption = receiveEnumInput<eGarageActions>();

            performGarageAction((eGarageActions)menuOption);
        }

        private void performGarageAction(eGarageActions userMenuSelection)
        {
            switch(userMenuSelection)
            {
                case 0:
                    //User selection failed
                    break;
                case eGarageActions.InsertNewVehicle:
                    getNewVehicleInformationAndInsertToGarage();
                    break;

            }
        }

        private void getNewVehicleInformationAndInsertToGarage()
        {
            string ownerName, ownerPhoneNumber, licenseNumber, modelName;
            VehicleCreator.eSupportedVehicles vehicleType;

            receiveVehicleOwnerInformation(out ownerName, out ownerPhoneNumber);
            Console.WriteLine("Please enter your vehicle information");

            receiveVehicleType(out vehicleType);
            receiveBasicVehicleInformation(out licenseNumber, out modelName);
            //receiveWheelInformation(out currentAirPressure, out wheelManufacturer); //TODO

            //Vehicle newVehicle;

            switch (vehicleType)
            {
                /*
                 *  case CAR:
                 *      newVehicle = receiveInformationAndCreateCar(licenseNumber, modelName, wheelManufacturer, currentAirPressure);
                 *
                 *  case MOTORCYCLE:
                 *      newVehicle = receiveInformationAndCreateMotorcycle(licenseNumber, modelName, wheelManufacturer, currentAirPressure);
                 *
                 *  case TRUCK:
                 *      newVehicle = receiveInformationAndCreateTruck(licenseNumber, modelName, wheelManufacturer, currentAirPressure);
                 */
            }

            Console.Write("Please enter your wheels' manufacturer: ");
            string wheelManufacturer = Console.ReadLine();

            Console.Write("How much air pressure is in your wheels? ");
            string currentAirPressure = Console.ReadLine();

            Console.Write(
@"Vehicle Color
1   Red
2   White
3   Black
4   Silver
What color is your vehicle? ");
            string vehicleColor = Console.ReadLine();

            Console.Write(
@"Vehicle Doors
2   Two
3   Three
4   Four
5   Five
How many doors does your vehicle have? ");
            string numberOfDoors = Console.ReadLine();

            Console.Write(
@"Engine type
1   Gas Engine
2   Electric Engine
Please enter the type of your engine: ");
            string engineType = Console.ReadLine();

            Console.Write(
@"License Type
1   A
2   A1
3   AA
4   B
Please enter the type of your license: ");
            string licenseType = Console.ReadLine();

            Console.Write("Please enter your motorcycle's engine capacity: ");
            string engineCapacity = Console.ReadLine();

            Console.Write(
@"Hazardous Materials
1   Yes
2   No
Is your truck carrying hazardous materials? ");
            string isCarryingHazardousMaterials = Console.ReadLine();

            Console.Write("Please enter your truck's load capacity: ");
            string loadCapacity = Console.ReadLine();

            //r_Garage.AddVehicleToGarage(licenseNumber, ownerName, ownerPhoneNumber, newVehicle);
        }

        private void receiveVehicleOwnerInformation(out string o_OwnerName, out string o_PhoneNumber)
        {
            Console.WriteLine("Hello, please enter your personal information");
            bool isValidName, isValidPhoneNumber;
            string ownerName, ownerPhoneNumber;

            do
            {
                Console.Write("Name: ");
                ownerName = Console.ReadLine();
                isValidName = checkNameInput(ownerName);
                
            }
            while(!isValidName);

            o_OwnerName = ownerName;

            do
            {
                Console.Write("Phone number: ");
                ownerPhoneNumber = Console.ReadLine();
                isValidPhoneNumber = checkPhoneNumberInput(ownerPhoneNumber);
            }
            while(!isValidPhoneNumber);

            o_PhoneNumber = ownerPhoneNumber;
        }

        private void receiveVehicleType(out VehicleCreator.eSupportedVehicles o_VehicleType)
        {
            Console.Write(
@"Vehicle Type
1   Motorcycle
2   Car
3   Truck
Please enter your vehicle type: ");

            int supportedVehicleNumber = receiveEnumInput<VehicleCreator.eSupportedVehicles>();

            o_VehicleType = (VehicleCreator.eSupportedVehicles)supportedVehicleNumber;
        }

        private void receiveBasicVehicleInformation(out string o_LicenseNumber, out string o_ModelName)
        {
            bool isValidLicenseNumber = false, isValidModelName = false;
            string licenseNumber, vehicleModel;

            Console.Write("Please enter your license number: ");
            do
            {
                licenseNumber = Console.ReadLine();
                isValidLicenseNumber = checkLicenseNumberInput(licenseNumber);
            }
            while(!isValidLicenseNumber);

            o_LicenseNumber = licenseNumber;

            Console.Write("Please enter the model of your vehicle: ");
            do
            {
                vehicleModel = Console.ReadLine();
                isValidModelName = string.IsNullOrEmpty(vehicleModel);
            }
            while(!isValidModelName);

            o_ModelName = vehicleModel;
        }

        private bool checkNameInput(string i_Name)
        {
            bool isValidName = true;

            if(i_Name.Length == 0)
            {
                Console.Write("You must enter a name, please try again: ");
                isValidName = false;
            }
            else
            {
                foreach(char c in i_Name)
                {
                    if(!char.IsLetter(c) && c != ' ')
                    {
                        Console.Write("Name must only include letters or spaces, please try again: ");
                        isValidName = false;
                        break;
                    }
                }
            }

            return isValidName;
        }

        private bool checkPhoneNumberInput(string i_PhoneNumber)
        {
            bool isValidPhoneNumber = true;

            if(i_PhoneNumber.Length != 10)
            {
                Console.Write("Phone number must be 10-digits long, please try again: ");
                isValidPhoneNumber = false;
            }
            else
            {
                foreach(char c in i_PhoneNumber) 
                {
                    if(!char.IsDigit(c)) 
                    {
                        Console.Write("Phone number must include only digits, please try again: ");
                        isValidPhoneNumber = false;
                        break;
                    }
                }
            }
            
            return isValidPhoneNumber;
        }

        private bool checkLicenseNumberInput(string i_LicenseNumber)
        {
            bool isValidLicenseNumber = true;

            if(i_LicenseNumber.Length < 7 || i_LicenseNumber.Length > 8)
            {
                Console.Write("License number must be 7-8 digits, please try again: ");
                isValidLicenseNumber = false;
            }
            else
            {
                foreach(char c in i_LicenseNumber)
                {
                    if(!char.IsDigit(c))
                    {
                        Console.Write("License number must contain digits only, please try again: ");
                        isValidLicenseNumber = false;
                        break;
                    }
                }
            }

            return isValidLicenseNumber;
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
6   Recharge vehicle
7   Display vehicle information
What would you like to do? ");
        }

        private int receiveEnumInput<T>()
        {
            int selectedOption = -1;
            bool isValidInput = false;

            do
            {
                try
                {
                    string userSelection = Console.ReadLine();
                    selectedOption = int.Parse(userSelection);
                    isValidInput = isInEnumRange<T>(selectedOption);
                }
                catch (FormatException formatException)
                {
                    Console.Write("You must enter a number, please try again: ");
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.Write("You must select an option from the menu, please try again: ");
                }
            }
            while (!isValidInput);

            return selectedOption;
        }

        private bool isInEnumRange<T>(int i_Selection)
        {
            if (!Enum.IsDefined(typeof(T), i_Selection))
            {
                throw new ValueOutOfRangeException(Enum.GetValues(typeof(T)).Length, 1);
            }

            return true;
        }
    }
}
