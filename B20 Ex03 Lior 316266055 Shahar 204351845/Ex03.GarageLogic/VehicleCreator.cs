using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public enum eSupportedVehicles
        {
            Motorcycle = 1,
            Car,
            Truck
        }
        /*
        public Vehicle CreateNewVehicle(
            eSupportedVehicles i_VehicleType,
            string i_LicenseNumber,
            string i_ModelName,
            float i_EnergyPercentageLeft,
            Car.eColor i_CarColor,
            Car.eNumberOfDoors i_NumberOfDoors,
            float i_CurrentAirPressure,
            string i_WheelManufacturer,
            Engine.eEngineType i_EngineType,
            float i_MaxEnergyCapacity,
            float i_CurrentEnergyAmount,
            Motorcycle.eLicenseType i_LicenseType,
            int i_EngineCapacity,
            float i_LoadCapacity,
            bool i_IsCarryingHazardousMaterials)
        {
            switch(i_VehicleType)
            {
                case eSupportedVehicles.Car:

                case eSupportedVehicles.Motorcycle:

                case eSupportedVehicles.Truck:

                default:
            }
        }
        */

        public static Car CreateCar(
            string i_LicenseNumber,
            string i_ModelName,
            float i_EnergyPercentageLeft,
            Car.eColor i_CarColor,
            Car.eNumberOfDoors i_NumberOfDoors,
            float i_CurrentAirPressure,
            string i_WheelManufacturer,
            Engine.eEngineType i_EngineType,
            float i_MaxEnergyCapacity,
            float i_CurrentEnergyAmount)
        {
            return new Car(
                i_LicenseNumber,
                i_ModelName,
                i_EnergyPercentageLeft,
                i_CarColor,
                i_NumberOfDoors,
                i_CurrentAirPressure,
                i_WheelManufacturer,
                i_EngineType,
                i_MaxEnergyCapacity,
                i_CurrentEnergyAmount);
        }

        public static Motorcycle CreateMotorcycle(
            Motorcycle.eLicenseType i_LicenseType,
            int i_EngineCapacity,
            string i_LicenseNumber,
            string i_ModelName,
            float i_EnergyPercentageLeft,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            Engine.eEngineType i_EngineType,
            float i_MaxEnergyCapacity,
            float i_CurrentEnergyAmount)
        {
            return new Motorcycle(
                i_LicenseType,
                i_EngineCapacity,
                i_LicenseNumber,
                i_ModelName,
                i_EnergyPercentageLeft,
                i_WheelsManufacturer,
                i_CurrentAirPressure,
                i_EngineType,
                i_MaxEnergyCapacity,
                i_CurrentEnergyAmount);
        }

        public static Truck CreateTruck(
            float i_LoadCapacity,
            bool i_IsCarryingHazardousMaterials,
            string i_LicenseNumber,
            string i_ModelName,
            float i_EnergyPercentageLeft,
            string i_WheelsManufacturer,
            float i_CurrentAirPressure,
            float i_CurrentGasAmount)
        {
            return new Truck(
                i_LoadCapacity,
                i_IsCarryingHazardousMaterials,
                i_LicenseNumber,
                i_ModelName,
                i_EnergyPercentageLeft,
                i_WheelsManufacturer,
                i_CurrentAirPressure,
                i_CurrentGasAmount);
        }
    }
}
