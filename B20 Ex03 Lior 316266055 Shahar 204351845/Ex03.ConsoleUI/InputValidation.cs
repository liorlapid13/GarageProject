using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class InputValidation
    {
        public static bool CheckNameInput(string i_Name)
        {
            bool isValidName = true;

            if (i_Name.Length == 0)
            {
                Console.Write("You must enter a name, please try again: ");
                isValidName = false;
            }
            else
            {
                foreach (char c in i_Name)
                {
                    if (!Char.IsLetter(c) && c != ' ')
                    {
                        Console.Write("Name must only include letters or spaces, please try again: ");
                        isValidName = false;
                        break;
                    }
                }
            }

            return isValidName;
        }

        public static bool CheckPhoneNumberInput(string i_PhoneNumber)
        {
            bool isValidPhoneNumber = true;

            if (i_PhoneNumber.Length != 10)
            {
                Console.Write("Phone number must be 10-digits long, please try again: ");
                isValidPhoneNumber = false;
            }
            else
            {
                foreach (char c in i_PhoneNumber)
                {
                    if (!Char.IsDigit(c))
                    {
                        Console.Write("Phone number must include only digits, please try again: ");
                        isValidPhoneNumber = false;
                        break;
                    }
                }
            }

            return isValidPhoneNumber;
        }

        public static bool CheckLicenseNumberInput(string i_LicenseNumber)
        {
            bool isValidLicenseNumber = true;

            if(string.IsNullOrEmpty(i_LicenseNumber))
            {
                Console.Write("You must enter a license number, please try again: ");
                isValidLicenseNumber = false;
            }
            else
            {
                foreach(char c in i_LicenseNumber)
                {
                    if(!char.IsDigit(c) && !char.IsLetter(c) && c != '-')
                    {
                        Console.Write("License number must contain digits/letters/hyphens only, please try again: ");
                        isValidLicenseNumber = false;
                        break;
                    }
                }
            }

            return isValidLicenseNumber;
        }

        public static bool CheckAirPressureInput(
            string i_CurrentAirPressure,
            VehicleCreator.eSupportedVehicles i_VehicleType)
        {
            float airPressure;
            bool isValidAirPressure = float.TryParse(i_CurrentAirPressure, out airPressure);

            if(!isValidAirPressure)
            {
                Console.Write("You must enter a number, please try again: ");
            }
            else
            {
                string maxAirPressureString = Enum.GetName(typeof(VehicleCreator.eSupportedVehicles), i_VehicleType);
                float maxAirPressure = (float)Enum.Parse(typeof(Wheel.eMaxAirPressure), maxAirPressureString);

                if(airPressure > maxAirPressure)
                {
                    Console.Write("The maximum air pressure for your wheels is {0}, please try again: ", maxAirPressure);
                    isValidAirPressure = false;
                }
            }

            return isValidAirPressure;
        }

        public static bool CheckEnergyAmount(string i_EnergyAmount, float i_MaxEnergyCapacity, string i_EnergyType)
        {
            float energyAmount;
            bool isValidEnergyAmount = float.TryParse(i_EnergyAmount, out energyAmount);

            if (!isValidEnergyAmount)
            {
                Console.Write("You must enter a number, please try again: ");
            }
            else
            {
                if (energyAmount > i_MaxEnergyCapacity)
                {
                    Console.Write(
                        "The maximum amount of {0} for your vehicle is {1}, please try again: ",
                        i_EnergyType,
                        i_MaxEnergyCapacity);
                    isValidEnergyAmount = false;
                }
            }

            return isValidEnergyAmount;
        }
    }
    }
}
