using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInformation> m_VehicleDictionary;

        public Garage()
        {
            m_VehicleDictionary = new Dictionary<string, VehicleInformation>();
        }

        private void addVehicleToGarage(string i_LicenseNumber, string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            bool isVehicleInGarage = false;

            foreach(KeyValuePair<string,VehicleInformation> item in m_VehicleDictionary)
            {
                if(i_LicenseNumber == item.Key)
                {
                    item.Value.VehicleStatus = VehicleInformation.eVehicleStatus.InService;
                    isVehicleInGarage = true;
                }
            }

            if(!isVehicleInGarage)
            {
                VehicleInformation newVehicleInformation = new VehicleInformation(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle);
                m_VehicleDictionary.Add(i_LicenseNumber, newVehicleInformation);
            }
            else
            {
                throw new ArgumentException("Vehicle already in garage, status changed to 'InService'");
            }
        }

        public Dictionary<string, VehicleInformation> VehicleDictionary
        {
            get
            {
                return m_VehicleDictionary;
            }
        }

        public class VehicleInformation
        {
            private readonly string r_OwnerName;
            private readonly string r_OwnerPhoneNumber;
            private eVehicleStatus m_VehicleStatus;
            private Vehicle m_Vehicle;

            public enum eVehicleStatus
            {
                InService = 1,
                Repaired,
                Paid
            }

            public VehicleInformation(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
            {
                r_OwnerName = i_OwnerName;
                r_OwnerPhoneNumber = i_OwnerPhoneNumber;
                m_VehicleStatus = eVehicleStatus.InService;
                m_Vehicle = i_Vehicle;
            }

            public string OwnerName
            {
                get
                {
                    return r_OwnerName;
                }
            }

            public string OwnerPhoneNumber
            {
                get
                {
                    return r_OwnerPhoneNumber;
                }
            }

            public eVehicleStatus VehicleStatus
            {
                get
                {
                    return m_VehicleStatus;
                }

                set
                {
                    m_VehicleStatus = value;
                }
            }

            public Vehicle Vehicle
            {
                get
                {
                    return m_Vehicle;
                }
            }
        }
    }
}
