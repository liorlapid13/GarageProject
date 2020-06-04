using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        private eGasType m_GasType;

        public enum eGasType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        public GasEngine(float i_MaxGasCapacity)
            : base(i_MaxGasCapacity)
        {
            
        }

        public enum eGasCapacity
        {
            Motorcycle = 7,
            Car = 60, 
            Truck = 120
        }

        public eGasType GasType
        {
            get
            {
                return m_GasType;
            }

            set
            {
                m_GasType = value;
            }
        }

        public override void AddEnergy(float i_AmountOfGasToAdd)
        {
            if(i_AmountOfGasToAdd <= 0)
            {
                throw new ArgumentException("Non-positive number entered");
            }

            fillGasTank(i_AmountOfGasToAdd);
        }

        private void fillGasTank(float i_AmountOfGasToAdd)
        {
            if(m_CurrentEnergy + i_AmountOfGasToAdd > r_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(r_MaxEnergyCapacity, 0);
            }

            m_CurrentEnergy += i_AmountOfGasToAdd;
        }

        public override string ToString()
        {
           
            string engineInformationOutput = string.Format(
@"Engine Information
Gas Type: {0}
Current Amount of Gas: {1} liters
Max Gas Capacity: {2} liters",
                m_GasType.ToString(),
                CurrentEnergy,
                MaxEnergyCapacity);

            return engineInformationOutput;
        }

        public void CheckGasType(eGasType i_GasType)
        {
            if(m_GasType != i_GasType)
            {
                throw new ArgumentException("Incompatible gas type");
            }
        }
    }
}
