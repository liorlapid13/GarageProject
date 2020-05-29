using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex03_Lior_316266055_Shahar_204351845
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base(string.Format("Error! Entered value out of given range: Max:{0} Min:{1}", i_MaxValue, i_MinValue))
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
