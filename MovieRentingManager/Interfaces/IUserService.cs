using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Interfaces
{
    internal interface IUserService
    {
        bool AddUser(Models.User user);
        bool RemoveUser(int userId);

        bool UpdateUser(Models.User user);
    }
}
