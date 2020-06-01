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
    }
}
