using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubManagement.mvvm.model
{
    public class MembershipRepository
    {
        public MembershipRepository()
        {

        }

        static MembershipRepository instance;
        public static MembershipRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new MembershipRepository();
                return instance;
            }
        }
        internal IEnumerable<Membership> GetAllMemberships()
        {
            var result = new List<Membership>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            string sql = "SELECT membershipsID as id, tittle from memberships;";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Membership membership = new Membership();
                int id;
                while (reader.Read())
                {

                    id = reader.GetInt32("id");
                    if (membership.ID != id)
                    {
                        membership = new Membership();
                        result.Add(membership);
                        membership.ID = id;
                        membership.Title = reader.GetString("tittle");

                    }
                }
            }

            return result;
        }
    }
}
