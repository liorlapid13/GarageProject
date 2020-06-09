using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eNumberOfDoors m_NumberOfDoors;
        private eColor m_CarColor;

        public enum eColor
        {
            Red = 1,
            White,
            Black,
            Silver
        }

        public enum eNumberOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public enum eCarUserDialogueListIndex
        {
            Color = 4,
            NumberOfDoors
        }

        public Car(string i_LicenseNumber, Engine.eEngineType i_EngineType, float i_MaxEngineEnergyCapacity)
            : base(i_LicenseNumber, i_EngineType, i_MaxEngineEnergyCapacity)
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

        public override List<string> GetUserDialogueStrings()
        {
            List<string> userDialogueStringList = base.GetUserDialogueStrings();

            userDialogueStringList.Add(
@"Vehicle Color
1   Red
2   White
3   Black
4   Silver
What color is your car? ");
            userDialogueStringList.Add(
@"Vehicle Doors
1   Two
2   Three
3   Four
4   Five
Select car number of doors: ");

            return userDialogueStringList;
        }

        public override bool CheckLatestUserInput(string i_StringToCheck, int i_IndexInList)
        {
            bool isValidInput = false;

            if(i_IndexInList < 4)
            {
                isValidInput = base.CheckLatestUserInput(i_StringToCheck, i_IndexInList);
            }
            else
            {
                eCarUserDialogueListIndex dialogueListIndex = (eCarUserDialogueListIndex)i_IndexInList;

                switch(dialogueListIndex)
                {
                    case eCarUserDialogueListIndex.Color:
                        isValidInput = CheckVehicleEnumSelection<eColor>(i_StringToCheck);
                        break;
                    case eCarUserDialogueListIndex.NumberOfDoors:
                        isValidInput = CheckVehicleEnumSelection<eNumberOfDoors>(i_StringToCheck);
                        break;
                }
            }

            return isValidInput;
        }

        public override void UpdateProperties(List<string> i_UserDialogueInputsList)
        {
            base.UpdateProperties(i_UserDialogueInputsList);
            float currentWheelAirPressure = float.Parse(
                i_UserDialogueInputsList[(int)eVehicleUserDialogueListIndex.CurrentWheelAirPressure]);
            int numberOfDoors = int.Parse(i_UserDialogueInputsList[(int)eCarUserDialogueListIndex.NumberOfDoors]);
            int carColor = int.Parse(i_UserDialogueInputsList[(int)eCarUserDialogueListIndex.Color]);
            GasEngine gasEngine = m_Engine as GasEngine;

            if(gasEngine != null)
            {
                gasEngine.GasType = GasEngine.eGasType.Octan96;
            }

            InitializeWheelsList(
                eNumberOfWheels.Car,
                i_UserDialogueInputsList[(int)eVehicleUserDialogueListIndex.WheelManufacturer],
                currentWheelAirPressure,
                Wheel.eMaxAirPressure.Car);
            m_NumberOfDoors = (eNumberOfDoors)numberOfDoors;
            m_CarColor = (eColor)carColor;
        }

        public override string ToString()
        {
            string carInformationOutput = string.Format(
@"{0}
Number of Wheels: {1}
Color: {2}
Number of Doors: {3}",
                VehicleToString(),
                ((int)eNumberOfWheels.Car).ToString(),
                m_CarColor.ToString(),
                m_NumberOfDoors);

            return carInformationOutput;
        }
    }
}