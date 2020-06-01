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

        public static Vehicle CreateNewVehicle(string i_LicenseNumber, eSupportedVehicles i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch(i_VehicleType)
            {
                case eSupportedVehicles.Car:
                    newVehicle = new Car(i_LicenseNumber);
                    break;

                case eSupportedVehicles.Truck:
                    newVehicle = new Truck(i_LicenseNumber);
                    break;

                case eSupportedVehicles.Motorcycle:
                    newVehicle = new Motorcycle(i_LicenseNumber);
                    break;
            }

            return newVehicle;
        }
    }
}
