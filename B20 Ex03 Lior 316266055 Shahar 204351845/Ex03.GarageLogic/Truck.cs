using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private readonly float r_LoadCapacity;
        private bool m_IsCarryingHazardousMaterials;

        public Truck(
            float i_LoadCapacity,
            bool i_IsCarryingHazardousMaterials,
            string i_LicenseNumber,
            string i_ModelName,
            float i_EnergyPercentageLeft,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            float i_CurrentGasAmount)
            : base(i_LicenseNumber, i_ModelName, i_EnergyPercentageLeft)
        {
            r_LoadCapacity = i_LoadCapacity;
            m_IsCarryingHazardousMaterials = i_IsCarryingHazardousMaterials;
            InitializeWheelsList(eNumberOfWheels.Truck, i_WheelsManufacturer, i_CurrentAirPressure, Wheel.eMaxAirPressure.Truck);
            InitializeEngine(Engine.eEngineType.Gas, (float)GasEngine.eGasCapacity.Truck, i_CurrentGasAmount, GasEngine.eGasType.Soler);
        }

        public float LoadCapacity
        {
            get
            {
                return r_LoadCapacity;
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
    }
}
