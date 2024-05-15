using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportClubManagement.mvvm.model
{
    public class ClientRepository
    {
        static ClientRepository instance;
        public static ClientRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClientRepository();
                return instance;
            }
        }

        // запрос на чтение всех клиентов с БД
        internal IEnumerable<Client> GetAllClients(string sql)
        {
            var result = new List<Client>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                int id;
                while (reader.Read())
                {

                    id = reader.GetInt32("clientID");
                    var client = new Client();

                    client.ID = id;
                    client.FIO = reader.GetString("FIO");
                    client.MembershipID = reader.GetInt32("membershipID");
                    client.TypeActivitiesID = reader.GetInt32("typeActivitiesID");
                    client.CoachID = reader.GetInt32("coachID");
                    client.ClassesDays = reader.GetInt32("classes_days");
                    client.PhoneNumber = reader.GetString("phone_number");
                    client.Email = reader.GetString("email");

                    client.Coach = reader.GetString("coach");
                    client.Membership = reader.GetString("membership");
                    client.TypeActivities = reader.GetString("typeActivities");
        
                    result.Add(client);
                }
            }
            return result;
        }

        //запрос на добавление клиента в БД
        internal void AddClient(Client client)
        {
            try
            {
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return;

                int id = MySqlDB.Instance.GetAutoID("clients");

                string sql = "INSERT INTO clients VALUES (0, @FIO, @membershipID, @typeActivitiesID, @coachID, @classes_days, @phone_number, @email)";
                using (var mc = new MySqlCommand(sql, connect))
                {
                    mc.Parameters.Add(new MySqlParameter("FIO", client.FIO));
                    mc.Parameters.Add(new MySqlParameter("membershipID", client.MembershipID));
                    mc.Parameters.Add(new MySqlParameter("typeActivitiesID", client.TypeActivitiesID));
                    mc.Parameters.Add(new MySqlParameter("coachID", client.CoachID));
                    mc.Parameters.Add(new MySqlParameter("classes_days", client.ClassesDays));
                    mc.Parameters.Add(new MySqlParameter("phone_number", client.PhoneNumber));
                    mc.Parameters.Add(new MySqlParameter("email", client.Email));
                    mc.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // запрос на редактирование клиента в БД
        internal void Update(Client client)
        {
            try
            {
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return;


                string sql = "UPDATE clients SET FIO = @FIO, membershipID = @membershipID, typeActivitiesID = @typeActivitiesID, coachID = @coachID, classes_days = @classes_days, phone_number = @phone_number, email = @email WHERE clientID = '" + client.ID + "';";
                using (var mc = new MySqlCommand(sql, connect))
                {
                    mc.Parameters.Add(new MySqlParameter("FIO", client.FIO));
                    mc.Parameters.Add(new MySqlParameter("membershipID", client.MembershipID));
                    mc.Parameters.Add(new MySqlParameter("typeActivitiesID", client.TypeActivitiesID));
                    mc.Parameters.Add(new MySqlParameter("coachID", client.CoachID));
                    mc.Parameters.Add(new MySqlParameter("classes_days", client.ClassesDays));
                    mc.Parameters.Add(new MySqlParameter("phone_number", client.PhoneNumber));
                    mc.Parameters.Add(new MySqlParameter("email", client.Email));
                    mc.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // запрос на удаление клмента из БД
        internal void Remove(Client client)
        {
            try
            {
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return;

                string sql = "DELETE FROM clients WHERE clientID = '" + client.ID + "';";

                using (var mc = new MySqlCommand(sql, connect))
                    mc.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // запрос на выборку клиентов по параметам ФИО клиента и номера телефона клиента
        internal IEnumerable<Client> Search(string searchText, Coach selectedCoach, Membership selectedMembership, TypeActivities selectedTypeActivities)
        {
            try
            {
                string sql = "SELECT c.clientID, c.FIO, c.membershipID, c.typeActivitiesID, c.coachID, c.classes_days, c.phone_number, c.email, ch.FIO as coach, m.tittle as membership, t.title as typeActivities FROM clients c, coaches ch, memberships m, typeActivities t WHERE c.coachID = ch.coachID AND c.membershipID = m.membershipsID AND c.typeActivitiesID = t.typeActivitiesID";
                sql += " AND( c.FIO LIKE '%" + searchText + "%'";
                sql += " OR c.phone_number LIKE '%" + searchText + "%')";
                if (selectedCoach != null && selectedCoach.ID != 0)
                    sql += " AND c.coachID = " + selectedCoach.ID;
                if (selectedMembership != null && selectedMembership.ID != 0)
                    sql += " AND c.membershipID = " + selectedMembership.ID;
                if (selectedTypeActivities != null && selectedTypeActivities.ID != 0)
                    sql += " AND c.typeActivitiesID = " + selectedTypeActivities.ID;
                sql += " ORDER BY c.clientID";

                return GetAllClients(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
    
}
