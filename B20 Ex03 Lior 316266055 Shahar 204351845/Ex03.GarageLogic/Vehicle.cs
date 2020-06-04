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

        public enum eVehicleUserDialogueListIndex
        {
            ModelName,
            WheelManufacturer,
            CurrentWheelAirPressure,
            CurrentEnergyAmount,
        }

        protected Vehicle(string i_LicenseNumber, Engine.eEngineType i_EngineType, float i_MaxEngineEnergyCapacity)
        {
            r_LicenseNumber = i_LicenseNumber;

            switch (i_EngineType)
            {
                case Engine.eEngineType.Electric:
                    m_Engine = new ElectricEngine(i_MaxEngineEnergyCapacity);
                    break;
                case Engine.eEngineType.Gas:
                    //GasEngine.eGasType gasType = (GasEngine.eGasType)Enum.Parse(typeof(GasEngine.eGasType), GetType().Name);
                    m_Engine = new GasEngine(i_MaxEngineEnergyCapacity);
                    break;
            }
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

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public void InflateAllWheels()
        {
            foreach(Wheel wheel in m_Wheels)
            {
                wheel.InflateWheel();
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

        public virtual List<string> GetUserDialogueStrings()
        {
            List<string> userDialogueStringList = new List<string>
            {
                "Model Name: ",
                "Wheel Manufacturer: ",
                "Current Wheel Air Pressure: ",
                "Current Gas/Energy Amount (Liters/Hours): "
            };

            return userDialogueStringList;
        }

        private bool checkModelName(string i_ModelName)
        {
            if(string.IsNullOrEmpty(i_ModelName))
            {
                throw new ArgumentException("You entered a empty string,please try again");
            }

            return true;
        }

        private bool checkWheelManufacturer(string i_WheelManufacture)
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

            if(!Enum.IsDefined(typeof(T), selection))
            {
                throw new ValueOutOfRangeException(Enum.GetValues(typeof(T)).Length, 1);
            }

            return true;
        }

        public virtual void UpdateProperties(List<string> i_UserDialogueInputsList)
        {
            m_ModelName = i_UserDialogueInputsList[(int)eVehicleUserDialogueListIndex.ModelName];
            m_Engine.CurrentEnergy = float.Parse(
                i_UserDialogueInputsList[(int)eVehicleUserDialogueListIndex.CurrentEnergyAmount]);

            m_EnergyPercentageLeft = m_Engine.CurrentEnergy / m_Engine.MaxEnergyCapacity * 100;
        }

        private bool checkCurrentEnergyAmountInput(string i_CurrentEnergyAmountInput)
        {
            float currentEnergyAmount;
            bool isValidEnergyAmount = float.TryParse(i_CurrentEnergyAmountInput, out currentEnergyAmount);

            if(!isValidEnergyAmount)
            {
                throw new FormatException("Failed parse: string->float");
            }

            if(currentEnergyAmount > m_Engine.MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(m_Engine.MaxEnergyCapacity, 0);
            }

            return true;
        }

        private bool checkCurrentWheelAirPressure(string i_CurrentWheelAirPressure)
        {
            float currentAirPressure;
            bool isValidAirPressure = float.TryParse(i_CurrentWheelAirPressure, out currentAirPressure);

            if(!isValidAirPressure)
            {
                throw new FormatException("Failed parse: string->float");
            }

            string vehicleType = this.GetType().Name;
            Wheel.eMaxAirPressure maxAirPressure = (Wheel.eMaxAirPressure)Enum.Parse(typeof(Wheel.eMaxAirPressure), vehicleType);

            if(currentAirPressure > (float)maxAirPressure) 
            {
                throw new ValueOutOfRangeException((float)maxAirPressure, 0);
            }

            return true;
        }

        public virtual bool CheckLatestUserInput(string i_StringToCheck, int i_IndexInList)
        {
            bool isValidInput = false;
            eVehicleUserDialogueListIndex dialogueListIndex = (eVehicleUserDialogueListIndex)i_IndexInList;

            switch(dialogueListIndex)
            {
                case eVehicleUserDialogueListIndex.ModelName:
                    isValidInput = checkModelName(i_StringToCheck);
                    break;
                case eVehicleUserDialogueListIndex.WheelManufacturer:
                    isValidInput = checkWheelManufacturer(i_StringToCheck);
                    break;
                case eVehicleUserDialogueListIndex.CurrentWheelAirPressure:
                    isValidInput = checkCurrentWheelAirPressure(i_StringToCheck);
                    break;
                case eVehicleUserDialogueListIndex.CurrentEnergyAmount:
                    isValidInput = checkCurrentEnergyAmountInput(i_StringToCheck);
                    break;
            }

            return isValidInput;
        }

        protected string VehicleToString()
        {
            string vehicleInformationOutput = string.Format(
@"Vehicle Information
License Number: {0}
Model Name: {1}
Energy Percentage Left: {2}%
{3}
{4}",
                r_LicenseNumber,
                m_ModelName,
                m_EnergyPercentageLeft,
                m_Engine.ToString(),
                m_Wheels[0].ToString());

            return vehicleInformationOutput;
        }
    }
}
