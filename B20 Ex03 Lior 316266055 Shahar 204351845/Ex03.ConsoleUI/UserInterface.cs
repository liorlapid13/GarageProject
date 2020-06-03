using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
                    //getNewVehicleInformationAndInsertToGarage();
                    addNewVehicleToGarage();
                    r_Garage.VehicleDictionary["1234567"].Vehicle.ToString();
                    break;

            }
        }

        private void getNewVehicleInformationAndInsertToGarage()
        {
            string ownerName, ownerPhoneNumber, licenseNumber, modelName, wheelManufacturer;
            float currentAirPressure;
            VehicleCreator.eSupportedVehicles vehicleType;

            receiveVehicleOwnerInformation(out ownerName, out ownerPhoneNumber);
            Console.WriteLine("Please enter your vehicle information");

            receiveVehicleType(out vehicleType);
            receiveBasicVehicleInformation(out licenseNumber, out modelName);
            receiveWheelInformation(out currentAirPressure, out wheelManufacturer, vehicleType);

            Vehicle newVehicle = VehicleCreator.CreateNewVehicle(licenseNumber, vehicleType);

            switch (vehicleType)
            {
                //case VehicleCreator.eSupportedVehicles.Car:
                    //receiveAndInsertCarInformation((Car)newVehicle, modelName, wheelManufacturer, currentAirPressure);
                   // break;
                


                    /*
                     *  case MOTORCYCLE:
                     *      receiveAndInsertMotorcycleInformation(newVehicle, modelName, wheelManufacturer, currentAirPressure);
                     *      break;
                     *
                     *  case TRUCK:
                     *      receiveAndInsertTruckInformation(newVehicle, modelName, wheelManufacturer, currentAirPressure);
                     *      break;
                     */
            }

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
            Console.WriteLine("Please enter vehicle owner's information");
            bool isValidName, isValidPhoneNumber;
            string ownerName, ownerPhoneNumber;

            do
            {
                Console.Write("Name: ");
                ownerName = Console.ReadLine();
                isValidName = InputValidation.CheckNameInput(ownerName);
                
            }
            while(!isValidName);

            o_OwnerName = ownerName;

            do
            {
                Console.Write("Phone number: ");
                ownerPhoneNumber = Console.ReadLine();
                isValidPhoneNumber = InputValidation.CheckPhoneNumberInput(ownerPhoneNumber);
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
            bool isValidLicenseNumber, isValidModelName;
            string licenseNumber, vehicleModel;

            Console.Write("Please enter your license number: ");
            do
            {
                licenseNumber = Console.ReadLine();
                isValidLicenseNumber = InputValidation.CheckLicenseNumberInput(licenseNumber);
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

        private void receiveWheelInformation(out float o_CurrentAirPressure, out string o_WheelManufacturer, VehicleCreator.eSupportedVehicles i_VehicleType)
        {
            bool isValidWheelManufacturer, isValidCurrentAirPressure;
            string currentAirPressure, wheelManufacturer;

            Console.WriteLine("Please enter your vehicle's wheels information");

            Console.Write("Current air pressure: ");

            do
            {
                currentAirPressure = Console.ReadLine();
                isValidCurrentAirPressure = InputValidation.CheckAirPressureInput(currentAirPressure, i_VehicleType);
            }
            while(!isValidCurrentAirPressure);

            o_CurrentAirPressure = float.Parse(currentAirPressure);

            Console.Write("Manufacturer: ");

            do
            {
                wheelManufacturer = Console.ReadLine();
                isValidWheelManufacturer = string.IsNullOrEmpty(wheelManufacturer);
            }
            while (!isValidWheelManufacturer);

            o_WheelManufacturer = wheelManufacturer;
        }

        private void receiveAndInsertCarInformation(Car i_NewCar, string i_ModelName, string i_WheelManufacturer, float i_CurrentAirPressure)
        {
            Console.WriteLine("Please enter your car information");
            Engine.eEngineType engineType;
            float currentEnergyAmount, currentEnergyPercentage;

            //receiveEngineInformation(out engineType, out currentEnergyAmount, out currentEnergyPercentage, VehicleCreator.eSupportedVehicles.Car);

            Console.Write(
@"Vehicle Color
1   Red
2   White
3   Black
4   Silver
What color is your car? ");
            Car.eColor carColor = (Car.eColor)receiveEnumInput<Car.eColor>();

            Console.Write(
@"Vehicle Doors
2   Two
3   Three
4   Four
5   Five
How many doors does your car have? ");
            Car.eNumberOfDoors carNumberOfDoors = (Car.eNumberOfDoors)receiveEnumInput<Car.eNumberOfDoors>();

            i_NewCar.ModelName = i_ModelName;
            //i_NewCar.EnergyPercentageLeft = currentEnergyPercentage;
            i_NewCar.InitializeWheelsList(Vehicle.eNumberOfWheels.Car, i_WheelManufacturer, i_CurrentAirPressure, Wheel.eMaxAirPressure.Car);
            //i_NewCar.InitializeEngine(engineType, currentEnergyAmount);
            i_NewCar.CarColor = carColor;
            i_NewCar.NumberOfDoors = carNumberOfDoors;
        }

        private void receiveEngineInformation(
            out Engine.eEngineType o_EngineType,
            out float o_CurrentEnergyAmount,
            out float o_CurrentEnergyPercentage,
            VehicleCreator.eSupportedVehicles i_VehicleType)
        {
            Console.WriteLine("Please enter engine information");

            Console.Write(
                @"Engine Type
1   Gas Engine
2   Electric Engine
Please enter the type of your engine: ");
            o_EngineType = (Engine.eEngineType)receiveEnumInput<Engine.eEngineType>();

            receiveVehicleEnergyInformation(
                i_VehicleType,
                o_EngineType,
                out o_CurrentEnergyAmount,
                out o_CurrentEnergyPercentage);
        }

        private void receiveVehicleEnergyInformation(
            VehicleCreator.eSupportedVehicles i_VehicleType,
            Engine.eEngineType i_EngineType,
            out float o_CurrentEnergyAmount,
            out float o_CurrentEnergyPercentage)
        {
            
            string maxEnergyCapacityString = Enum.GetName(typeof(VehicleCreator.eSupportedVehicles), i_VehicleType);
            float maxEnergyCapacity;

            if (i_EngineType == Engine.eEngineType.Gas)
            {
                maxEnergyCapacity = (float)Enum.Parse(typeof(GasEngine.eGasCapacity), maxEnergyCapacityString);
            }
            else
            {
                maxEnergyCapacity = (float)Enum.Parse(
                                        typeof(ElectricEngine.eElectricEngineCapacityInMinutes),
                                        maxEnergyCapacityString) / 60;
            }

            string typeOfEnergy = i_EngineType == Engine.eEngineType.Electric ? "energy" : "gas";
            bool isValidEnergyAmount;
            string currentEnergyString;

            Console.Write("How much {0} is in your vehicle? ", typeOfEnergy);
            do
            {
                currentEnergyString = Console.ReadLine();
                isValidEnergyAmount = InputValidation.CheckEnergyAmount(currentEnergyString, maxEnergyCapacity, typeOfEnergy);
            }
            while (!isValidEnergyAmount);

            o_CurrentEnergyAmount = float.Parse(currentEnergyString);
            o_CurrentEnergyPercentage = o_CurrentEnergyAmount / maxEnergyCapacity * 100;
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

        //============================================================================================//

        private void addNewVehicleToGarage()
        {
            string licenseNumber;
            bool isValidLicenseNumber;

            Console.Write("Please enter your license number: ");
            do
            {
                licenseNumber = Console.ReadLine();
                isValidLicenseNumber = InputValidation.CheckLicenseNumberInput(licenseNumber);
            }
            while (!isValidLicenseNumber);

            if(r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                Console.WriteLine("The Vehicle you entered already exists in the garage");
                r_Garage.ChangeVehicleStatus(licenseNumber, Garage.VehicleInformation.eVehicleStatus.InService);
            }
            else
            {
                Vehicle newVehicle = createNewVehicle(licenseNumber);
                insertNewVehicleToGarage(licenseNumber, newVehicle);
            }
        }

        private Vehicle createNewVehicle(string i_LicenseNumber)
        {
            string[] vehicleTypes = Enum.GetNames(typeof(VehicleCreator.eSupportedVehicles));
            StringBuilder vehicleTypesOutputMessage = new StringBuilder();

            for(int i = 0; i < vehicleTypes.Length; i++)
            {
                vehicleTypesOutputMessage.AppendLine(string.Format("{0}\t{1}", i + 1, vehicleTypes[i]));
            }

            Console.Write(
@"Vehicle Types
{0}Please select vehicle type: ",
                vehicleTypesOutputMessage);

            int selectedVehicleType = receiveEnumInput<VehicleCreator.eSupportedVehicles>();

            Vehicle newVehicle = VehicleCreator.CreateNewVehicle(
                i_LicenseNumber,
                (VehicleCreator.eSupportedVehicles)selectedVehicleType);

            return newVehicle;
        }

        private void insertNewVehicleToGarage(string i_LicenseNumber, Vehicle i_NewVehicle)
        {
            string ownerName, ownerPhoneNumber;
            bool isValidInput;

            receiveVehicleOwnerInformation(out ownerName, out ownerPhoneNumber);
            List<string> userDialogueStringsList = i_NewVehicle.GetUserDialogueStrings();
            List<string> userDialogueInputsList = new List<string>();

            for(int i = 0; i < userDialogueStringsList.Count; ++i) 
            {
                isValidInput = false;
                Console.Write(userDialogueStringsList[i]);
                do
                {
                    try
                    {
                        string userInput = Console.ReadLine();

                        if(i_NewVehicle.CheckLatestUserInput(userInput, i))
                        {
                            isValidInput = true;
                            userDialogueInputsList.Add(userInput);
                        }
                    }
                    catch (FormatException)
                    {
                        Console.Write("Invalid input, please try again: ");
                    }
                    catch (ValueOutOfRangeException valueOutOfRangeException)
                    {
                        Console.Write("You must enter a number between {0} and {1}: ", valueOutOfRangeException.MinValue, valueOutOfRangeException.MaxValue);
                    }
                }
                while(!isValidInput);
            }

            i_NewVehicle.UpdateProperties(userDialogueInputsList);
            r_Garage.AddVehicleToGarage(i_LicenseNumber, ownerName, ownerPhoneNumber, i_NewVehicle);
        }
    }
}
