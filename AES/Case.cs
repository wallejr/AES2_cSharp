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
        private Personal requester;
        private Category cat;
        private Status state;
        private string workDesc;
        private Personal pLeader;
        private Personal assigne;
        private DateTime Created;
        private DateTime changed;
        private int countTime;
        private int totalTime;
        private string caseTitle;
        private string caseDesc;
        private string solution;
        private string comments;


        public int CaseID
        {
            get { return caseID; }
            set { caseID = value; }
        }

        public Personal Requester
        {
            get { return requester; }
            set { requester = value; }
        }

    }
}
