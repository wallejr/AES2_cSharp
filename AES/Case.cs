using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    public class Case
    {
        private int caseID;
        private string requester;
        private Category cat;
        private string state;
        private string workDesc;
        private Personal pLeader;
        private Personal assigne;
        private DateTime created;
        private DateTime changed;
        private int countTime;
        private int totalTime;
        private string caseTitle;
        private string caseDesc;
        private string solution;
        private string comments;

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

        public string Requester
        {
            get { return requester; }
            set { requester = value; }
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
