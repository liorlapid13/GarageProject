using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private readonly eNumberOfDoors m_NumberOfDoors;
        private eColor m_CarColor;

        public enum eColor
        {
            Red,
            White,
            Black,
            Silver
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public Car(
            eColor i_CarColor,
            eNumberOfDoors i_NumberOfDoors,
            string i_LicenseNumber,
            string i_ModelName,
            float i_EnergyPercentageLeft,
            float i_CurrentAirPressure,
            string i_WheelManufacturer,
            Engine.eEngineType i_EngineType,
            float i_MaxEnergyCapacity,
            float i_CurrentEnergyAmount)
            : base(i_LicenseNumber, i_ModelName, i_EnergyPercentageLeft)
        {

            m_CarColor = i_CarColor;
            m_NumberOfDoors = i_NumberOfDoors;
            InitializeWheelsList(eNumberOfWheels.Car, i_WheelManufacturer, i_CurrentAirPressure, Wheel.eMaxAirPressure.Car);
            InitializeEngine(i_EngineType, i_MaxEnergyCapacity, i_CurrentEnergyAmount, GasEngine.eGasType.Octan96);
        }


        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
        }
        public eColor CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

    }
}
