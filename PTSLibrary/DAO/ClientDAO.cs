using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PTSLibrary.DAO
{

    // this class is similar to the Customer DAO. It provides functionality that will enable the Team leader access the variouse projects in the loop.
    class ClientDAO
    {   
        // used to authenticate the team leader in the system during a login operation
        public int Authenticate(string username, string password)
        {
            string sql = String.Format("SELECT DISTINCT Person.Name, UserId, TeamId FROM Person INNER JOIN Team ON (Team.TeamLeaderId = Person.UserId) WHERE Username = '{0}' AND Password = '{1}'",
                username, password);
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            int id = 0;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    id = (int)dr["CustomerId"];
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error Accessing the Database", ex);
            }
            finally
            {
                cn.Close();
            }
            return id;
        }



    }
}
