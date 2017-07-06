using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PTSLibrary.DAO
{
    class AdminDAO
    {
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

        public void CreateProject(string name, DateTime startDate, DateTime endDate, int customerId, int administratorId)
        {
            Guid projectid = Guid.NewGuid();
            string sql = "INSERT INTO Project (ProjectId, Name, ExpectedStartDate, ExpectedEndDate, CustomerId, AdministratorId)" ;
            sql += String.Format("VALUES('{0}', '{1}','{2}','{3}', {4}, {5})",projectid, name, startDate, endDate,customerId, administratorId);
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error creating  Project");
            }
            finally
            {
                cn.Close();
            }

        }

        public void CreateTask(string name, DateTime startDate, DateTime endDate, int projectId, int teamId)
        {
            Guid taskId = Guid.NewGuid();
            string sql = "INSERT INTO Task (TaskId, Name, ExpectedDateStarted, ExpectedDateCompleted, ProjectId,TeamId)";
            sql += String.Format("VALUES('{0}', '{1}','{2}','{3}', {4}, {5})", taskId, name, startDate, endDate, projectId, teamId);
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error Creating Task");
            }
            finally
            {
                cn.Close();
            }
        }

        public List<Customer> GetListOfCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string sql = " SELECT * FROM Customers" ;
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Customer custom = new Customer(dr["Name"].ToString(), (int)dr["CustomerId"]);
                   customers.Add(custom);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error getting list of Customers", ex);
            }
            finally
            {
                cn.Close();
            }

            return customers;
        }
    
        public List<Team> GetListOfTeams()
        {
            List<Team> teams = new List<Team>();
            string sql = "SELECT * FROM Team";
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Team team = new Team((int)dr["TeamId"], dr["Location"].ToString(), dr["Name"].ToString(),(TeamLeader)dr["TeamLeaderId"]);
                    teams.Add(team);
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error getting list of Teams", ex);
            }
            finally
            {
                cn.Close();
            }
            return teams;
        }

        public List<Project> GetListOfProjects(int adminId)
        {
            List<Project> projects = new List<Project>();
            string sql = " SELECT * FROM Projects  WHERE AdministratorId=" + adminId;
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
                    Project p = new Project(dr["Name"].ToString(), (DateTime)dr["ExpectedStartDate"], (DateTime)dr["ExpectedEndDate"],
                        (Guid)dr["ProjectId"], tasks);
                    projects.Add(p);

                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error getting list of projects created by this administartor", ex);
            }
            finally
            {
                cn.Close();
            }
            return projects;
        }






    }
}
