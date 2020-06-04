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
        private const int k_TerminateProgram = 8;
        private readonly Garage r_Garage;

        public enum eGarageActions
        {
            InsertNewVehicle = 1,
            ShowAllLicensePlatesOfVehiclesInGarage,
            ChangeVehicleStatus,
            InflateWheels,
            RefuelVehicle,
            ChargeVehicle,
            DisplayVehicleInformation,
            ExitProgram
        }

        public UserInterface()
        {
            r_Garage = new Garage();
        }

        public void StartProgram()
        {
            MainMenu();
        }

        public void MainMenu()
        {
            bool terminateProgram = false;

            while(!terminateProgram)
            {
                presentUserMenu();

                int menuOption = receiveEnumInput<eGarageActions>();

                if (menuOption == k_TerminateProgram)
                {
                    terminateProgram = true;
                }

                performGarageAction((eGarageActions)menuOption);
                Console.Clear();
            }
        }

        private void performGarageAction(eGarageActions userMenuSelection)
        {
            switch(userMenuSelection)
            {
                case eGarageActions.InsertNewVehicle:
                    addNewVehicleToGarage();
                    break;
                case eGarageActions.ShowAllLicensePlatesOfVehiclesInGarage:
                    showAllLicensePlates();
                    break;
                case eGarageActions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eGarageActions.InflateWheels:
                    inflateWheels();
                    break;
                case eGarageActions.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eGarageActions.ChargeVehicle:
                    chargeVehicle();
                    break;
            }
        }

        private string receiveLicenseNumberInput()
        {
            bool isValidLicenseNumber;
            string licenseNumber;

            Console.Write("Please enter license number: ");
            do
            {
                licenseNumber = Console.ReadLine();
                isValidLicenseNumber = InputValidation.CheckLicenseNumberInput(licenseNumber);
            }
            while (!isValidLicenseNumber);

            return licenseNumber;
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
8   Exit
What would you like to do? ");
        }

        private int receiveEnumInput<T>()
        {
            int selectedOption = -1;
            bool isValidInput = false;

            while(!isValidInput)
            {
                string userSelection = Console.ReadLine();

                if(int.TryParse(userSelection, out selectedOption))
                {
                    isValidInput = isInEnumRange<T>(selectedOption);
                }

                if(!isValidInput)
                {
                    Console.Write("Invalid input, please try again: ");
                }
            }

            return selectedOption;
        }

        private bool isInEnumRange<T>(int i_Selection)
        {
            return Enum.IsDefined(typeof(T), i_Selection);
        }

        private void addNewVehicleToGarage()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if (r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
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

        private void showAllLicensePlates()
        {
            Console.WriteLine(
                @"Filter Vehicle List
1   In Service
2   Repaired
3   Paid
4   All Vehicles
Please select a filter option: ");
            bool isValidInput = false;
            int userSelectionInt;
            Garage.VehicleInformation.eVehicleStatus? vehicleStatusFilter = null;

            while(!isValidInput)
            {
                string userSelection = Console.ReadLine();

                if(!int.TryParse(userSelection, out userSelectionInt))
                {
                    Console.Write("You must enter a number, please try again: ");
                }
                else if(!isInEnumRange<Garage.VehicleInformation.eVehicleStatus>(userSelectionInt))
                {
                    if(userSelection == "4")
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.Write("You must enter a number between 1 and 4: ");
                    }
                }
                else
                {
                    vehicleStatusFilter = (Garage.VehicleInformation.eVehicleStatus)userSelectionInt;
                    isValidInput = true;
                }
            }

            presentAllLicensePlatesOfVehiclesInGarage(vehicleStatusFilter);
        }

        private void presentAllLicensePlatesOfVehiclesInGarage(Garage.VehicleInformation.eVehicleStatus? i_VehicleStatusToPresent)
        {
            if (!i_VehicleStatusToPresent.HasValue)
            {
                Console.WriteLine("All vehicles in garage:");
                foreach(KeyValuePair<string, Garage.VehicleInformation> vehicleInformation in r_Garage.VehicleDictionary)
                {
                    Console.WriteLine(vehicleInformation.Key);
                }
            }
            else
            {
                Console.WriteLine("All vehicles with status: {0} in garage", i_VehicleStatusToPresent.Value);
                foreach(KeyValuePair<string, Garage.VehicleInformation> vehicleInformation in r_Garage.VehicleDictionary)
                {
                    if(vehicleInformation.Value.VehicleStatus == i_VehicleStatusToPresent.Value)
                    {
                        Console.WriteLine(vehicleInformation.Key);
                    }
                }
            }
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if(r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                Console.Write(
                    @"Status Options
1   In Service
2   Repaired
3   Paid
Please select desired status: ");
                int userSelection = receiveEnumInput<Garage.VehicleInformation.eVehicleStatus>();

                r_Garage.ChangeVehicleStatus(licenseNumber, (Garage.VehicleInformation.eVehicleStatus)userSelection);
            }
            else
            {
                Console.WriteLine("There is no vehicle with license number: {0} in the garage, returning to main menu..", licenseNumber);
            }
        }

        private void inflateWheels()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if (r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                r_Garage.VehicleDictionary[licenseNumber].Vehicle.InflateAllWheels();
            }
            else
            {
                Console.WriteLine(
                    "There is no vehicle with license number: {0} in the garage, returning to main menu..",
                    licenseNumber);
            }
        }

        private void refuelVehicle()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if (r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                GasEngine vehicleEngine = r_Garage.VehicleDictionary[licenseNumber].Vehicle.Engine as GasEngine;

                if (vehicleEngine != null)
                {
                    receiveFuelInputAndRefuelVehicle(vehicleEngine);
                }
                else
                {
                    Console.Write("This vehicle is not gas-powered, returning to main menu..");
                }
            }
            else
            {
                Console.WriteLine(
                    "There is no vehicle with license number: {0} in the garage, returning to main menu..",
                    licenseNumber);
            }
        }

        private void receiveFuelInputAndRefuelVehicle(GasEngine i_Engine)
        {
            Console.Write(
@"Gas Type
1   Soler
2   Octan95
3   Octan96
4   Octan98
Please select gas type: ");
            bool isValidGasType = false;

            do
            {
                try
                {
                    int gasType = receiveEnumInput<GasEngine.eGasType>();
                    i_Engine.CheckGasType((GasEngine.eGasType)gasType);
                    isValidGasType = true;
                }
                catch (ArgumentException argumentException)
                {
                    Console.Write("Incompatible gas type selected, this vehicle takes {0}, please try again: ", i_Engine.GasType);
                }
            }
            while (!isValidGasType);

            bool isValidGasAmount = false;

            Console.Write(
@"Your vehicle has {0}l out of {1}l
How much gas would you like to add? ",
                i_Engine.CurrentEnergy,
                i_Engine.MaxEnergyCapacity);
            do
            {
                try
                {
                    string amountOfGasInput = Console.ReadLine();
                    float amountOfGasToAdd = float.Parse(amountOfGasInput);
                    i_Engine.AddEnergy(amountOfGasToAdd);
                    isValidGasAmount = true;
                }
                catch(ArgumentException argumentException)
                {
                    Console.Write("You must enter a positive number, please try again: ");
                }
                catch(FormatException formatException)
                {
                    Console.Write("You must enter a number, please try again: ");
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.Write("That is too much gas, please try again: ");
                }
            }
            while(!isValidGasAmount);
        }

        private void chargeVehicle()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if(r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                ElectricEngine vehicleEngine = r_Garage.VehicleDictionary[licenseNumber].Vehicle.Engine as ElectricEngine;

                if (vehicleEngine != null)
                {
                    receiveEnergyInputAndChargeVehicle(vehicleEngine);
                }
                else
                {
                    Console.Write("This vehicle is not electric-powered, returning to main menu..");
                }
            }
            else
            {
                Console.WriteLine(
                    "There is no vehicle with license number: {0} in the garage, returning to main menu..",
                    licenseNumber);
            }
        }

        private void receiveEnergyInputAndChargeVehicle(ElectricEngine i_Engine)
        {
            bool isValidEnergyAmount = false;

            Console.Write(
@"Your vehicle has {0}h out of {1}h
For how many minutes would you like to charge the vehicle? ",
                i_Engine.CurrentEnergy,
                i_Engine.MaxEnergyCapacity);
            do
            {
                try
                {
                    string amountOfEnergyInput = Console.ReadLine();
                    float amountOfEnergyToAdd = float.Parse(amountOfEnergyInput);
                    i_Engine.AddEnergy(amountOfEnergyToAdd);
                    isValidEnergyAmount = true;
                }
                catch (ArgumentException argumentException)
                {
                    Console.Write("You must enter a positive number, please try again: ");
                }
                catch (FormatException formatException)
                {
                    Console.Write("You must enter a number, please try again: ");
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.Write("That is too much energy, please try again: ");
                }
            }
            while (!isValidEnergyAmount);
        }
    }
}
