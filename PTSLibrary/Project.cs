using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSLibrary
{
    class Project
    {

        private string name;
        private DateTime expectedStartDate;
        private DateTime expectedEndDate;
        private Customer theCustomer;
        private Guid projectId;
        private List<Task> tasks;
        private string v;
        private DateTime dateTime1;
        private DateTime dateTime2;
        private Guid guid;

        public string Name {
            get { return name; }
            set { name = value; }
        }
        public DateTime StartDate
        {
            get { return expectedStartDate; }
            set { expectedStartDate = value; }
        }
        public DateTime EndDate
        {
            get { return expectedEndDate; }
            set { expectedEndDate = value; }
        }
        public Customer Customer
        {
            get { return theCustomer; }
            set { theCustomer = value;  }
        }
        public Guid ProjectId
        {
            get { return projectId; }
            set { projectId = value;  }
        }
        public List<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        // the project class has been set up with two constructors, one for the tasks and the other for the customer
        public Project(string name, DateTime startDate, DateTime endDate, Guid projectId, Customer customer)
        {
            this.name = name;
            this.projectId = projectId;
            this.expectedStartDate = startDate;
            this.expectedEndDate = endDate;
            this.theCustomer = customer;
        }

        public Project(string name, DateTime startDate, DateTime endDate, Guid projectId, List<Task> tasks)
        {
            this.name = name;
            this.projectId = projectId;
            this.expectedEndDate = endDate;
            this.expectedStartDate = startDate;
            this.tasks = tasks;
        }

       
    }
}
