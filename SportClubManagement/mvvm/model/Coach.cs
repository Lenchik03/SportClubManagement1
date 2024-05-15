using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubManagement.mvvm.model
{
    public class Coach
    {
        public int ID { get; set; }
        public string FIO { get; set; } = string.Empty;
        public int TypeActivitiesID { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string TypeActivities { get; set; } = string.Empty;
    }
}
