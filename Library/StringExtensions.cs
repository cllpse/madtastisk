using System;


namespace Library
{
    public static class StringExtensions
    {
        /// <summary>
        /// Shorthand for Convert.ToDouble()
        /// </summary>
        public static Double ToDouble(this String s)
        {
            return Convert.ToDouble(s);
        }


        /// <summary>
        /// Shorthand for Convert.ToInt32()
        /// </summary>
        public static Double ToInteger(this String s)
        {
            return Convert.ToInt32(s);
        }


        /// <summary>
        /// Repeats the string
        /// </summary>
        public static String Repeat(this String s, Int32 count)
        {
            var i = 0;
            var r = "";

            while (i < count)
            {
                r += s;

                i++;
            }

            return r;
        }
    }
}