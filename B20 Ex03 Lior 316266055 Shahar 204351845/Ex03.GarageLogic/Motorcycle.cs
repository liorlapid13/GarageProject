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
    }
}