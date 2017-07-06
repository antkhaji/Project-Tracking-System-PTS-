using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSLibrary
{
    class TeamLeader : User
    {
        private int teamId;

        // create a getter and setter first for the teamid
        public int TeamId
        {
            get { return teamId; }
            set { teamId = value;  }
        }

        public TeamLeader(string name, int teamId, int id)
        {
            this.name = name;
            this.id = id;
            this.teamId = teamId;
        }
    }
}
