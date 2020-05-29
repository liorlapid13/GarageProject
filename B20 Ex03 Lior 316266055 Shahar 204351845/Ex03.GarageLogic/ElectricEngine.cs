using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_CurrentBatteryTimeLeft, float i_MaxBatteryCapacity)
            : base(i_MaxBatteryCapacity, i_CurrentBatteryTimeLeft)
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
