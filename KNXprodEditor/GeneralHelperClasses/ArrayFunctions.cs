using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knxprod_ns
{
    public static class HandleArrayFunctions
    {
        /// <summary>
        /// einen neuen Eintrag in ein Array beliebigen Typs einfügen 
        /// </summary>
        public static T[] Add<T>(T[] array, T newValue)
        {
            int newLength = array.Length + 1;

            T[] result = new T[newLength];

            for (int i = 0; i < array.Length; i++)
                result[i] = array[i];

            result[newLength - 1] = newValue;

            return result;
        }

        /// <summary>
        /// einen Eintrag aus einem Array beliebigen Typs löschen
        /// </summary>
        public static T[] Delete<T>(T[] array, int index)
        {
            int newLength = array.Length - 1;

            T[] result = new T[newLength];

            if (newLength < 1)
            {
                return result;
            }

            for (int i = 0, j = 0; i < array.Length; i++, j++)
            {
                if (i == index)
                {
                    j--;
                }
                else 
                {
                    result[j] = array[i];
                }
            }
            return result;
        }
    }
}
