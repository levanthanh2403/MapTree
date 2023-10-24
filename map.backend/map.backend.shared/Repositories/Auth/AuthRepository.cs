using map.backend.shared.DTO;
using map.backend.shared.Entities.Auth;
using map.backend.shared.Interfaces.Auth;
using map.backend.shared.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Ocsp;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace map.backend.shared.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _iconfiguration;
        public AuthRepository(IUnitOfWork unitOfWork, IConfiguration iconfiguration)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _iconfiguration = iconfiguration ?? throw new ArgumentNullException(nameof(iconfiguration));
        }
        public async Task<login_response> loginAction(login_request req)
        {
            login_response res = new login_response();
            var _userRepository = _unitOfWork.GetRepository<tb_user>(true);
            var _user = await _userRepository.GetFirstOrDefaultAsync(predicate: o => o.userid.ToUpper() == req.userid.ToUpper()
                                                                                  && o.record_stat == "O");
            if (_user == null)
                throw new Exception("Thông tin đăng nhập không chính xác không chính xác!");
            else if (_user.status == "C")
                throw new Exception("Tài khoản đã bị khóa!");
            bool verified = BCrypt.Net.BCrypt.Verify(req.password, _user.password);
            if (!verified)
                throw new Exception("Thông tin đăng nhập không chính xác không chính xác!");
            _user.limit = 5;
            await _userRepository.UpdateAsync(_user);
            //Tạo token đăng nhập
            var tokenHandler = new JwtSecurityTokenHandler();
            var aaa = _iconfiguration["JWT:Key"];
            var key = Encoding.ASCII.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("userid", _user.userid == null ? "" : _user.userid.ToUpper()),
                        new Claim("username", _user.username == null ? "" : _user.username),
                        new Claim("email", _user.email == null ? "" : _user.email),
                        new Claim("phonenumber", _user.phone == null ? "" : _user.phone),
                        new Claim("role", _user.rolecode == null ? "" : _user.rolecode)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            res.token = token;
            res.img = _user.img;
            return res;
        }
        public async Task<register_response> RegisterUser(register_request req)
        {
            register_response res = new register_response();
            if (string.IsNullOrEmpty(req.userName)) throw new Exception("Vui lòng nhập họ tên!");
            if (string.IsNullOrEmpty(req.userId)) throw new Exception("Vui lòng nhập tên đăng nhập!");
            if (string.IsNullOrEmpty(req.password)) throw new Exception("Vui lòng nhập mật khẩu!");
            if (string.IsNullOrEmpty(req.rePassword)) throw new Exception("Vui lòng nhập lại mật khẩu!");
            if (string.IsNullOrEmpty(req.rolecode)) throw new Exception("Vui lòng chọn quyền!");
            if (req.password != req.rePassword)
                throw new Exception("Mật khẩu không khớp!");
            var _userRepository = _unitOfWork.GetRepository<tb_user>(true);
            var _user = await _userRepository.GetFirstOrDefaultAsync(predicate: o => o.userid.ToUpper() == req.userId.ToUpper());
            if (_user != null)
                throw new Exception("Tài khoản đã được đăng ký, vui lòng nhập tài khoản khác!");
            tb_user _data = new tb_user();
            _data.userid = req.userId.ToUpper();
            _data.username = req.userName;
            _data.password = BCrypt.Net.BCrypt.HashPassword(req.password);
            _data.email = req.email;
            _data.phone = req.phone;
            _data.limit = 5;
            _data.status = string.IsNullOrEmpty(req.status) ? "O" : req.status;
            _data.img = req.img;
            _data.create_by = _userRepository.GetUserFromToken();
            _data.create_date = DateTime.Now;
            _data.record_stat = "O";
            _data.rolecode = req.rolecode;
            await _userRepository.AddAsync(_data);
            res.resDesc = "Đăng ký tài khoản thành công!";
            return res;
        }
        public async Task<register_response> UpdateUser(register_request req)
        {
            register_response res = new register_response();
            if (string.IsNullOrEmpty(req.userName)) throw new Exception("Vui lòng nhập họ tên!");
            if (string.IsNullOrEmpty(req.userId)) throw new Exception("Vui lòng nhập tên đăng nhập!");
            if (string.IsNullOrEmpty(req.rolecode)) throw new Exception("Vui lòng chọn quyền!");
            var _userRepository = _unitOfWork.GetRepository<tb_user>(true);
            var _user = await _userRepository.GetFirstOrDefaultAsync(predicate: o => o.userid.ToUpper() == req.userId.ToUpper());
            if (_user == null)
                throw new Exception("Tài khoản không tồn tại, vui lòng kiểm tra lại!");

            if (!string.IsNullOrEmpty(req.password) && !string.IsNullOrEmpty(req.rePassword))
            {
                if (req.password != req.rePassword)
                    throw new Exception("Mật khẩu không khớp!");
                _user.password = BCrypt.Net.BCrypt.HashPassword(req.password);

            }
            _user.username = string.IsNullOrEmpty(req.userName) ? _user.username : req.userName;
            _user.email = string.IsNullOrEmpty(req.email) ? _user.email : req.email;
            _user.phone = string.IsNullOrEmpty(req.phone) ? _user.phone : req.phone;
            _user.limit = 5;
            _user.status = string.IsNullOrEmpty(req.status) ? _user.status : req.status;
            _user.img = string.IsNullOrEmpty(req.img) ? _user.img : req.img;
            _user.modify_by = _userRepository.GetUserFromToken();
            _user.modify_date = DateTime.Now;
            _user.rolecode = string.IsNullOrEmpty(req.rolecode) ? _user.rolecode : req.rolecode;
            await _userRepository.UpdateAsync(_user);
            res.resDesc = "Cập nhât tài khoản thành công!";
            return res;
        }
        public async Task<userlist_dto> GetListUser(int page, int pagesize, string userId, string username, string phone, string email, string status, string rolecode)
        {
            userlist_dto res = new userlist_dto();
            var _userRepository = _unitOfWork.GetRepository<tb_user>(true);
            var _roleRepository = _unitOfWork.GetRepository<tb_roles>(true);
            //var _pageEntity = await _userRepository.GetPaged(page, pagesize,
            //    filter: o => o.userid.ToLower().Contains("%" + userId == null ? "" : userId.ToLower() + "")
            //              && o.username.ToLower().Contains("%" + username == null ? "" : username.ToLower() + "")
            //              && o.phone.ToLower().Contains("%" + phone == null ? "" : phone.ToLower() + "")
            //              && o.email.ToLower().Contains("%" + email == null ? "" : email.ToLower() + "")
            //              && o.status == (string.IsNullOrEmpty(status) ? o.status : status),
            //    orderBy: o => o.OrderByDescending(i => i.create_date));
            //res.users = _pageEntity;
            var _userdto = await (from a in _userRepository.GetAll()
                                  join b in _roleRepository.GetAll() on a.rolecode equals b.rolecode
                                  where a.userid.ToLower().Contains(string.IsNullOrEmpty(userId) ? "" : userId.ToLower())
                                     && a.username.ToLower().Contains(string.IsNullOrEmpty(username) ? "" : username.ToLower())
                                     && a.phone.ToLower().Contains(string.IsNullOrEmpty(phone) ? "" : phone.ToLower())
                                     && a.email.ToLower().Contains(string.IsNullOrEmpty(email) ? "" : email.ToLower())
                                     && a.status == (string.IsNullOrEmpty(status) ? a.status : status)
                                     && a.rolecode == (string.IsNullOrEmpty(rolecode) ? a.rolecode : rolecode)
                                  orderby a.userid ascending
                                  select new user_dto
                                  {
                                      userid = a.userid,
                                      username = a.username,
                                      email = a.email,
                                      phone = a.phone,
                                      status = (a.status == "O" ? "Mở" : "Đóng"),
                                      createdBy = a.create_by,
                                      createdDate = a.create_date.ToString(),
                                      rolecode = a.rolecode,
                                      rolename = b.rolename
                                  }).ToListAsync();
            var _roledto = await (from a in _roleRepository.GetAll()
                                  select new role_dto
                                  {
                                      rolecode = a.rolecode,
                                      rolename = a.rolename
                                  }).ToListAsync();
            res.users = _userdto;
            res.roles = _roledto;
            return res;
        }
        public async Task<user_dto> GetDetailUser(string userId)
        {
            user_dto res = new user_dto();
            var _userRepository = _unitOfWork.GetRepository<tb_user>(true);
            var _roleRepository = _unitOfWork.GetRepository<tb_roles>(true);
            var _user = await (from a in _userRepository.GetAll()
                               join b in _roleRepository.GetAll() on a.rolecode equals b.rolecode
                               where a.userid.ToUpper() == userId.ToUpper()
                               select new user_dto
                               {
                                   userid = a.userid,
                                   username = a.username,
                                   email = a.email,
                                   phone = a.phone,
                                   status = a.status,
                                   img = a.img,
                                   createdBy = a.create_by,
                                   createdDate = a.create_date.ToString(),
                                   rolecode = a.rolecode,
                                   rolename = b.rolename
                               }).FirstOrDefaultAsync();
            if (_user == null)
                throw new Exception("Tài khoản không tồn tại, vui lòng kiểm tra lại!");
            return _user;
        }
    }
}
