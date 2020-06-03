using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const float k_MaxEngineCapacity = 9999F;
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public enum eLicenseType
        {
            A = 1,
            A1,
            AA,
            B
        }

        public enum eMotorcycleUserDialogueListIndex
        {
            EngineCapacity = 4,
            LicenseType
        }

        public Motorcycle(string i_LicenseNumber, Engine.eEngineType i_EngineType, float i_MaxEngineEnergyCapacity)
            : base(i_LicenseNumber, i_EngineType, i_MaxEngineEnergyCapacity)
        {
            
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }
        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
            set
            {
                m_EngineCapacity = value;
            }
        }

        public override List<string> GetUserDialogueStrings()
        {
            List<string> userDialogueStringList = base.GetUserDialogueStrings();

            userDialogueStringList.Add("Engine Capacity: ");
            userDialogueStringList.Add(
                @"License Type
1   A
2   A1
3   AA
4   B
What is your license type? ");

            return userDialogueStringList;
        }

        public override bool CheckLatestUserInput(string i_StringToCheck, int i_IndexInList)
        {
            bool isValidInput = false;

            if(i_IndexInList < 4)
            {
                isValidInput = base.CheckLatestUserInput(i_StringToCheck, i_IndexInList);
            }
            else
            {
                eMotorcycleUserDialogueListIndex dialogueListIndex = (eMotorcycleUserDialogueListIndex)i_IndexInList;

                switch (dialogueListIndex)
                {
                    case eMotorcycleUserDialogueListIndex.EngineCapacity:
                        isValidInput = checkEngineCapacityInput(i_StringToCheck);
                        break;
                    case eMotorcycleUserDialogueListIndex.LicenseType:
                        isValidInput = CheckEnumSelect<eLicenseType>(i_StringToCheck);
                        break;
                }
            }
            

            return isValidInput;
        }

        public override void UpdateProperties(List<string> i_UserDialogueInputsList)
        {
            base.UpdateProperties(i_UserDialogueInputsList);
            float currentWheelAirPressure = float.Parse(
                i_UserDialogueInputsList[(int)eVehicleUserDialogueListIndex.CurrentWheelAirPressure]);
            int licenseType = int.Parse(i_UserDialogueInputsList[(int)eMotorcycleUserDialogueListIndex.LicenseType]);

            InitializeWheelsList(
                eNumberOfWheels.Truck,
                i_UserDialogueInputsList[(int)eVehicleUserDialogueListIndex.WheelManufacturer],
                currentWheelAirPressure,
                Wheel.eMaxAirPressure.Truck);
            m_EngineCapacity =
                int.Parse(i_UserDialogueInputsList[(int)eMotorcycleUserDialogueListIndex.EngineCapacity]);
            m_LicenseType = (eLicenseType)licenseType;
        }

        private bool checkEngineCapacityInput(string i_EngineCapacity)
        {
            float engineCapacity;
            bool isValidEngineCapacity = float.TryParse(i_EngineCapacity, out engineCapacity);

            if(!isValidEngineCapacity)
            {
                throw new FormatException("Failed parse: string->float");
            }

            if(engineCapacity <= 0)
            {
                throw new ValueOutOfRangeException(k_MaxEngineCapacity, 1);
            }

            return true;
        }

        public override string ToString()
        {
            string motorcycleInformationOutput = string.Format(
                @"{0}
Number of Wheels: {1}
Truck Information
License Type: {2}
Engine Capacity: {3}cc",
                VehicleToString(),
                eNumberOfWheels.Motorcycle,
                m_LicenseType.ToString(),
                m_EngineCapacity);

            return motorcycleInformationOutput;
        }
    }
}