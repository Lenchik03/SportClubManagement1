using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportClubManagement.mvvm.model
{
    public class ManagerRepository
    {
        public ManagerRepository()
        {

        }

        static ManagerRepository instance;
        public static ManagerRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ManagerRepository();
                return instance;
            }
        }

        internal IEnumerable<Manager> GetAllManagers()
        {
            var result = new List<Manager>();
            var connect = MySqlDB.Instance.GetConnection();
            if (connect == null)
                return result;
            string sql = "SELECT managerID as id, FIO, phone_number, email, password FROM managers;";
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Manager manager = new Manager();
                int id;
                while (reader.Read())
                {

                    id = reader.GetInt32("id");
                    if (manager.ID != id)
                    {
                        manager = new Manager();
                        result.Add(manager);
                        manager.ID = id;
                        manager.FIO = reader.GetString("FIO");
                        manager.PhoneNumber = reader.GetString("phone_number");
                        manager.Email = reader.GetString("email");
                        manager.Password = reader.GetString("password");

                    }
                }
            }

            return result;
        }

        ////запрос на добавление менеджера в БД
        internal void AddManager(Manager manager)
        {
            try
            {
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return;

                int id = MySqlDB.Instance.GetAutoID("managers");
                manager.ID = id;
                string sql = "INSERT INTO managers VALUES (0, @FIO, @phone_number, @email, @password)";
                using (var mc = new MySqlCommand(sql, connect))
                {
                    mc.Parameters.Add(new MySqlParameter("FIO", manager.FIO));
                    mc.Parameters.Add(new MySqlParameter("phone_number", manager.PhoneNumber));
                    mc.Parameters.Add(new MySqlParameter("email", manager.Email));
                    mc.Parameters.Add(new MySqlParameter("password", Md5Hash.HashPassword(manager.Password)));
                    mc.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //internal void UpdateManager(Manager manager)
        //{
        //    try
        //    {
        //        var connect = MySqlDB.Instance.GetConnection();
        //        if (connect == null)
        //            return;


        //        string sql = "SELECT managerID as id, FIO, email, password, phone_number FROM managers WHERE managerID = '" + manager.ID + "';";
        //        using (var mc = new MySqlCommand(sql, connect))
        //        {
        //            mc.Parameters.Add(new MySqlParameter("FIO", manager.FIO));
        //            mc.Parameters.Add(new MySqlParameter("email", manager.Email));
        //            mc.Parameters.Add(new MySqlParameter("password", manager.Password));
        //            mc.Parameters.Add(new MySqlParameter("phone_number", manager.PhoneNumber));
        //            mc.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        // запрос на чтение таблицы менеджеров с параметром с целью получения менеджера с помощью сравнения введенных менеджером данных с имеющимися в базе данных.
        internal Manager ActiveManager(string email, string password)
        {
            try
            {
                Manager manager = new Manager();
                var connect = MySqlDB.Instance.GetConnection();
                if (connect == null)
                    return manager;

                string sql = "SELECT mangerID as id, FIO, email, phone_number FROM managers WHERE email = '" + email + "' AND password = '" + Md5Hash.HashPassword(password) + "';";

                using (var mc = new MySqlCommand(sql, connect))
                using (var reader = mc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        manager.ID = reader.GetInt32("id");
                        manager.FIO = reader.GetString("FIO");
                        manager.PhoneNumber = reader.GetString("phone_number");
                        manager.Email = reader.GetString("email");
                        
                        
                    }
                }

                return manager;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
