using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    class CommentManager : ListManager<Comment>
    {

        public bool AddComment(Comment com, int caseId)
        {
            bool succes = false;
            SqlConnection cn = null;

            try
            {
                string sqlPath = "User ID=sa; Password=aik71111; " +
                    "server=localhost; Trusted_Connection=yes; " +
                    "database=AES; Connection timeout=30";


                using (cn = new SqlConnection(sqlPath))
                {
                    cn.Open();

                   // SqlDataAdapter adapter = new SqlDataAdapter("SELECT CASE_ID FROM dbo.CASES", cn);

                    string query = "INSERT INTO [dbo].[CASE_COMMENTS] (COMMENTS, WRITTENBY, TIMED, CASE_ID_FK) VALUES (@COMMENTS, @WRITTENBY, @TIMED, @CASE_ID_FK)";
                    using (SqlCommand commentsCmd = new SqlCommand(query, cn))
                    {
                        commentsCmd.Parameters.Add("@COMMENTS", SqlDbType.NVarChar).Value = com.Comments;
                        commentsCmd.Parameters.Add("@WRITTENBY", SqlDbType.NVarChar).Value = com.WrittenBy;
                        commentsCmd.Parameters.Add("@TIMED", SqlDbType.DateTime).Value = com.WrittenTime;
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


        public bool UpdateCaseComment(Comment com)
        {
            bool succes = false;
            SqlConnection cn = null;
            

            try
            {
                string sqlPath = "User ID=sa; Password=aik71111; " +
                    "server=localhost; Trusted_Connection=yes; " +
                    "database=AES; Connection timeout=30";


                using (cn = new SqlConnection(sqlPath))
                {
                    cn.Open();
                    String query = "INSERT INTO [dbo].[CASE_COMMENTS] (COMMENTS, WRITTENBY, TIMED, CASE_ID_FK) VALUES (@COMMENTS, @WRITTENBY, @TIMED, @CASE_ID_FK)";
                    using (SqlCommand commentsCmd = new SqlCommand(query, cn))
                    {

                        commentsCmd.Parameters.Add("@COMMENTS", SqlDbType.NVarChar).Value = com.Comments;
                        commentsCmd.Parameters.Add("@WRITTENBY", SqlDbType.NVarChar).Value = com.WrittenBy;
                        commentsCmd.Parameters.Add("@TIMED", SqlDbType.DateTime).Value = com.WrittenTime;
                        commentsCmd.Parameters.Add("@CASE_ID_FK", SqlDbType.Int).Value = com.CaseIDFK;


                        int inserted = commentsCmd.ExecuteNonQuery();
                        if (inserted > 0)
                        {
                            succes = true;
                        }
                        else
                        {
                            succes = false;
                        } //End inner if
                    }//End using SqlCommand
                }//End using SqlConnection
            }//End try
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cn != null)
                    cn.Close();
            }//End finally

            return succes;

        }//End method

        public List<Comment> loadComments(int caseID)
        {
            a_List = new List<Comment>();
            string sqlPath = "User ID=sa; Password=aik71111; " +
                "server=localhost; Trusted_Connection=yes; " +
                "database=AES; Connection timeout=30";



            using (var con = new SqlConnection(sqlPath))
            {
                string query = "SELECT * FROM CASE_COMMENTS " +
                    "WHERE CASE_ID_FK = '"+caseID+"'";
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                using (SqlDataReader objReader = cmd.ExecuteReader())
                {
                    if (objReader.HasRows)
                    {
                        while (objReader.Read())
                        {
                            var c = new Comment();
                            c.WrittenTime = Convert.ToDateTime(objReader["TIMED"]);
                            c.WrittenBy = objReader["WRITTENBY"].ToString();
                            c.Comments = objReader["COMMENTS"].ToString();


                            a_List.Add(c);

                        }//End while
                    }//End if
                }//End inner using     
            }//End outer using

            return a_List;

        }


    }//End class

}
