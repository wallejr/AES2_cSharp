/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    public interface IListManager<T>
    {
        /// <summary>
        /// Instance variabel
        /// Returner antalet items i collection m_list
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Metod för att lägga till ett objekt till collection m_list
        /// </summary>
        /// <param name ="aType"> A type<param>
        /// <returns>True om lyckat annars false</returns>
        bool Add(T aType);

        /// <summary>
        /// Metod för att ta bort ett objekt från collection m_list
        /// på angiven position
        /// </summary>
        /// <param name ="anInidex">Index på objektet som skall tas bort</param>
        /// <returns>True om det lyckas annars false
        bool DeleteAt(int anIndex);

        T GetAt(int anIndex);

        bool CheckIndex(int index);

        bool ChangeAt(T aType, int anIndex);

        string[] ToStringArray();
    }
}
