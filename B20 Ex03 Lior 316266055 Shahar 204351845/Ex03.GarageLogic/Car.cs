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
    }
}
