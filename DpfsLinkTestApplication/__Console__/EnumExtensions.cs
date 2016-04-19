using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpfsLinkTestApplication.__Console__ {
    public static class EnumExtensions {


        /// <summary>
        /// Provides for Integer return values.
        /// For example converting any value to int. 
        /// </summary>
        /// <param name="value">Int32</param>
        /// <returns></returns>
        public static int GetValue(this Enum value) {
            return Convert.ToInt32(value);
        }


        /// <summary>
        /// Provides for Integer return values.
        /// For example converting any value to string. 
        /// </summary>
        /// <param name="value">string</param>
        /// <returns></returns>
        public static string GetString(this Enum value) {
            return Convert.ToInt32(value).ToString();
        }


        /// <summary>
        /// Method used for Casting strings from Enum Extension Methods.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string CastToString<T>(object o) {
            return ((T)o).ToString();
        }


        public static void Main(string[] args) {
            foreach (var value in (ClientSize[])Enum.GetValues(typeof(ClientSize))) {
                int v = value.GetValue();
                System.Console.WriteLine("{0} = {1} (int)", value.ToString(), v);

                string s = value.GetString();
                System.Console.WriteLine("{0} = {1} (string)", value.ToString(), s);
            }

            System.Console.ReadLine();
        }


    }
}
