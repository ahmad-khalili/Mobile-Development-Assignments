using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD_Assignment3.Models
{
    public class AdminUser : User
    {
        public List<StaffUser> StaffUsers { get; set; }

        public override string ToString()
        {
            return base.ToString() +  $" -- Staff Users: {printStaffUsers()}";
        }

        private string printStaffUsers()
        {
            string staffUsers = string.Empty;
            int count = 0;
            foreach (var staffUser in StaffUsers)
            {
                if (count > 0)
                {
                    staffUsers += " -- ";
                }
                staffUsers += $"{staffUser.Name}";
                count++;
            }
            return staffUsers;
        }
    }
}
