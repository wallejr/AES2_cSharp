/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    /// <summary>
    /// Case method that will handle the information about each case before saved to the database
    /// </summary>
    public class Case
    {

        private int caseID;
        private string cat;
        private string state;
        private string workDesc;
        private string requesterName;
        private string reqUserName;
        private string assigne;
        private string createdBy;
        private DateTime created;
        private DateTime changed;
        private int countTime;
        private int totalTime;
        private string caseTitle;
        private string caseDesc;
        private string solution;
        private string comments;
        private string phoneNr;
        private string computerName;
        private string dep;
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string RequesterName
        {
            get { return requesterName; }
            set { requesterName = value; }
        }

        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public string Dep
        {
            get { return dep; }
            set { dep = value; }
        }

        public string Assigne
        {
            get { return assigne; }
            set { assigne = value; }
        }

        public string ReqUserName
        {
            get { return reqUserName; }
            set { reqUserName = value; }
        }

        public string Cat
        {
            get { return cat; }
            set { cat = value; }
        }

        public string ComputerName
        {
            get { return computerName; }
            set { computerName = value; }
        }



        public string PhoneNr
        {
            get { return phoneNr; }
            set { phoneNr = value; }
        }

       

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }


        public int CaseID
        {
            get { return caseID; }
            set { caseID = value; }
        }

        public string WorkDesc
        {
            get { return workDesc; }
            set { workDesc = value; }
        }


        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }



        public DateTime Changed
        {
            get { return changed; }
            set { changed = value; }
        }

        public int CountTime
        {
            get { return countTime; }
            set { countTime = value; }
        }

        public int TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        public string CaseTitle
        {
            get { return caseTitle; }
            set { caseTitle = value; }
        }

        public string CaseDesc
        {
            get { return caseDesc; }
            set { caseDesc = value; }
        }


        public string Solution
        {
            get { return solution; }
            set { solution = value; }
        }



    }
}
