using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInformation> r_VehiclesInGarage;

        public Garage()
        {
            r_VehiclesInGarage = new Dictionary<string, VehicleInformation>();
        }

        public Dictionary<string, VehicleInformation> VehicleDictionary
        {
            get
            {
                return r_VehiclesInGarage;
            }
        }

        public void AddVehicleToGarage(string i_LicenseNumber, string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            VehicleInformation newVehicleInformation =
                new VehicleInformation(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle);

            r_VehiclesInGarage.Add(i_LicenseNumber, newVehicleInformation);
        }

        public bool CheckIfVehicleExistsInGarage(string i_LicenseNumber)
        {
            return r_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, VehicleInformation.eVehicleStatus i_NewVehicleStatus)
        {
            if(i_NewVehicleStatus != r_VehiclesInGarage[i_LicenseNumber].VehicleStatus)
            {
                r_VehiclesInGarage[i_LicenseNumber].VehicleStatus = i_NewVehicleStatus;
            }
        }

        public string CreateVehicleDetailsString(string i_LicenseNumber)
        {
            return r_VehiclesInGarage[i_LicenseNumber].ToString();
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

            public override string ToString()
            {
                string vehicleInformationOutput = string.Format(
@"Name: {0}
Phone Number: {1}
Vehicle Status: {2}
{3}",
                    r_OwnerName,
                    r_OwnerPhoneNumber,
                    m_VehicleStatus,
                    m_Vehicle.ToString());

                return vehicleInformationOutput;
            }
        }
    }
}
