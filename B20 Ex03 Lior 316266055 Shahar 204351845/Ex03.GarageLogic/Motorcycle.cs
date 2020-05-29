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

        public Motorcycle(
            eLicenseType i_LicenseType,
            int i_EngineCapacity,
            string i_LicenseNumber,
            string i_ModelName,
            float i_EnergyPercentageLeft,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            Engine.eEngineType i_EngineType,
            float i_MaxEnergyCapacity,
            float i_CurrentEnergyAmount)
            : base(i_LicenseNumber, i_ModelName, i_EnergyPercentageLeft)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
            InitializeWheelsList(
                eNumberOfWheels.Motorcycle,
                i_WheelsManufacturer,
                i_CurrentAirPressure,
                Wheel.eMaxAirPressure.Motorcycle);
            InitializeEngine(i_EngineType, i_MaxEnergyCapacity, i_CurrentEnergyAmount, GasEngine.eGasType.Octan95);
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
    }
}