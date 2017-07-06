using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PTSLibrary.DAO
{
    class SuperDAO
    {
        protected Customer GetCustomer(int custId)
        {
            // declare objects necessary to access the database
            Customer cust;
            string sql = "SELECT * FROM Customer WHERE CustomerId=" + custId;
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();// first open the connection
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow); // sql command is set to return a single row
                dr.Read();
                 cust = new Customer(dr["Name"].ToString(), (int)dr["CustomerId"]);
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception (" Error getting new customer", ex);
            }
            finally
            {
                cn.Close();
            }
            return cust;
        }

        public List<Task> GetListOfTasks(Guid projectId)
        {
            List<Task> tasks = new List<Task>();
            string sql = " SELECT * FROM Task  WHERE ProjectId=" + projectId;
            SqlConnection  cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open(); // opens a connection
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Task t = new Task((Guid)dr["TaskId"], dr["Name"].ToString(), (Status)(int)dr["StatusId"]);
                    tasks.Add(t);
                }
                dr.Close();
            }
            catch (SqlException e)
            {
                throw new Exception("Error getting Task List", e);
            }
           finally
            {
                cn.Close();
            }
            return tasks;
        }
    }

}