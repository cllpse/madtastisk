using System;


namespace Library
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Shorthand for Convert.ToDouble()
        /// </summary>
        public static Double ToDouble(this Object o)
        {
            return Convert.ToDouble(o);
        }


        /// <summary>
        /// Shorthand for Convert.ToInt32()
        /// </summary>
        public static Double ToInteger(this Object o)
        {
            return Convert.ToInt32(o);
        }
    }
}