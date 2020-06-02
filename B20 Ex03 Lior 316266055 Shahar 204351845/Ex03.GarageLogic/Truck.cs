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
        public override bool CheckCurrentWheelAirPressure(string i_CurrentWheelAirPressure)
        {
            float airPressure;
            bool isValidAirPressure = float.TryParse(i_CurrentWheelAirPressure, out airPressure);

            if (!isValidAirPressure)
            {
                throw new FormatException("Parse is failed");
            }
            else
            {
                string maxAirPressureString = Enum.GetName(typeof(VehicleCreator.eSupportedVehicles), VehicleCreator.eSupportedVehicles.Truck);
                Wheel.eMaxAirPressure maxAirPressure = (Wheel.eMaxAirPressure)Enum.Parse(typeof(Wheel.eMaxAirPressure), maxAirPressureString);

                if (airPressure > (float)maxAirPressure)
                {
                    throw new ValueOutOfRangeException((float)Wheel.eMaxAirPressure.Truck, 0);
                    isValidAirPressure = false;
                }
            }

            return true;
        }
        public override bool CheckCurrentEnergyAmount(string i_EnergyAmount)
        {
            float energyAmount;
            float maxEnergyCapacity = (float)GasEngine.eGasCapacity.Truck;
            bool isValidEnergyAmount = float.TryParse(i_EnergyAmount, out energyAmount);

            if (!isValidEnergyAmount)
            {
                throw new FormatException("Parse is failed");
            }
            else
            {
                if (energyAmount > maxEnergyCapacity)
                {
                    throw new ValueOutOfRangeException(maxEnergyCapacity, 0);

                }
            }

            return true;
        }

        public override bool CheckLatestUserInput(string i_StringToCheck, int i_IndexInList)
        {
            bool isValidInput = false;

            switch (i_IndexInList)
            {
                case 0:
                    isValidInput = CheckModelName(i_StringToCheck);
                    break;
                case 1:
                    isValidInput = CheckWheelManufacturer(i_StringToCheck);
                    break;
                case 2:
                    isValidInput = CheckCurrentWheelAirPressure(i_StringToCheck);
                    break;
                case 3:
                    isValidInput = CheckCurrentEnergyAmount(i_StringToCheck);
                    break;
            }

            return isValidInput;
        }
        public override void UpdateProperties(List<string> userDialogueInputsList)
        {

        }
    }
}
