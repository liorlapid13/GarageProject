using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;


        public enum eLicenseType
        {
            A = 1,
            A1,
            AA,
            B
        }

        public Motorcycle(string i_LicenseNumber)
            : base(i_LicenseNumber)
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

        public override void InitializeEngine(Engine.eEngineType i_EngineType, float i_CurrentEnergyAmount)
        {
            if (i_EngineType == Engine.eEngineType.Gas)
            {
                m_Engine = new GasEngine(GasEngine.eGasType.Octan95, i_CurrentEnergyAmount, GasEngine.eGasCapacity.Motorcycle);
            }
            else
            {
                m_Engine = new ElectricEngine(i_CurrentEnergyAmount, ElectricEngine.eElectricEngineCapacityInMinutes.Motorcycle);
            }
        }
        public override bool CheckCurrentWheelAirPressure(string i_CurrentWheelAirPressure)
        {
            float airPressure;
            bool isValidAirPressure = float.TryParse(i_CurrentWheelAirPressure, out airPressure);

            if (!isValidAirPressure)
            {
                throw new FormatException("Parse is failed");
            }
            else
            {
                string maxAirPressureString = Enum.GetName(typeof(VehicleCreator.eSupportedVehicles), VehicleCreator.eSupportedVehicles.Motorcycle);
                Wheel.eMaxAirPressure maxAirPressure = (Wheel.eMaxAirPressure)Enum.Parse(typeof(Wheel.eMaxAirPressure), maxAirPressureString);

                if (airPressure > (float)maxAirPressure)
                {
                    throw new ValueOutOfRangeException((float)Wheel.eMaxAirPressure.Motorcycle, 0);
                    isValidAirPressure = false;
                }
            }

            return true;
        }
        public override bool CheckCurrentEnergyAmount(string i_EnergyAmount)
        {
            float energyAmount;
            float maxEnergyCapacity;
            int engineType = 1;

            if (engineType == 1)
            {
                maxEnergyCapacity = (float)GasEngine.eGasCapacity.Car;
            }
            else
            {
                maxEnergyCapacity = (float)ElectricEngine.eElectricEngineCapacityInMinutes.Car;
            }
            bool isValidEnergyAmount = float.TryParse(i_EnergyAmount, out energyAmount);

            if (!isValidEnergyAmount)
            {
                throw new FormatException("Parse is failed");
            }
            else
            {
                if (energyAmount > maxEnergyCapacity)
                {
                    throw new ValueOutOfRangeException(maxEnergyCapacity, 0);

                }
            }

            return true;
        }

        public override bool CheckLatestUserInput(string i_StringToCheck, int i_IndexInList)
        {
            bool isValidInput = false;

            switch (i_IndexInList)
            {
                case 0:
                    isValidInput = CheckModelName(i_StringToCheck);
                    break;
                case 1:
                    isValidInput = CheckWheelManufacturer(i_StringToCheck);
                    break;
                case 2:
                    isValidInput = CheckCurrentWheelAirPressure(i_StringToCheck);
                    break;
                case 3:
                    isValidInput = CheckEnumSelect<Engine.eEngineType>(i_StringToCheck);
                    break;
                case 4:
                    isValidInput = CheckCurrentEnergyAmount(i_StringToCheck);
                    break;
            }

            return isValidInput;
        }
        public override void UpdateProperties(List<string> userDialogueInputsList)
        {

        }
    }
}