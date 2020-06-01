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

        public ElectricEngine(float i_CurrentBatteryTimeLeft, eElectricEngineCapacityInMinutes i_MaxBatteryCapacity)
            : base(((float)i_MaxBatteryCapacity) / 60, i_CurrentBatteryTimeLeft)
        {

        }

        protected override void addEnergy(float i_ChargeDuration)
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
    }
}
