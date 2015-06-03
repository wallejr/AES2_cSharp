using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AES
{
    public class CaseManager : ListManager<Case>
    {
        public bool addCase(Case caset)
    {
        bool succes = false;
        SqlConnection cn = null;

        try
        {
            string SqlPath = @"Data Source=WIN-1UG6IV73N5H;Initial Catalog=AES;Persist Security Info=True;User ID=sa;Password=aik71111";
            using (cn = new SqlConnection(SqlPath))
            
            {
                try
                {

                    cn.Open();
                    var cmd = cn.CreateCommand();


                    cmd.CommandText = "INSERT INTO CASES (TITEL, DESCRIPTION, STATE, CREATEDBY, REQUESTERFULLNAME, " +
                            "PHONENR, COMPUTERNAME, TIDBEREKNAD, TIDSLUTFORT, SKAPATDEN, ANDRATDEN" +
                            "ASSIGNE, DEPARTMENT, KATEGORI) " +
                           "VALUES (@TITEL, @DESCRIPTION, @STATE, @CREATEDBY, @REQUESTERFULLNAME, " +
                            "@PHONENR, @COMPUTERNAME, @TIDBEREKNAD, @TIDSLUTFORT, @SKAPATDEN, @ANDRATDEN" +
                            "@ASSIGNE, @DEPARTMENT, @KATEGORI)";

                    cmd.Parameters.Add("@TITEL", SqlDbType.NVarChar).Value = caset.CaseTitle;
                    cmd.Parameters.Add("@DESCRIPTION", SqlDbType.Text).Value = caset.CaseDesc;
                    cmd.Parameters.Add("@STATE", SqlDbType.NVarChar).Value = caset.State;
                    cmd.Parameters.Add("@CREATEDBY", SqlDbType.NVarChar).Value = caset.CreatedBy;
                    cmd.Parameters.Add("@REQUESTERFULLNAME", SqlDbType.NVarChar).Value = caset.RequesterName;
                    cmd.Parameters.Add("@PHONENR", SqlDbType.NVarChar).Value = caset.PhoneNr;
                    cmd.Parameters.Add("@COMPUTERNAME", SqlDbType.NVarChar).Value = caset.ComputerName;
                    cmd.Parameters.Add("@TIDEBEREKNAD", SqlDbType.Int).Value = caset.CountTime;
                    cmd.Parameters.Add("@TIDSLUTFORT", SqlDbType.Int).Value = caset.TotalTime;
                    cmd.Parameters.Add("@SKAPATDEN", SqlDbType.Timestamp).Value = caset.Created;
                    cmd.Parameters.Add("@ANDRATDEN", SqlDbType.Timestamp).Value = caset.Changed;
                    cmd.Parameters.Add("@ASSIGNE", SqlDbType.NVarChar).Value = caset.Assigne;
                    cmd.Parameters.Add("@DEPARTMENT", SqlDbType.NVarChar).Value = caset.Dep;
                    cmd.Parameters.Add("@KATEGORI", SqlDbType.NVarChar).Value = caset.Cat;
                    
                    var result = cmd.ExecuteNonQuery();

                    //int i = caseStmt.executeUpdate();

                    //if (i > 0)
                    //{


                        if (!caset.Comments.ToString().Equals(""))
                        {
                            cmd = cn.CreateCommand();

                            cmd.CommandText = "INSERT INTO CASE_COMMENTS(COMMENTS, Timed, CASE_ID) VALUES (@COMMENTS, @TIMED, LAST_INSERT_ID())";
                            cmd.Parameters.Add("@COMMENTS", SqlDbType.Text).Value = caset.Comments;
                            cmd.Parameters.Add("@TIMED", SqlDbType.Timestamp).Value = caset.Changed;


                            cmd.ExecuteNonQuery();

                            succes = true;

                        }
                        else
                        {
                            succes = false;
                        }

                        //succes = true;
                    //}
                }
                catch (SqlException sqlEx)
                {
                    throw new CustomSqlException("There was an error connecting to the database\n", sqlEx);
                }
                catch (InvalidCastException castExc)
                {
                    throw new CustomSqlException("There was an error casting to database datatypes\n", castExc);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception("There was an error creating the case:\n" + e.Message);
            
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

        public List<Case> loadFromDatabase()
        {
            string sqlPath = @"Data Source=WIN-1UG6IV73N5H;Initial Catalog=AES;Persist Security Info=True;User ID=sa;Password=aik71111";
            


            using(var con = new SqlConnection(sqlPath))
            { string query = "SELECT * FROM CASES";
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
                                c.Assigne = (Personal)objReader["ASSIGNE"];

                                a_List.Add(c);

                            }
                    }
                }     
            }

            return a_List;

          
            

        }
    
    //public bool updateCase(Case caset)
    //{
    //    bool succes = false;
    //    String tempstring;
    //    Connection cn = null;
    //    String DBURL = "jdbc:mysql://localhost:3306/AES?" +
    //    "user=root&password=aik71111";
    //    Timestamp sqlStartDate = new Timestamp(caset.getSkapad().getTime());
    //    Timestamp sqlAndradDate = new Timestamp(caset.getAndrad().getTime());

        
    //    try
    //    {
    //        Class.forName("com.mysql.jdbc.Driver");
    //        cn = DriverManager.getConnection(DBURL);
            
    //        if (cn == null)
    //        {
    //            throw new SQLException("Uppkoppling mot databas saknas");
    //        }
            
    //        PreparedStatement caseStmt = cn.prepareStatement("update CASES set TITEL = ?, "+
    //                "DESCRIPTION = ?,ANDRATDEN = ?, STATUS = ?, PHONENR = ?, COMPUTERNAME = ?, " +
    //                "TIDBEREKNAD = ?, ASSIGNE = ?, DEPARTMENT = ?, TIDSLUTFORT = ?, KATEGORI = ? " +
    //                "where CASES_ID= '"+caset.getId()+"'");
                        
            
    //        caseStmt.setString(1, caset.getTitel());
    //        caseStmt.setString(2, caset.getCaseDesc());
    //        caseStmt.setTimestamp(3, sqlAndradDate);
    //        caseStmt.setString(4, caset.getStatus());
    //        caseStmt.setString(5, caset.getPhoneNR());
    //        caseStmt.setString(6, caset.getCompName());
    //        caseStmt.setInt(7, caset.getBeraknadTid());
    //        caseStmt.setString(8, caset.getTilldeladTill());
    //        caseStmt.setString(9, caset.getAvdelning());
    //        caseStmt.setInt(10, caset.getTidsAtgang());
    //        caseStmt.setString(11, caset.getKategori());
          
            
            
    //        int i = caseStmt.executeUpdate();
    //        caseStmt.execute();
            
    //        if (i > 0 )
    //        {
    //            tempstring = caset.getComments();
    //            String empty = "";
                
    //            if (tempstring != null && !tempstring.equals(empty))
    //            {
                   
    //                PreparedStatement commentStmt = cn.prepareStatement("INSERT INTO CASE_COMMENTS(COMMENTS, Timed, CASE_ID)" + "VALUES (?, ?, ?)");
    //                commentStmt.setString(1, caset.getComments());
    //                commentStmt.setTimestamp(2, sqlAndradDate);
    //                commentStmt.setInt(3, caset.getId());


    //                int x = commentStmt.executeUpdate();
    //                if (x > 0)
    //                {
    //                    succes = true;
    //                } //End inner, inner if, verifying comments insert success

    //            }//End inner if, runningt comments insert statement
                
    //            if(caset.getSolution() != null && !caset.getSolution().isEmpty())
    //            {
    //                PreparedStatement solutionStmt = cn.prepareStatement("INSERT INTO SOLUTIONS(SOLUTION, Timed, CASE_ID)" + "VALUES(?, ?, ?)");
    //                solutionStmt.setString(1, caset.getSolution());
    //                solutionStmt.setTimestamp(2, sqlAndradDate);
    //                solutionStmt.setInt(3, caset.getId());
                    
                    
    //            }
                
                
    //            succes = true;
    //        }//End outer if verifyt case update
            
            
    //    } catch (ClassNotFoundException classE)
    //    {
    //        throw new SQLException("Problem med db:" + classE.getMessage());
    //    }
    //    catch(NullPointerException nullE)
    //    {
    //        throw new NullPointerException("Problem " + nullE.getMessage());
    //    }
                
        
    //    finally
    //    {
    //        if (cn != null)
    //            cn.close();
    //    }
        
    //    return  succes;
        
    //} //End method updateCase
    }
}
