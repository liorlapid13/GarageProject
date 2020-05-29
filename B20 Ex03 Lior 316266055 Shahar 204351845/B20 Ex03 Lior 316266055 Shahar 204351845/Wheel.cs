using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex03_Lior_316266055_Shahar_204351845
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private float m_CurrentAirPressure;
        private string m_Manufacturer;

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
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
        }

        public void InflateWheel(float i_AmountOfAirToAdd)
        {
            if(m_CurrentAirPressure + i_AmountOfAirToAdd > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(r_MaxAirPressure, 0);
            }

            m_CurrentAirPressure += i_AmountOfAirToAdd;
        }
    }
}
