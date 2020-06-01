using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private float m_LoadCapacity;
        private bool m_IsCarryingHazardousMaterials;

        public Truck(string i_LicenseNumber)
            : base(i_LicenseNumber)
        {
           
        }

        public float LoadCapacity
        {
            get
            {
                return m_LoadCapacity;
            }

            set
            {
                m_LoadCapacity = value;
            }
        }

        public bool IsCarryingHazardousMaterials
        {
            get
            {
                return m_IsCarryingHazardousMaterials;
            }

            set
            {
                m_IsCarryingHazardousMaterials = value;
            }
        }

        public override void InitializeEngine(Engine.eEngineType i_EngineType, float i_CurrentEnergyAmount)
        {
            m_Engine = new GasEngine(GasEngine.eGasType.Soler, i_CurrentEnergyAmount, GasEngine.eGasCapacity.Truck);
        }
    }
}
