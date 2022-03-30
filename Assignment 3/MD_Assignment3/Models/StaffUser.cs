using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD_Assignment3.Models
{
    public class StaffUser : User
    {
        public enum Roles { Role1, Role2, Role3 };
        public Roles Role { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" -- Role: {Role}";
        }
    }
}
