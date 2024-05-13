using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubManagement.mvvm.model
{
    public class Client
    {
        public int ID { get; set; }
        public string FIO { get; set; } = string.Empty;
        public int MembershipID { get; set; }
        public int TypeActivitiesID { get; set; }
        public int CoachID { get; set; }
        public int ClassesDays { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


        public string Membership { get; set; } = string.Empty;
        public string TypeActivities { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;


    }
}
