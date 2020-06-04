using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private float m_CurrentAirPressure;
        private string m_Manufacturer;

        public enum eMaxAirPressure
        {
            Truck = 28,
            Motorcycle = 30,
            Car = 32
        }

        
        public Wheel(float i_MaxAirPressure, float i_CurrentAirPressure, string i_Manufacturer)
        {
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_Manufacturer = i_Manufacturer;
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
        }

        public void InflateWheel()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public override string ToString()
        {
           
            string wheelInformationOutput = string.Format(
 @"Wheel Information
Manufacturer: {0}
Current Air Pressure: {1}
Max Air Pressure: {2}",
                m_Manufacturer,
                m_CurrentAirPressure,
                r_MaxAirPressure);

            return wheelInformationOutput;
        }
    }
}
