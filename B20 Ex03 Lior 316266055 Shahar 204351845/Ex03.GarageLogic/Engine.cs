using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected readonly float r_MaxEnergyCapacity;
        protected float m_CurrentEnergy;
        
        public enum eEngineType
        {
            Gas = 1,
            Electric
        }

        protected Engine(float i_MaxEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return r_MaxEnergyCapacity;
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }

            set
            {
                m_CurrentEnergy = value;
            }
        }
        
        public abstract void AddEnergy(float i_AmountOfEnergyToAdd);

        public abstract override string ToString();
    }
}
