using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webprojekt_Simma_Fitnessstudio.Models
{
    public enum Gender
    {
        male, female
    }
    public class User
    {
        private string _password, _username;
        public string UserName {
            get
            {
                return this._username;
            }
            set
            {
                if (value.Length >= 4)
                {
                    this._username = value;
                }
            }
        }
        public string Password {
            get
            {
                return this._password;
            }
            set
            {
                if(value.Length >= 4)
                {
                    this._password = value;
                }
            }
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Age { get; set; }
        public Gender Gender { get; set; }

        public User() : this("","","","", DateTime.MinValue, Gender.male) { }
        public User(string username, string password, string firstname, string lastname, DateTime age, Gender gender)
        {
            this.UserName = username;
            this.Password = password;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Age = age;
            this.Gender = Gender;
        }
    }
}