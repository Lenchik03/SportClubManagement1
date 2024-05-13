using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubManagement.mvvm.model
{
    public class TypeActivitiesRepository
    {

        public TypeActivitiesRepository()
        {

        }

        static TypeActivitiesRepository instance;
        public static TypeActivitiesRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new TypeActivitiesRepository();
                return instance;
            }
        }
        internal IEnumerable<TypeActivities> GetAllTypeActivities()
        {
            var result = new List<TypeActivities>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            string sql = "SELECT typeActivitiesID as id, title from typeActivities;";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                TypeActivities membership = new TypeActivities();
                int id;
                while (reader.Read())
                {

                    id = reader.GetInt32("id");
                    if (membership.ID != id)
                    {
                        membership = new TypeActivities();
                        result.Add(membership);
                        membership.ID = id;
                        membership.Title = reader.GetString("title");

                    }
                }
            }

            return result;
        }
    }
}
