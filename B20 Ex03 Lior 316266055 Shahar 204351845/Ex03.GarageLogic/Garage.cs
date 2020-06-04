using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInformation> m_VehiclesInGarage;

        public Garage()
        {
            m_VehiclesInGarage = new Dictionary<string, VehicleInformation>();
        }

        public Dictionary<string, VehicleInformation> VehicleDictionary
        {
            get
            {
                return m_VehiclesInGarage;
            }
        }

        public void AddVehicleToGarage(string i_LicenseNumber, string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            VehicleInformation newVehicleInformation =
                new VehicleInformation(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle);

            m_VehiclesInGarage.Add(i_LicenseNumber, newVehicleInformation);
        }

        public bool CheckIfVehicleExistsInGarage(string i_LicenseNumber)
        {
            return m_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, VehicleInformation.eVehicleStatus i_NewVehicleStatus)
        {
            if(i_NewVehicleStatus != m_VehiclesInGarage[i_LicenseNumber].VehicleStatus)
            {
                m_VehiclesInGarage[i_LicenseNumber].VehicleStatus = i_NewVehicleStatus;
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
