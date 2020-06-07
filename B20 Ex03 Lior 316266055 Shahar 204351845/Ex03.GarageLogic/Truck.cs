using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_MaxLoadCapacity = 9999F;
        private const string k_Yes = "1";
        private float m_LoadCapacity;
        private bool m_IsCarryingHazardousMaterials;

        public enum eTruckUserDialogueListIndex
        {
            HazardousGoods = 4,
            LoadCapacity
        }

        public Truck(string i_LicenseNumber, Engine.eEngineType i_EngineType, float i_MaxEngineEnergyCapacity)
            : base(i_LicenseNumber, i_EngineType, i_MaxEngineEnergyCapacity)
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

        public override List<string> GetUserDialogueStrings()
        {
            List<string> userDialogueStringList = base.GetUserDialogueStrings();

            userDialogueStringList.Add(
                @"Hazardous Goods
1   Yes
2   No
Is your truck carrying hazardous goods? ");
            userDialogueStringList.Add("Load Capacity: ");

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
                eTruckUserDialogueListIndex dialogueListIndex = (eTruckUserDialogueListIndex)i_IndexInList;

                switch(dialogueListIndex)
                {
                    case eTruckUserDialogueListIndex.HazardousGoods:
                        isValidInput = checkHazardousGoodsInput(i_StringToCheck);
                        break;
                    case eTruckUserDialogueListIndex.LoadCapacity:
                        isValidInput = checkLoadCapacity(i_StringToCheck);
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
            GasEngine gasEngine = m_Engine as GasEngine;

            gasEngine.GasType = GasEngine.eGasType.Soler;
            InitializeWheelsList(
                eNumberOfWheels.Truck,
                i_UserDialogueInputsList[(int)eVehicleUserDialogueListIndex.WheelManufacturer],
                currentWheelAirPressure,
                Wheel.eMaxAirPressure.Truck);
            m_IsCarryingHazardousMaterials =
                i_UserDialogueInputsList[(int)eTruckUserDialogueListIndex.HazardousGoods] == k_Yes; // User inputs "1" for yes, "2" for no
            m_LoadCapacity = float.Parse(i_UserDialogueInputsList[(int)eTruckUserDialogueListIndex.LoadCapacity]);
        }

        private bool checkLoadCapacity(string i_LoadCapacity)
        {
            float loadCapacity;
            bool isValidInput = float.TryParse(i_LoadCapacity, out loadCapacity);
            
            if(!isValidInput)
            {
                throw new FormatException("Failed parse: string->float");
            }

            if(loadCapacity > k_MaxLoadCapacity || loadCapacity < 0)
            {
                throw new ValueOutOfRangeException(k_MaxLoadCapacity, 0);
            }

            return true;
        }

        private bool checkHazardousGoodsInput(string i_isCarryingHazardousGoods)
        {
            int userInput;
            bool isValidInput = int.TryParse(i_isCarryingHazardousGoods, out userInput);

            if(!isValidInput)
            {
                throw new FormatException("Failed parse: string->int");
            }

            if(userInput != 1 && userInput != 2)
            {
                throw new ValueOutOfRangeException(2, 1);
            }

            return true;
        }

        public override string ToString()
        {
            string isCarryingHazardousGoods = m_IsCarryingHazardousMaterials ? "Yes" : "No";

            string truckInformationOutput = string.Format(
@"{0}
Number of Wheels: {1}
Carrying Hazardous Goods: {2}
Load Capacity: {3}",
                VehicleToString(),
                (int)eNumberOfWheels.Truck,
                isCarryingHazardousGoods,
                m_LoadCapacity);

            return truckInformationOutput;
        }
    }
}
