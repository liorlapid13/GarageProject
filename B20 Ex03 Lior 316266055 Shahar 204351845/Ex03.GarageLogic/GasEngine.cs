using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        private readonly eGasType r_GasType;

        public enum eGasType
        {
            None,
            Soler,
            Octan95,
            Octan96,
            Octan98
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
                return r_GasType;
            }
        }

        public GasEngine(eGasType i_GasType, float i_MaxGasCapacity, float i_CurrentGasAmount) :base(i_MaxGasCapacity, i_CurrentGasAmount)
        {
            r_GasType = i_GasType;
        }

        protected override void addEnergy(float i_AmountOfGasToAdd)
        {
            fillGasTank(i_AmountOfGasToAdd);
        }

        private void fillGasTank(float i_AmountOfGasToAdd)
        {
            if(m_CurrentEnergy + i_AmountOfGasToAdd < r_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(r_MaxEnergyCapacity, 0);
            }

            m_CurrentEnergy += i_AmountOfGasToAdd;
        }
    }
}
