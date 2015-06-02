using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AES
{
    public class CaseManager
    {
        public bool addCase(Case caset)
    {
        bool succes = false;
        
        try
        {
            string SqlPath = @"Data Source=WIN-1UG6IV73N5H;Initial Catalog=AES;Persist Security Info=True;User ID=sa;Password=aik71111"
            using (SqlConnection cn = new SqlConnection(SqlPath))
            
            {

                if (cn == null)
                {
                    throw new SQLException("Uppkoppling mot databas saknas");
                }

                cn.Open();
                var cmd = cn.CreateCommand();


                cmd.CommandText = "INSERT INTO CASES (TITEL, DESCRIPTION, STATE, CREATEDBY, STATUS, REQUESTERFULLNAME, REQUESTERUSERNAME, " +
                        "PHONENR, COMPUTERNAME, TIDBEREKNAD, TIDSLUTFORT, SKAPATDEN, ANDRATDEN" +
                        "ASSIGNE, DEPARTMENT, KATEGORI) " +
                       "VALUES (@TITEL, @DESCRIPTION, @STATE, @CREATEDBY, @STATUS, @REQUESTERFULLNAME, @REQUESTERUSERNAME, " +
                        "@PHONENR, @COMPUTERNAME, @TIDBEREKNAD, @TIDSLUTFORT, @SKAPATDEN, @ANDRATDEN" +
                        "@ASSIGNE, @DEPARTMENT, @KATEGORI)";

                cmd.Parameters.Add("@TITEL", SqlDbType.NVarChar).Value = caset.CaseTitle;
                cmd.Parameters.Add("@DESCRIPTION", SqlDbType.Text).Value = caset.CaseDesc;
                cmd.Parameters.Add("@STATE", SqlDbType.NVarChar).Value = caset.State;
                


                caseStmt.setString(1, caset.getTitel());
                caseStmt.setString(2, caset.getCaseDesc());
                caseStmt.setTimestamp(3, sqlStartDate);
                caseStmt.setTimestamp(4, sqlAndradDate);
                caseStmt.setString(5, caset.getStatus());
                caseStmt.setString(6, caset.getSkapadAv());
                caseStmt.setString(7, caset.getBestallareFullNamn());
                caseStmt.setString(8, caset.getBestallareAnvNamn());
                caseStmt.setString(9, caset.getPhoneNR());
                caseStmt.setString(10, caset.getCompName());
                caseStmt.setInt(11, caset.getBeraknadTid());
                caseStmt.setString(12, caset.getTilldeladTill());
                caseStmt.setString(13, caset.getAvdelning());
                caseStmt.setString(14, caset.getKategori());



                int i = caseStmt.executeUpdate();

                if (i > 0)
                {


                    if (!caset.getComments().isEmpty())
                    {
                        PreparedStatement commentStmt;

                        commentStmt = cn.prepareStatement("INSERT INTO CASE_COMMENTS(COMMENTS, Timed, CASE_ID)" + "VALUES (?, ?, LAST_INSERT_ID())");
                        commentStmt.setString(1, caset.getComments());
                        commentStmt.setTimestamp(2, sqlAndradDate);


                        commentStmt.executeUpdate();

                        succes = true;

                    }
                    else
                    {
                        succes = false;
                    }

                    succes = true;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception("There was an error creating the case:\n" + e.getMessage());
            
        }
        finally
        {
            if (cn != null)
            {
                cn.close();
            }
            
        }
        
        return succes;
    
    }//End metod addCase
    
    public bool updateCase(Case caset)
    {
        bool succes = false;
        String tempstring;
        Connection cn = null;
        String DBURL = "jdbc:mysql://localhost:3306/AES?" +
        "user=root&password=aik71111";
        Timestamp sqlStartDate = new Timestamp(caset.getSkapad().getTime());
        Timestamp sqlAndradDate = new Timestamp(caset.getAndrad().getTime());

        
        try
        {
            Class.forName("com.mysql.jdbc.Driver");
            cn = DriverManager.getConnection(DBURL);
            
            if (cn == null)
            {
                throw new SQLException("Uppkoppling mot databas saknas");
            }
            
            PreparedStatement caseStmt = cn.prepareStatement("update CASES set TITEL = ?, "+
                    "DESCRIPTION = ?,ANDRATDEN = ?, STATUS = ?, PHONENR = ?, COMPUTERNAME = ?, " +
                    "TIDBEREKNAD = ?, ASSIGNE = ?, DEPARTMENT = ?, TIDSLUTFORT = ?, KATEGORI = ? " +
                    "where CASES_ID= '"+caset.getId()+"'");
                        
            
            caseStmt.setString(1, caset.getTitel());
            caseStmt.setString(2, caset.getCaseDesc());
            caseStmt.setTimestamp(3, sqlAndradDate);
            caseStmt.setString(4, caset.getStatus());
            caseStmt.setString(5, caset.getPhoneNR());
            caseStmt.setString(6, caset.getCompName());
            caseStmt.setInt(7, caset.getBeraknadTid());
            caseStmt.setString(8, caset.getTilldeladTill());
            caseStmt.setString(9, caset.getAvdelning());
            caseStmt.setInt(10, caset.getTidsAtgang());
            caseStmt.setString(11, caset.getKategori());
          
            
            
            int i = caseStmt.executeUpdate();
            caseStmt.execute();
            
            if (i > 0 )
            {
                tempstring = caset.getComments();
                String empty = "";
                
                if (tempstring != null && !tempstring.equals(empty))
                {
                   
                    PreparedStatement commentStmt = cn.prepareStatement("INSERT INTO CASE_COMMENTS(COMMENTS, Timed, CASE_ID)" + "VALUES (?, ?, ?)");
                    commentStmt.setString(1, caset.getComments());
                    commentStmt.setTimestamp(2, sqlAndradDate);
                    commentStmt.setInt(3, caset.getId());


                    int x = commentStmt.executeUpdate();
                    if (x > 0)
                    {
                        succes = true;
                    } //End inner, inner if, verifying comments insert success

                }//End inner if, runningt comments insert statement
                
                if(caset.getSolution() != null && !caset.getSolution().isEmpty())
                {
                    PreparedStatement solutionStmt = cn.prepareStatement("INSERT INTO SOLUTIONS(SOLUTION, Timed, CASE_ID)" + "VALUES(?, ?, ?)");
                    solutionStmt.setString(1, caset.getSolution());
                    solutionStmt.setTimestamp(2, sqlAndradDate);
                    solutionStmt.setInt(3, caset.getId());
                    
                    
                }
                
                
                succes = true;
            }//End outer if verifyt case update
            
            
        } catch (ClassNotFoundException classE)
        {
            throw new SQLException("Problem med db:" + classE.getMessage());
        }
        catch(NullPointerException nullE)
        {
            throw new NullPointerException("Problem " + nullE.getMessage());
        }
                
        
        finally
        {
            if (cn != null)
                cn.close();
        }
        
        return  succes;
        
    } //End method updateCase
    }
}
