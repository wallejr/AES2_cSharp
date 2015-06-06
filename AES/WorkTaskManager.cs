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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    /// <summary>
    /// Class that will handle Worktasks. The class will load information from a database and store it an 
    /// list inherited from Listmanager
    /// </summary>
    class WorkTaskManager : ListManager<WorkTask>
    {
        /// <summary>
        /// Method that will get an id variable able reference to a Worktask object with information from the caller.
        /// The method will then add the task to the database and return to the caller
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="wt"></param>
        /// <returns></returns>
        public bool AddTask(int caseId, WorkTask wt)
        {
            bool succes = false;
            SqlConnection cn = null;

            try
            {
                string sqlPath = "User ID=sa; Password=***********; " +
                    "server=localhost; Trusted_Connection=yes; " +
                    "database=AES; Connection timeout=30";


                using (cn = new SqlConnection(sqlPath))
                {
                    cn.Open();

                    // SqlDataAdapter adapter = new SqlDataAdapter("SELECT CASE_ID FROM dbo.CASES", cn);

                    string query = "INSERT INTO [dbo].[WORKTASK] (WORKTASK, WRITTENBY, CASE_ID_FK) VALUES (@WORKTASK, @WRITTENBY, @CASE_ID_FK)";
                    using (SqlCommand commentsCmd = new SqlCommand(query, cn))
                    {
                       
                        commentsCmd.Parameters.Add("@WORKTASK", SqlDbType.NVarChar).Value = wt.TaskTodo;
                        commentsCmd.Parameters.Add("@WRITTENBY", SqlDbType.NVarChar).Value = wt.CreatedBy;
                        commentsCmd.Parameters.Add("@CASE_ID_FK", SqlDbType.Int).Value = caseId;


                        int inserted = commentsCmd.ExecuteNonQuery();
                        if (inserted > 0)
                        {
                            succes = true;
                        }
                        else
                        {
                            throw new Exception();
                        } //End inner if
                    } //End using SQLCommand
                }//End using SQLconnection
            }//End try
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }//End finally

            return succes;

        } //End method addCase

        /// <summary>
        /// Method to display the worktasks related to the case. The method recieves the id variable from the caller
        /// and search the database for corresponding worktasks and add those in a generic list inherited from class Listmanager
        /// </summary>
        /// <param name="caseID"></param>
        /// <returns></returns>
        public List<WorkTask> ShowTaskList(int caseID)
        {
            a_List = new List<WorkTask>();
            string sqlPath = "User ID=sa; Password=***********; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";



            using (var con = new SqlConnection(sqlPath))
            {
                string query = "SELECT * FROM WORKTASK " +
                    "WHERE CASE_ID_FK = '" + caseID + "'";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var wt = new WorkTask();
                            wt.TaskTodo = objReader["WORKTASK"].ToString();
                            wt.CreatedBy = objReader["WRITTENBY"].ToString();


                            a_List.Add(wt);

                        }//End while
                    }//End if
                }//End inner using     
            }//End outer using

            return a_List;

        } //End method ShowTasklList

        /// <summary>
        /// Method that deletes a task from the database, recieves a task string from the caller.
        /// I could have made the view to display task id and delete based on that. But decided that each
        /// case shouldn't have duplicated task strings exact the same.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool DelTask(string task)
        {
            bool succes = false;
            SqlConnection cn = null;

            try
            {
                string sqlPath = "User ID=sa; Password=***********; " +
                    "server=localhost; Trusted_Connection=yes; " +
                    "database=AES; Connection timeout=30";


                using (cn = new SqlConnection(sqlPath))
                {
                    cn.Open();

                    string query = "DELETE FROM [dbo].[WORKTASK] " + 
                        "WHERE WORKTASK = '"+task+"'";
                    using (SqlCommand commentsCmd = new SqlCommand(query, cn))
                    {


                        int inserted = commentsCmd.ExecuteNonQuery();
                        if (inserted > 0)
                        {
                            succes = true;
                        }
                        else
                        {
                            throw new Exception();
                        } //End inner if
                    } //End using SQLCommand
                }//End using SQLconnection
            }//End try
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cn != null)
                {
                    cn.Close();
                }
            }//End finally

            return succes;

        } //End method addCase

    }
}
