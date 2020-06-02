using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_LicenseNumber;
        protected string m_ModelName;
        protected float m_EnergyPercentageLeft;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;

        public enum eNumberOfWheels
        {
            Motorcycle = 2,
            Car = 4,
            Truck = 16
        }

        protected Vehicle(string i_LicenseNumber)
        {
            r_LicenseNumber = i_LicenseNumber;
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public float EnergyPercentageLeft
        {
            get
            {
                return m_EnergyPercentageLeft;
            }

            set
            {
                m_EnergyPercentageLeft = value;
            }
        }

        public void InitializeWheelsList(
            eNumberOfWheels i_NumberOfWheels,
            string i_Manufacturer,
            float i_CurrentAirPressure,
            Wheel.eMaxAirPressure i_MaxAirPressure)
        {
            m_Wheels = new List<Wheel>();

            for(int i = 0; i < (int)i_NumberOfWheels; i++)
            {
                m_Wheels.Add(new Wheel((float)i_MaxAirPressure, i_CurrentAirPressure, i_Manufacturer));
            }
        }

        public abstract void InitializeEngine(Engine.eEngineType i_EngineType, float i_CurrentEnergyAmount);

        public virtual List<string> GetUserDialogueStrings()
        {
            List<string> userDialogueStringList = new List<string>();
            
            userDialogueStringList.Add("Please enter your model name: ");
            userDialogueStringList.Add("What is your wheels' manufacturer? ");
            userDialogueStringList.Add("What is your wheels' air pressure? ");

            return userDialogueStringList;
        }

        public bool CheckModelName(string i_ModelName)
        {
            if(string.IsNullOrEmpty(i_ModelName))
            {
                throw new ArgumentException("You entered a empty string,please try again");
            }

            return true;
        }

        public bool CheckWheelManufacturer(string i_WheelManufacture)
        {
            if (string.IsNullOrEmpty(i_WheelManufacture))
            {
                throw new ArgumentException("You entered a empty string,please try again");
            }

            return true;
        }

        public bool CheckEnumSelect<T>(string i_EngineSelect)
        {
            int selection = int.Parse(i_EngineSelect);

            if (!Enum.IsDefined(typeof(T), selection))
            {
                throw new ValueOutOfRangeException(Enum.GetValues(typeof(T)).Length, 1);
            }

            return true;
        }

        public abstract void UpdateProperties(List<string> userDialogueInputsList);
        public abstract bool CheckCurrentEnergyAmount(string i_EnergyAmount);
        public abstract bool CheckCurrentWheelAirPressure(string i_WheelAirPressure);
        
        public abstract bool CheckLatestUserInput(string i_StringToCheck, int i_IndexInList);

    }
}
