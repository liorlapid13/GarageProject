using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_LicenseNumber;
        private readonly string r_ModelName;
        private float m_EnergyPercentageLeft;
        private List<Wheel> m_Wheels;
        private Engine m_Engine;

        public enum eNumberOfWheels
        {
            Motorcycle = 2,
            Car = 4,
            Truck = 16
        }

        protected Vehicle(string i_LicenseNumber, string i_ModelName, float i_EnergyPercentageLeft)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_ModelName = i_ModelName;
            m_EnergyPercentageLeft = i_EnergyPercentageLeft;
        }

        protected void InitializeWheelsList(
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

        protected void InitializeEngine(
            Engine.eEngineType i_EngineType,
            float i_MaxEnergyCapacity,
            float i_CurrentEnergyAmount,
            GasEngine.eGasType i_GasType)
        {
            if(i_EngineType == Engine.eEngineType.Gas)
            {
                m_Engine = new GasEngine(i_GasType, i_MaxEnergyCapacity, i_CurrentEnergyAmount);
            }
            else
            {
                m_Engine = new ElectricEngine(i_MaxEnergyCapacity, i_CurrentEnergyAmount);
            }
        }
    }
}
