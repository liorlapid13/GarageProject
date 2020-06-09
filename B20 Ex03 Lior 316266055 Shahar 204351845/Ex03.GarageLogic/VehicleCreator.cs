namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public enum eSupportedVehicles
        {
            ElectricMotorcycle = 1,
            GasMotorcycle,
            ElectricCar,
            GasCar,
            Truck
        }

        public static Vehicle CreateNewVehicle(string i_LicenseNumber, eSupportedVehicles i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch(i_VehicleType)
            {
                case eSupportedVehicles.GasCar:
                    newVehicle = new Car(
                        i_LicenseNumber, 
                        Engine.eEngineType.Gas, 
                        (float)GasEngine.eGasCapacity.Car);
                    break;
                case eSupportedVehicles.ElectricCar:
                    newVehicle = new Car(
                        i_LicenseNumber,
                        Engine.eEngineType.Electric,
                        (float)ElectricEngine.eElectricEngineCapacityInMinutes.Car);
                    break;
                case eSupportedVehicles.Truck:
                    newVehicle = new Truck(
                        i_LicenseNumber,
                        Engine.eEngineType.Gas,
                        (float)GasEngine.eGasCapacity.Truck);
                    break;
                case eSupportedVehicles.ElectricMotorcycle:
                    newVehicle = new Motorcycle(
                        i_LicenseNumber,
                        Engine.eEngineType.Electric,
                        (float)ElectricEngine.eElectricEngineCapacityInMinutes.Motorcycle);
                    break;
                case eSupportedVehicles.GasMotorcycle:
                    newVehicle = new Motorcycle(
                        i_LicenseNumber,
                        Engine.eEngineType.Gas,
                        (float)GasEngine.eGasCapacity.Motorcycle);
                    break;
            }

            return newVehicle;
        }
    }
}
