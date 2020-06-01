using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eNumberOfDoors m_NumberOfDoors;
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

        public Car(string i_LicenseNumber)
            : base(i_LicenseNumber)
        {

        }

        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                m_NumberOfDoors = value;
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

        public override void InitializeEngine(Engine.eEngineType i_EngineType, float i_CurrentEnergyAmount)
        {
            if(i_EngineType == Engine.eEngineType.Gas)
            {
                m_Engine = new GasEngine(GasEngine.eGasType.Octan96, i_CurrentEnergyAmount, GasEngine.eGasCapacity.Car);
            }
            else
            {
                m_Engine = new ElectricEngine(i_CurrentEnergyAmount, ElectricEngine.eElectricEngineCapacityInMinutes.Car);
            }
        }
    }
}
