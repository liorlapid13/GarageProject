using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eNumberOfDoors m_NumberOfDoors;
        private eColor m_CarColor;

        public enum eColor
        {
            Red,
            White,
            Black,
            Silver
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public Car(string i_LicenseNumber)
            : base(i_LicenseNumber)
        {

        }

        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                m_NumberOfDoors = value;
            }
        }
        public eColor CarColor
        {
            get
            {
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

        public override void InitializeEngine(Engine.eEngineType i_EngineType, float i_CurrentEnergyAmount)
        {
            if(i_EngineType == Engine.eEngineType.Gas)
            {
                m_Engine = new GasEngine(GasEngine.eGasType.Octan96, i_CurrentEnergyAmount, GasEngine.eGasCapacity.Car);
            }
            else
            {
                m_Engine = new ElectricEngine(i_CurrentEnergyAmount, ElectricEngine.eElectricEngineCapacityInMinutes.Car);
            }
        }

        public override List<string> GetUserDialogueStrings()
        {
            List<string> userDialogueStringList = base.GetUserDialogueStrings();

            userDialogueStringList.Add(
@"Engine Type
1   Gas Engine
2   Electric Engine
Please enter the type of your engine: ");
            userDialogueStringList.Add("How much gas/energy is in your car? ");
            userDialogueStringList.Add(
@"Vehicle Color
1   Red
2   White
3   Black
4   Silver
What color is your car? ");
            userDialogueStringList.Add(
@"Vehicle Doors
2   Two
3   Three
4   Four
5   Five
How many doors does your car have? ");

            return userDialogueStringList;
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
                string maxAirPressureString = Enum.GetName(typeof(VehicleCreator.eSupportedVehicles), VehicleCreator.eSupportedVehicles.Car);
                Wheel.eMaxAirPressure maxAirPressure = (Wheel.eMaxAirPressure)Enum.Parse(typeof(Wheel.eMaxAirPressure),maxAirPressureString);

                if (airPressure > (float)maxAirPressure)
                {
                    throw new ValueOutOfRangeException((float)Wheel.eMaxAirPressure.Car, 0);
                }
            }

            return true;
        }

        public override bool CheckCurrentEnergyAmount(string i_EnergyAmount)
        {
            float energyAmount;
            float maxEnergyCapacity;
            int engineType = 1;

            if (engineType == 1)
            {
                maxEnergyCapacity = (float)GasEngine.eGasCapacity.Car;
            }
            else
            {
                maxEnergyCapacity = (float)ElectricEngine.eElectricEngineCapacityInMinutes.Car;
            }
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

            switch(i_IndexInList)
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
                    isValidInput = CheckEnumSelect<Engine.eEngineType>(i_StringToCheck);
                    break;
                case 4:
                    isValidInput = CheckCurrentEnergyAmount(i_StringToCheck);
                    break;
                case 5:
                    isValidInput = CheckEnumSelect<Car.eColor>(i_StringToCheck);
                    break;
                case 6:
                    isValidInput = CheckEnumSelect<Car.eNumberOfDoors>(i_StringToCheck);
                    break;
            }


            return isValidInput;
        }

        public override void UpdateProperties(List<string> userDialogueInputsList)
        {

        }

    }

}
   