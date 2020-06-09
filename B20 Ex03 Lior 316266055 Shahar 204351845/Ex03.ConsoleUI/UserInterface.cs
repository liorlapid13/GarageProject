using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UserInterface
    {
        private const int k_TerminateProgram = 8;
        private readonly Garage r_Garage;

        internal enum eGarageActions
        {
            InsertNewVehicle = 1,
            ShowAllLicensePlatesOfVehiclesInGarage,
            ChangeVehicleStatus,
            InflateWheelsToMaximum,
            RefuelVehicle,
            ChargeVehicle,
            DisplayVehicleInformation,
            ExitProgram
        }

        internal UserInterface()
        {
            r_Garage = new Garage();
        }

        internal void StartProgram()
        {
            mainMenu();
        }

        private void mainMenu()
        {
            bool terminateProgram = false;

            while(!terminateProgram)
            {
                presentUserMenu();

                int menuOption = Utilities.ReceiveEnumInput<eGarageActions>();

                if(menuOption == k_TerminateProgram)
                {
                    terminateProgram = true;
                }

                performGarageAction((eGarageActions)menuOption);
                if(!terminateProgram)
                {
                    pauseAndPressEnterToContinue();
                }
            }
        }

        private void performGarageAction(eGarageActions i_UserMenuSelection)
        {
            Console.Clear();
            switch(i_UserMenuSelection)
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
                case eGarageActions.InflateWheelsToMaximum:
                    inflateWheelsToMaximum();
                    break;
                case eGarageActions.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eGarageActions.ChargeVehicle:
                    chargeVehicle();
                    break;
                case eGarageActions.DisplayVehicleInformation:
                    searchAndPresentVehicleByLicenseNumber();
                    break;
                case eGarageActions.ExitProgram:
                    terminateProgramMessage();
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
                isValidLicenseNumber = Utilities.CheckLicenseNumberInput(licenseNumber);
            }
            while(!isValidLicenseNumber);

            return licenseNumber;
        }

        private void receiveVehicleOwnerInformation(out string o_OwnerName, out string o_PhoneNumber)
        {
            Console.WriteLine("Please enter vehicle owner's information");
            bool isValidName, isValidPhoneNumber;
            string ownerName, ownerPhoneNumber;

            Console.Write("Name: ");
            do
            {
                ownerName = Console.ReadLine();
                isValidName = Utilities.CheckNameInput(ownerName);
            }
            while(!isValidName);

            o_OwnerName = ownerName;

            Console.Write("Phone number: ");
            do
            {
                ownerPhoneNumber = Console.ReadLine();
                isValidPhoneNumber = Utilities.CheckPhoneNumberInput(ownerPhoneNumber);
            }
            while(!isValidPhoneNumber);

            o_PhoneNumber = ownerPhoneNumber;
        }

        private void presentUserMenu()
        {
            Console.Write(
@"Garage Main Menu
1   Insert a new vehicle
2   Show all vehicles
3   Change vehicle status
4   Inflate wheels to maximum
5   Refuel vehicle
6   Recharge vehicle
7   Display vehicle information
8   Exit
What would you like to do? ");
        }

        private void addNewVehicleToGarage()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if(r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                Console.WriteLine("This vehicle is already registered in the garage, welcome back!");
                r_Garage.ChangeVehicleStatus(licenseNumber, Garage.VehicleInformation.eVehicleStatus.InService);
            }
            else
            {
                Vehicle newVehicle = createNewVehicle(licenseNumber);
                insertNewVehicleToGarage(licenseNumber, newVehicle);
                Console.WriteLine("Vehicle successfully added to garage");
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
            int selectedVehicleType = Utilities.ReceiveEnumInput<VehicleCreator.eSupportedVehicles>();
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
                    catch(FormatException formatException)
                    {
                        Console.Write("Invalid input, please try again: ");
                    }
                    catch(ValueOutOfRangeException valueOutOfRangeException)
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
                else if(!Utilities.IsInEnumRange<Garage.VehicleInformation.eVehicleStatus>(userSelectionInt))
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
            if(!i_VehicleStatusToPresent.HasValue)
            {
                Console.WriteLine("All vehicles in garage:");
                foreach(KeyValuePair<string, Garage.VehicleInformation> vehicleInformation in r_Garage.VehicleDictionary)
                {
                    Console.WriteLine(vehicleInformation.Key);
                }
            }
            else
            {
                Console.WriteLine("All vehicles with status: '{0}' in garage", i_VehicleStatusToPresent.Value);
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
                int userSelection = Utilities.ReceiveEnumInput<Garage.VehicleInformation.eVehicleStatus>();

                r_Garage.ChangeVehicleStatus(licenseNumber, (Garage.VehicleInformation.eVehicleStatus)userSelection);
                Console.WriteLine("Vehicle status is now {0}", (Garage.VehicleInformation.eVehicleStatus)userSelection);
            }
            else
            {
                Console.WriteLine("There is no vehicle with license number: {0} in the garage", licenseNumber);
            }
        }

        private void inflateWheelsToMaximum()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if(r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                try
                {
                    r_Garage.VehicleDictionary[licenseNumber].Vehicle.InflateAllWheelsToMax();
                    Console.WriteLine("Wheels inflated to maximum");
                }
                catch(ArgumentException argumentException)
                {
                    Console.WriteLine("Wheels are already full of air");
                }
            }
            else
            {
                Console.WriteLine("There is no vehicle with license number: {0} in the garage", licenseNumber);
            }
        }

        private void refuelVehicle()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if(r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                GasEngine vehicleEngine = r_Garage.VehicleDictionary[licenseNumber].Vehicle.Engine as GasEngine;

                if(vehicleEngine != null)
                {
                    if(!vehicleEngine.IsEngineFull())
                    {
                        receiveFuelInputAndRefuelVehicle(vehicleEngine);
                        Console.WriteLine("Vehicle refueled successfully");
                    }
                    else
                    {
                        Console.WriteLine("The vehicle is already full of gas");
                    }
                }
                else
                {
                    Console.WriteLine("This vehicle is not gas-powered");
                }
            }
            else
            {
                Console.WriteLine("There is no vehicle with license number: {0} in the garage", licenseNumber);
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
                    int gasType = Utilities.ReceiveEnumInput<GasEngine.eGasType>();
                    i_Engine.CheckGasType((GasEngine.eGasType)gasType);
                    isValidGasType = true;
                }
                catch(ArgumentException argumentException)
                {
                    Console.Write("Incompatible gas type selected, this vehicle takes {0}, please try again: ", i_Engine.GasType);
                }
            }
            while(!isValidGasType);

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
                catch(ValueOutOfRangeException valueOutOfRangeException)
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

                if(vehicleEngine != null)
                {
                    if(!vehicleEngine.IsEngineFull())
                    {
                        receiveEnergyInputAndChargeVehicle(vehicleEngine);
                        Console.WriteLine("Vehicle charged successfully");
                    }
                    else
                    {
                        Console.WriteLine("The vehicle is already fully charged");
                    }
                }
                else
                {
                    Console.WriteLine("This vehicle is not electric-powered");
                }
            }
            else
            {
                Console.WriteLine("There is no vehicle with license number: {0} in the garage", licenseNumber);
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
                catch(ArgumentException argumentException)
                {
                    Console.Write("You must enter a positive number, please try again: ");
                }
                catch(FormatException formatException)
                {
                    Console.Write("You must enter a number, please try again: ");
                }
                catch(ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.Write("That is too much energy, please try again: ");
                }
            }
            while(!isValidEnergyAmount);
        }

        private void searchAndPresentVehicleByLicenseNumber()
        {
            string licenseNumber = receiveLicenseNumberInput();

            if(r_Garage.CheckIfVehicleExistsInGarage(licenseNumber))
            {
                Console.WriteLine(r_Garage.CreateVehicleDetailsString(licenseNumber));
            }
            else
            {
                Console.WriteLine("There is no vehicle with this license number in the garage");
            }
        }

        private void pauseAndPressEnterToContinue()
        {
            Console.Write("Press enter to return to the Main Menu");
            Console.ReadLine();
            Console.Clear();
        }

        private void terminateProgramMessage()
        {
            Console.Write("Shutting down, good bye..");
        }
    }
}
