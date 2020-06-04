using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public enum eElectricEngineCapacityInMinutes
        {
            Motorcycle = 72,
            Car = 126
        }

        public ElectricEngine(float i_MaxEnergyCapacityInMinutes)
            : base(i_MaxEnergyCapacityInMinutes / 60)
        {

        }

        public override void AddEnergy(float i_ChargeDuration)
        {
            chargeBattery(i_ChargeDuration);
        }

        private void chargeBattery(float i_ChargeDuration)
        {
            if(m_CurrentEnergy + i_ChargeDuration > r_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(r_MaxEnergyCapacity, 0);
            }

            m_CurrentEnergy += i_ChargeDuration;
        }

        public override string ToString()
        {
            string engineInformationOutput = string.Format(
@"Engine Information
Current Amount of Energy: {0} hours
Max Energy Capacity: {1} hours",
                CurrentEnergy,
                MaxEnergyCapacity);

            return engineInformationOutput;
        }
    }
}
