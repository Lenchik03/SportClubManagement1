using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportClubManagement.mvvm.model
{
    public class CoachRepository
    {
        static CoachRepository instance;
        public static CoachRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new CoachRepository();
                return instance;
            }
        }

        // запрос на чтение всех тренеров с БД
        internal IEnumerable<Coach> GetAllCoaches()
        {
            var result = new List<Coach>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            string sql = "SELECT coachID as id, FIO, typeActivitiesID, phone_number from coaches;";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                int id;
                while (reader.Read())
                {

                    id = reader.GetInt32("id");
                    var coach = new Coach();

                    coach.ID = id;
                    coach.FIO = reader.GetString("FIO");
                    coach.TypeActivitiesID = reader.GetInt32("typeActivitiesID");
                    coach.PhoneNumber = reader.GetString("phone_number");
                    result.Add(coach);
                }
            }
            return result;
        }

        //запрос на добавление тренера в БД
        internal void AddCoach(Coach coach)
        {
            try
            {
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return;

                int id = MySqlDB.Instance.GetAutoID("coaches");

                string sql = "INSERT INTO coaches VALUES (0, @FIO, @typeActivitiesID, @phone_number)";
                using (var mc = new MySqlCommand(sql, connect))
                {
                    mc.Parameters.Add(new MySqlParameter("FIO", coach.FIO));
                    mc.Parameters.Add(new MySqlParameter("typeActivitiesID", coach.TypeActivitiesID));
                    mc.Parameters.Add(new MySqlParameter("phone_number", coach.PhoneNumber));
                    mc.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // запрос на редактирование тренера в БД
        internal void Update(Coach coach)
        {
            try
            {
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return;


                string sql = "UPDATE coaches SET FIO = @FIO, typeActivitiesID = @typeActivitiesID, phone_number = @phone_number WHERE coachID = '" + coach.ID + "';";
                using (var mc = new MySqlCommand(sql, connect))
                {
                    mc.Parameters.Add(new MySqlParameter("FIO", coach.FIO));
                    mc.Parameters.Add(new MySqlParameter("typeActivitiesID", coach.TypeActivitiesID));
                    mc.Parameters.Add(new MySqlParameter("phone_number", coach.PhoneNumber));
                    mc.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // запрос на удаление тренера из БД
        internal void Remove(Coach coach)
        {
            try
            {
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return;

                string sql = "DELETE FROM coaches WHERE coachID = '" + coach.ID + "';";

                using (var mc = new MySqlCommand(sql, connect))
                    mc.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        }
    }

