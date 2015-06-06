/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    /// <summary>
    /// The superklass Listmanagar will hold generic listmethods and implements the interface Listmanager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListManager<T> : IListManager<T>
    {


        /// <summary>
        /// Instance variable som innehåller collection m_list
        /// </summary>
        private List<T> a_list;
        private int count;

        /// <summary>
        /// Default konstruktor utan parametrar. Skapar och tillderar m_list en ny collection av List
        /// </summary>
        public ListManager()
        {
            a_list = new List<T>();
        }

        public int Count
        {
            get
            {
                return a_list.Count();
            }
            set
            {
                count = value;
            }
        }

        public List<T> a_List
        {
            get
            {
                return a_list;
            }
            set
            {
                a_list = value;
            }
        }


        /// <summary>
        /// Metod som hanterar listan av valfri typ genom att den är deklarerad med <T> så håller respektive lista sina egna objekt
        /// </summary>
        /// <param name="aType"></param>
        /// <returns></returns>
        public bool Add(T aType)
        {
            if (aType != null)
            {
                a_list.Add(aType);
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// Metod som tar bort item i listan på valt index
        /// </summary>
        /// <param name="anIndex"></param>
        /// <returns></returns>
        public bool DeleteAt(int anIndex)
        {
            try
            {
                if (CheckIndex(anIndex))
                {
                    a_list.RemoveAt(anIndex);
                    return true;
                }
                else
                    return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        /// <summary>
        /// Hämtar och ger information om vad som finns på valt index i listan
        /// </summary>
        /// <param name="anIndex"></param>
        /// <returns>Item i listan på utsatt index</returns>
        public T GetAt(int anIndex)
        {
            T item = a_list[anIndex];

            return item;

        }

        /// <summary>
        /// Metod för att verifera att valt objekt i listan ligger innanför listans index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>True om objekt är inom listans gränser annars false</returns>
        public bool CheckIndex(int index)
        {
            Count = a_list.Count;
            if (index <= Count && index >= 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metod för att ändra i listan
        /// </summary>
        /// <param name="aType">input om vad för objekt som ska ändras</param>
        /// <param name="anIndex">input som visar vilket index som ska ändras</param>
        /// <returns></returns>
        public bool ChangeAt(T aType, int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                a_list.RemoveAt(anIndex);
                a_list.Insert(anIndex, aType);
                return true;
            }
            else
                return false;

        }//Slut på metod ChangeAt

        /// <summary>
        /// Metod från interface som returnerar innehållet i en collection i form av en string array
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            string[] strInfoStrings = new string[a_list.Count];

            int i = 0;
            foreach (var T in a_list)
            {
                strInfoStrings[i++] = T.ToString();
            }
            return strInfoStrings;
        }//Slut på metod ToStringArray
    }
}
