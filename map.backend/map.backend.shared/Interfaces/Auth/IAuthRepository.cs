using map.backend.shared.DTO;
using map.backend.shared.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<login_response> loginAction(login_request req);
        Task<register_response> RegisterUser(register_request req);
        Task<register_response> UpdateUser(register_request req);
        Task<userlist_dto> GetListUser(int page, int pagesize, string userId, string username, string phone, string email, string status, string rolecode);
        Task<user_dto> GetDetailUser(string userId);
    }
}
