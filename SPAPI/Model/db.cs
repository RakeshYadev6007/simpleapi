using System.Data;
using System.Data.SqlClient;

namespace SPAPI.Model
{
    public class db
    {
        SqlConnection con = new SqlConnection("Server=RAKESH\\SQLEXP2016; Database=record; Trusted_Connection= true;");
        public string EmployeesOpt(Employee emp)
        {
            string msg = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("usp_GetAllEmployees", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.Id);
                com.Parameters.AddWithValue("@Name", emp.Name);
                com.Parameters.AddWithValue("@Age", emp.Age);
                com.Parameters.AddWithValue("@Active", emp.Active);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "SUCCESS";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return msg;
        }
        //Get Record
        public DataSet EmployeesGet(Employee emp, out string msg)
        {
            msg = string.Empty;
            DataSet ds = new DataSet();

            try
            {
                SqlCommand com = new SqlCommand("usp_GetAllEmploy", con);
                com.CommandType = CommandType.StoredProcedure;
               
                //com.Parameters.AddWithValue("@Id", emp.Id);
                //com.Parameters.AddWithValue("@Name", emp.Name);
                //com.Parameters.AddWithValue("@Age", emp.Age);
                //com.Parameters.AddWithValue("@Active", emp.Active);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
                //con.Open();
                //int i = 0;
                //i = com.ExecuteNonQuery();
                //con.Close();
                msg = "SUCCESS";
                return ds;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return ds;
        }
    }
}

