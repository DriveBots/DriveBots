using System;
using System.Data;
using System.Data.SqlClient;

public class Class1
{


    public DataSet GetData()
    {
        try
        {
            string strcon = "Server=(localdb)\\mssqllocaldb;Database=DriveBots;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SELECT * FROM DriveBots.dbo.Appointments", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);

            return ds;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



}
