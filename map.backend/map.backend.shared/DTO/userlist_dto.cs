using map.backend.shared.Entities.Auth;
using map.backend.shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.DTO
{
    public class userlist_dto
    {
        public List<user_dto> users {  get; set; }
        public List<role_dto> roles { get; set; }
    }
}
