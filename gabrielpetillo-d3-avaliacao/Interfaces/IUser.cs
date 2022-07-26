using gabrielpetillo_d3_avaliacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gabrielpetillo_d3_avaliacao.Interfaces
{
    /// <summary>
    /// User basic operations interface
    /// </summary>
    internal interface IUser
    {
        List<User> SearchAll();
        //User SearchByEmail(string email);
        bool Create(User User, string action);
        bool Update(User User);
        bool Delete(string idUser);
    }
}