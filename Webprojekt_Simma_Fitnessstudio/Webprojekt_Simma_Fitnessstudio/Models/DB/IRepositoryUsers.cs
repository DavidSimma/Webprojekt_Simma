using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webprojekt_Simma_Fitnessstudio.Models;

namespace Webprojekt_Simma_Fitnessstudio.Models.DB
{
    interface IRepositoryUsers
    {
        void Open();
        void Close();

        User getUserByUsername(string username);
        bool Insert(User user);
        bool Delete(string username);
        bool Update(string username, User newUser);
        bool Login(string username, string password);
    }
}
