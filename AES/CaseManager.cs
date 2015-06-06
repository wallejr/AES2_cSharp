/******************************************
 *  Projektuppgift C# II Malmö Högskola   *
 *  Ärendehanteringssystem                *
 *  Skapad av Patrik Wahlqvist            *
 *  Slutfört 2015-06-06                   *
 ******************************************
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AES
{
    /// <summary>
    /// Class that will handle the case communiation between list and database
    /// The class is a sublass of the Listmanager superclass
    /// </summary>
    public class CaseManager : ListManager<Case>
    {
        public int GetPKID { get; set; }

        /// <summary>
        /// Method that will add a case to the database, recieves a reference to a Case object as input from 
        /// the caller.
        /// The method will check if there is a solution written and then store it in the solution table f the database
        /// </summary>
        /// <param name="caset"></param>
        /// <returns></returns>
        public bool addCase(Case caset)
        {
            bool succes = false;
            SqlConnection cn = null;

            try
            {
                string sqlPath = "User ID=sa; Password=***********; " +
                    "server=localhost; Trusted_Connection=yes; " +
                    "database=AES; Connection timeout=30";


                using (var con = new SqlConnection(sqlPath))
                {
                    string query = "INSERT INTO [dbo].[CASES] (TITEL, DESCRIPTION, STATE, CREATEDBY, REQUESTERFULLNAME, REQUESTERUSERNAME, " +
                        "PHONENR, COMPUTERNAME, SKAPATDEN, ANDRATDEN, DEPARTMENT, ASSIGNE, KATEGORI, CITY) " +
                        "VALUES (@TITEL, @DESCRIPTION, @STATE, @CREATEDBY, @REQUESTERFULLNAME, @REQUESTERUSERNAME, " +
                        "@PHONENR, @COMPUTERNAME, @SKAPATDEN, @ANDRATDEN, @DEPARTMENT, @ASSIGNE, @KATEGORI, @CITY); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS int)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        cmd.Parameters.Add("@TITEL", SqlDbType.NVarChar).Value = caset.CaseTitle;
                        cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar).Value = caset.CaseDesc;
                        cmd.Parameters.Add("@STATE", SqlDbType.NVarChar).Value = caset.State;
                        cmd.Parameters.Add("@CREATEDBY", SqlDbType.NVarChar).Value = caset.CreatedBy;
                        cmd.Parameters.Add("@REQUESTERFULLNAME", SqlDbType.NVarChar).Value = caset.RequesterName;
                        cmd.Parameters.Add("@REQUESTERUSERNAME", SqlDbType.NVarChar).Value = caset.ReqUserName;
                        cmd.Parameters.Add("@PHONENR", SqlDbType.NVarChar).Value = caset.PhoneNr;
                        cmd.Parameters.Add("@COMPUTERNAME", SqlDbType.NVarChar).Value = caset.ComputerName;
                        cmd.Parameters.Add("@SKAPATDEN", SqlDbType.DateTime).Value = caset.Created;
                        cmd.Parameters.Add("@ANDRATDEN", SqlDbType.DateTime).Value = caset.Changed;
                        cmd.Parameters.Add("@DEPARTMENT", SqlDbType.NVarChar).Value = caset.Dep;
                        cmd.Parameters.Add("@ASSIGNE", SqlDbType.NVarChar).Value = caset.Assigne;
                        cmd.Parameters.Add("@KATEGORI", SqlDbType.NVarChar).Value = caset.Cat;
                        cmd.Parameters.Add("@CITY", SqlDbType.NVarChar).Value = caset.City;


                        int i = cmd.ExecuteNonQuery();

                        GetPKID = (int)cmd.ExecuteScalar();

                        if (i > 0)
                        {


                            if (!string.IsNullOrEmpty(caset.Solution))
                            {
                                query = "INSERT INTO [dbo].[SOLTUIONS] (SOLUTION, TIMED, CASE_ID_FK) VALUES (@SOLUTION, @TIMED, @CASE_ID_FK)";
                                using (SqlCommand solutionCmd = new SqlCommand(query, con))
                                {
                                    solutionCmd.Parameters.Add("@SOLUTION", SqlDbType.NVarChar).Value = caset.Solution;
                                    solutionCmd.Parameters.Add("@TIMED", SqlDbType.DateTime).Value = caset.Created;
                                    solutionCmd.Parameters.Add("@CASE_ID_FK", SqlDbType.Int).Value = GetPKID;


                                    int inserted = solutionCmd.ExecuteNonQuery();
                                    if (inserted > 0)
                                    {
                                        succes = true;
                                    }
                                    else
                                    {
                                        succes = false;
                                    } //End inner if
                                }//End using solution insert statement
                            }//End inner if
                           

                            succes = true;
                        }//End outer if

                    }//End outer using case statement

                }//End Outer ouer using connection statement
            }
            catch (SqlException sqlEx)
            {
                throw new CustomSqlException("There was an error connecting to the database\n", sqlEx);
            }
            catch (InvalidCastException castExc)
            {
                throw new CustomSqlException("There was an error casting to database datatypes\n", castExc);
            }

            finally
            {
                if (cn != null)
                {
                    cn.Close();
                    
                }

            }

            return succes;
            

        }//End metod addCase

        /// <summary>
        /// Method that will load cases from the database that has status Open and save
        /// it in a list of the generict listmanager class
        /// </summary>
        /// <returns></returns>
        public List<Case> loadFromDatabase()
        {
            a_List = new List<Case>();
            string sqlPath = "User ID=sa; Password=***********; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";
            


            using(var con = new SqlConnection(sqlPath))
            {
                string query = "SELECT * FROM CASES WHERE STATE = '" + "Open" + "'";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                
                using(SqlDataReader objReader = cmd.ExecuteReader())
                {
                        if(objReader.HasRows)
                    {
                        while(objReader.Read())
                        {
                            var c = new Case();
                            c.CaseID = Convert.ToInt32(objReader["CASE_ID"]);
                            c.CaseTitle = objReader["TITEL"].ToString();
                            c.CaseDesc = objReader["DESCRIPTION"].ToString();
                            c.Created = Convert.ToDateTime(objReader["SKAPATDEN"]);
                            c.Changed = Convert.ToDateTime(objReader["ANDRATDEN"]);
                            c.State = objReader["STATE"].ToString();
                            c.Assigne = objReader["ASSIGNE"].ToString();
                            c.CreatedBy = objReader["CREATEDBY"].ToString();
                            

                            a_List.Add(c);

                        }//End while
                    }//End if
                }//End inner using     
            }//End outer using

            return a_List;

        }//End method loadFromdatabase

        /// <summary>
        /// Method that will be called to load and fill caseinformation from a list to a casewindow
        /// The method will also check if there is any solution present and fill the casewindow with such information if any
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        public Case LoadCase(int caseId)
        {
            Case c = new Case();

            string sqlPath = "User ID=sa; Password=***********; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";
            try
            {
                using (var con = new SqlConnection(sqlPath))
                {
                    string query = "SELECT * FROM CASES WHERE CASE_ID = '"+caseId+"'";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    using (SqlDataReader objReader = cmd.ExecuteReader())
                    {
                        if (objReader.HasRows)
                        {
                            while (objReader.Read())
                            {
                                c.CaseID = Convert.ToInt32(objReader["CASE_ID"]);
                                c.CaseTitle = objReader["TITEL"].ToString();
                                c.CaseDesc = objReader["DESCRIPTION"].ToString();
                                c.State = objReader["STATE"].ToString();
                                c.CreatedBy = objReader["CREATEDBY"].ToString();
                                c.RequesterName = objReader["REQUESTERFULLNAME"].ToString();
                                c.ReqUserName = objReader["REQUESTERUSERNAME"].ToString();
                                c.PhoneNr = objReader["PHONENR"].ToString();
                                c.ComputerName = objReader["COMPUTERNAME"].ToString();
                                c.Created = Convert.ToDateTime(objReader["SKAPATDEN"]);
                                c.Changed = Convert.ToDateTime(objReader["ANDRATDEN"]);
                                c.Assigne = objReader["ASSIGNE"].ToString();
                                c.Dep = objReader["DEPARTMENT"].ToString();
                                c.Cat = objReader["KATEGORI"].ToString();
                                c.City = objReader["CITY"].ToString();

                            }
                        }
                    }//End inner using

                    query = "SELECT SOLUTION FROM SOLUTIONS " +
                        "WHERE SOLUTIONS.CASE_ID_FK = '" + caseId + "'";
                    cmd = new SqlCommand(query, con);
                    using (SqlDataReader solutionReader = cmd.ExecuteReader())
                    {
                        if (solutionReader.HasRows)
                        {
                            while (solutionReader.Read())
                            {
                                c.Solution = solutionReader["SOLUTION"].ToString();
                            }
                        }
                    }

                }//End outer using

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return c;

        }

        /// <summary>
        /// Method that will be called from the casewindow if there is an existent case being saved.
        /// The method will verify if there is any solution written y the user and  if so, a solution will
        /// be inserted in the solutions table with the caseid as foreign key
        /// </summary>
        /// <param name="caset"></param>
        /// <returns></returns>
        public bool updateCase(Case caset)
        {
            bool succes = false;
            SqlConnection cn = null;
            

            try
            {
                string sqlPath = "User ID=sa; Password=***********; " +
                    "server=localhost; Trusted_Connection=yes; " +
                    "database=AES; Connection timeout=30";


                using (var con = new SqlConnection(sqlPath))
                {
                    string query = "UPDATE [dbo].[CASES] SET TITEL=@TITEL, DESCRIPTION=@DESCRIPTION, STATE=@STATE, " +
                        "REQUESTERFULLNAME=@REQUESTERFULLNAME, REQUESTERUSERNAME=@REQUESTERUSERNAME, " +
                        "PHONENR=@PHONENR, COMPUTERNAME=@COMPUTERNAME, ANDRATDEN=@ANDRATDEN, DEPARTMENT=@DEPARTMENT, " +
                        "ASSIGNE=@ASSIGNE, KATEGORI=@KATEGORI, CITY=@CITY " +
                        "WHERE CASE_ID = '" +caset.CaseID+ "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();

                        cmd.Parameters.Add("@TITEL", SqlDbType.NVarChar).Value = caset.CaseTitle;
                        cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar).Value = caset.CaseDesc;
                        cmd.Parameters.Add("@STATE", SqlDbType.NVarChar).Value = caset.State;
                        cmd.Parameters.Add("@REQUESTERFULLNAME", SqlDbType.NVarChar).Value = caset.RequesterName;
                        cmd.Parameters.Add("@REQUESTERUSERNAME", SqlDbType.NVarChar).Value = caset.ReqUserName;
                        cmd.Parameters.Add("@PHONENR", SqlDbType.NVarChar).Value = caset.PhoneNr;
                        cmd.Parameters.Add("@COMPUTERNAME", SqlDbType.NVarChar).Value = caset.ComputerName;
                        cmd.Parameters.Add("@ANDRATDEN", SqlDbType.DateTime).Value = caset.Changed;
                        cmd.Parameters.Add("@DEPARTMENT", SqlDbType.NVarChar).Value = caset.Dep;
                        cmd.Parameters.Add("@ASSIGNE", SqlDbType.NVarChar).Value = caset.Assigne;
                        cmd.Parameters.Add("@KATEGORI", SqlDbType.NVarChar).Value = caset.Cat;
                        cmd.Parameters.Add("@CITY", SqlDbType.NVarChar).Value = caset.City;


                        int i = cmd.ExecuteNonQuery();

                        if (i > 0)
                        {


                            if (!string.IsNullOrEmpty(caset.Solution))
                            {
                                query = "INSERT INTO [dbo].[SOLUTIONS] (SOLUTION, TIMED, CASE_ID_FK) VALUES (@SOLUTION, @TIMED, @CASE_ID_FK)";
                                using (SqlCommand solutionCmd = new SqlCommand(query, con))
                                {
                                    solutionCmd.Parameters.Add("@SOLUTION", SqlDbType.NVarChar).Value = caset.Solution;
                                    solutionCmd.Parameters.Add("@TIMED", SqlDbType.DateTime).Value = caset.Changed;
                                    solutionCmd.Parameters.Add("@CASE_ID_FK", SqlDbType.Int).Value = caset.CaseID;


                                   int inserted = solutionCmd.ExecuteNonQuery();
                                    if (inserted > 0)
                                    {
                                        succes = true;
                                    }
                                    else
                                    {
                                        succes = false;
                                    } //End inner if
                                }//End using solution insert statement
                            } //End if solutions is empty
                        }//End outer if
                        else
                        {
                            succes = false;
                        }

                        succes = true;
                      

                    }//End outer using case statement

                }//End Outer ouer using connection statement
            }
            catch (Exception sqlEx)
            {
                throw new Exception(sqlEx.Message);
            }

            finally
            {
                if (cn != null)
                {
                    cn.Close();

                }

            }

            return succes;


        }//End metod addCase

        /// <summary>
        /// Methods below will load cases the be listed depending on the searchcriteria recieved
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<Case> SearchResultStatus(string status)
        {
            a_List = new List<Case>();
            string sqlPath = "User ID=sa; Password=***********; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";



            using (var con = new SqlConnection(sqlPath))
            {
                string query = "SELECT * FROM CASES WHERE STATE = '"+status+"'";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var c = new Case();
                            c.CaseID = Convert.ToInt32(objReader["CASE_ID"]);
                            c.CaseTitle = objReader["TITEL"].ToString();
                            c.CaseDesc = objReader["DESCRIPTION"].ToString();
                            c.Created = Convert.ToDateTime(objReader["SKAPATDEN"]);
                            c.Changed = Convert.ToDateTime(objReader["ANDRATDEN"]);
                            c.State = objReader["STATE"].ToString();
                            c.Assigne = objReader["ASSIGNE"].ToString();
                            c.CreatedBy = objReader["CREATEDBY"].ToString();


                            a_List.Add(c);

                        }//End while
                    }//End if
                }//End inner using     
            }//End outer using

            return a_List;

        }//End method SerchResultStatus

        public List<Case> SearchResultCategory(string category)
        {
            a_List = new List<Case>();
            string sqlPath = "User ID=sa; Password=***********; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";



            using (var con = new SqlConnection(sqlPath))
            {
                string query = "SELECT * FROM CASES WHERE KATEGORI='"+category+"'";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var c = new Case();
                            c.CaseID = Convert.ToInt32(objReader["CASE_ID"]);
                            c.CaseTitle = objReader["TITEL"].ToString();
                            c.CaseDesc = objReader["DESCRIPTION"].ToString();
                            c.Created = Convert.ToDateTime(objReader["SKAPATDEN"]);
                            c.Changed = Convert.ToDateTime(objReader["ANDRATDEN"]);
                            c.State = objReader["STATE"].ToString();
                            c.Assigne = objReader["ASSIGNE"].ToString();
                            c.CreatedBy = objReader["CREATEDBY"].ToString();


                            a_List.Add(c);

                        }//End while
                    }//End if
                }//End inner using     
            }//End outer using

            return a_List;

        }//End method SearchResultCategory

        public List<Case> SearchResultCaseID(int caseID)
        {
            a_List = new List<Case>();
            string sqlPath = "User ID=sa; Password=***********; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";



            using (var con = new SqlConnection(sqlPath))
            {
                string query = "SELECT * FROM CASES WHERE CASE_ID = '"+caseID+"'";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var c = new Case();
                            c.CaseID = Convert.ToInt32(objReader["CASE_ID"]);
                            c.CaseTitle = objReader["TITEL"].ToString();
                            c.CaseDesc = objReader["DESCRIPTION"].ToString();
                            c.Created = Convert.ToDateTime(objReader["SKAPATDEN"]);
                            c.Changed = Convert.ToDateTime(objReader["ANDRATDEN"]);
                            c.State = objReader["STATE"].ToString();
                            c.Assigne = objReader["ASSIGNE"].ToString();
                            c.CreatedBy = objReader["CREATEDBY"].ToString();


                            a_List.Add(c);

                        }//End while
                    }//End if
                }//End inner using     
            }//End outer using

            return a_List;

        }//End method loadFromdatabase

        public List<Case> SearchResultAssigne(string assigne)
        {
            a_List = new List<Case>();
            string sqlPath = "User ID=sa; Password=***********; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";



            using (var con = new SqlConnection(sqlPath))
            {
                string query = "SELECT * FROM CASES WHERE ASSIGNE = '"+assigne+"'";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var c = new Case();
                            c.CaseID = Convert.ToInt32(objReader["CASE_ID"]);
                            c.CaseTitle = objReader["TITEL"].ToString();
                            c.CaseDesc = objReader["DESCRIPTION"].ToString();
                            c.Created = Convert.ToDateTime(objReader["SKAPATDEN"]);
                            c.Changed = Convert.ToDateTime(objReader["ANDRATDEN"]);
                            c.State = objReader["STATE"].ToString();
                            c.Assigne = objReader["ASSIGNE"].ToString();
                            c.CreatedBy = objReader["CREATEDBY"].ToString();


                            a_List.Add(c);

                        }//End while
                    }//End if
                }//End inner using     
            }//End outer using

            return a_List;

        }//End method SearchResultAssigne

       
    }
}
