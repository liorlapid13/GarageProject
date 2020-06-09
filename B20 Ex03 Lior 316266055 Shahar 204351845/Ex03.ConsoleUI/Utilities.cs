using System;

namespace Ex03.ConsoleUI
{
    internal static class Utilities
    {
        internal static bool CheckNameInput(string i_Name)
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

        internal static bool CheckPhoneNumberInput(string i_PhoneNumber)
        {
            bool isValidPhoneNumber = true;

            if(i_PhoneNumber.Length != 10) 
            {
                Console.Write("Phone number must be 10 numbers, please try again: ");
                isValidPhoneNumber = false;
            }
            else
            {
                foreach(char c in i_PhoneNumber)
                {
                    if(!char.IsDigit(c)) 
                    {
                        Console.Write("Phone number must include only numbers, please try again: ");
                        isValidPhoneNumber = false;
                        break;
                    }
                }
            }

            return isValidPhoneNumber;
        }

        internal static bool CheckLicenseNumberInput(string i_LicenseNumber)
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

        internal static bool IsInEnumRange<T>(int i_Selection)
        {
            return Enum.IsDefined(typeof(T), i_Selection);
        }

        internal static int ReceiveEnumInput<T>()
        {
            int selectedOption = -1;
            bool isValidInput = false;

            while(!isValidInput)
            {
                string userSelection = Console.ReadLine();

                if(int.TryParse(userSelection, out selectedOption))
                {
                    isValidInput = IsInEnumRange<T>(selectedOption);
                }

                if(!isValidInput)
                {
                    Console.Write("Invalid input, please try again: ");
                }
            }

            return selectedOption;
        }
    }
}
