using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PTSLibrary.DAO
{
    class CustomerDAO
    {
        public int Authenticate(string username, string password)
        {
            string sql = String.Format("SELECT CustomerID FROM Customer WHERE Username='{0}' AND Password= '{1}'",  username,password);
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            int id = 0;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    id =(int) dr["CustomerId"];
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
        // function to get a list of the projects that a customer has commissioned.
        public List<Project> GetListOfProjects(int custId)
        {
            List<Project> projects = new List<Project>();
            string sql = " SELECT * FROM Projects  WHERE CustomerId=" + custId;
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    List<Task> tasks = new List<Task>();   
                    // query to select a list of all projects based on their project ID from the table.                
                    sql = "SELECT * FROM Task WHERE ProjectId = ' " + dr["ProjectId"].ToString() + "'";
                    SqlConnection cn2 = new SqlConnection(Properties.Settings.Default.ConnectionString);
                    SqlCommand cmd2 = new SqlCommand(sql, cn2);
                    cn2.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        Task t = new Task((Guid)dr2["TaskId"], dr2["Name"].ToString(), (Status)(int)dr2["StatusId"]);
                        tasks.Add(t);
                    }
                    dr2.Close();
                    Project p = new Project(dr["Name"].ToString(),(DateTime)dr["ExpectedStartDate"],(DateTime)dr["ExpectedEndDate"],
                        (Guid)dr["ProjectId"], tasks);
                    projects.Add(p);

                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error getting list of projects", ex);
            }
            finally
            {
                cn.Close();
            }
            return projects;  
        }
        
    }
}
