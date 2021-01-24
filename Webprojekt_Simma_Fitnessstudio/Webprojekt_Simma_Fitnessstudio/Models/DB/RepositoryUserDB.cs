using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Webprojekt_Simma_Fitnessstudio.Models;

namespace Webprojekt_Simma_Fitnessstudio.Models.DB
{
    public class RepositoryUserDB : IRepositoryUsers
    {
        private MySqlConnection _connection = null;
        private string _connectionString = "server=localhost;database=swp_fitnessstudio;uid=user;pwd=user";
        public void Open()
        {
            if (this._connection == null)
            {
                this._connection = new MySqlConnection(this._connectionString);
            }
            
            if (this._connection.State == ConnectionState.Open)
            {
                this._connection.Open();
                
            }
        }
        public void Close()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Close();
            }
        }

        User IRepositoryUsers.getUserByUsername(string username)
        {
            if (this._connection.State == ConnectionState.Open)
            {
                User user = new User();

                DbCommand cmdSelect = this._connection.CreateCommand();

                cmdSelect.CommandText = "select * from users where username = @username";

                using (DbDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        user = new User
                        {
                            UserName = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
                            Firstname = Convert.ToString(reader["firstname"]),
                            Lastname = Convert.ToString(reader["lastname"]),
                            Age = Convert.ToDateTime(reader["age"]),
                            Gender = (Gender)Convert.ToInt32(reader["gender"])
                        };
                    }
                } 

                if (user == null)
                {
                    return null;
                }
                return user;
            }
            throw new Exception("Datenbank: Verbindung ist nicht geöffnet!");
            throw new NotImplementedException();

        }

        bool IRepositoryUsers.Insert(User user)
        {
            if (user == null)
            {
                return false;
            }
            if (this._connection.State == ConnectionState.Open)
            {

                DbCommand cmdInsert = this._connection.CreateCommand();

                cmdInsert.CommandText = "insert into users values(@username, @password, @firstname, @lastname, @age, @gender);";

                DbParameter haramUsername = cmdInsert.CreateParameter();
                haramUsername.ParameterName = "username";
                haramUsername.DbType = DbType.String;
                haramUsername.Value = user.UserName;

                DbParameter haramPassword = cmdInsert.CreateParameter();
                haramPassword.ParameterName = "password";
                haramPassword.DbType = DbType.String;
                haramPassword.Value = user.Password;

                DbParameter haramFirstname = cmdInsert.CreateParameter();
                haramFirstname.ParameterName = "firstname";
                haramFirstname.DbType = DbType.String;
                haramFirstname.Value = user.Firstname;

                DbParameter haramLastname = cmdInsert.CreateParameter();
                haramLastname.ParameterName = "lastname";
                haramLastname.DbType = DbType.String;
                haramLastname.Value = user.Lastname;

                DbParameter haramAge = cmdInsert.CreateParameter();
                haramAge.ParameterName = "age";
                haramAge.DbType = DbType.Date;
                haramAge.Value = user.Age;

                DbParameter haramGender = cmdInsert.CreateParameter();
                haramGender.ParameterName = "gender";
                haramGender.DbType = DbType.Int32;
                haramGender.Value = user.Gender;

                cmdInsert.Parameters.Add(haramUsername);
                cmdInsert.Parameters.Add(haramPassword);
                cmdInsert.Parameters.Add(haramFirstname);
                cmdInsert.Parameters.Add(haramLastname);
                cmdInsert.Parameters.Add(haramAge);
                cmdInsert.Parameters.Add(haramGender);

                return cmdInsert.ExecuteNonQuery() == 1;

            }
            return false;

        }
        bool IRepositoryUsers.Delete(string username)
        {
            if (username == null)
            {
                return false;
            }
            if (this._connection.State == ConnectionState.Open)
            {
                DbCommand cmdInsert = this._connection.CreateCommand();

                cmdInsert.CommandText = "drop * from users where username = @username;";

                DbParameter haramUsername = cmdInsert.CreateParameter();
                haramUsername.ParameterName = "username";
                haramUsername.DbType = DbType.String;
                haramUsername.Value = username;

                cmdInsert.Parameters.Add(haramUsername);

                return cmdInsert.ExecuteNonQuery() == 1;
            }
            return false;
        }
    

        bool IRepositoryUsers.Update(string username, User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
