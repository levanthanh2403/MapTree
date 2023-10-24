using map.backend.shared.Entities.Auth;
using map.backend.shared.Interfaces.UnitOfWork;
using map.backend.shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Migrations.Seeder
{
    public static class SeedData
    {
        public static void Seed(AppDbContext dbContext)
        {
            string userId = "admin";
            string password = "123456";
            string passwordhash = BCrypt.Net.BCrypt.HashPassword(password);

            string userId1 = "etp";
            string password1 = "123456";
            string passwordhash1 = BCrypt.Net.BCrypt.HashPassword(password1);

            string userId2 = "user";
            string password2 = "123456";
            string passwordhash2 = BCrypt.Net.BCrypt.HashPassword(password2);

            string role = "ADMIN";
            string role_1 = "ETP";
            string role_2 = "USR";

            var checkUser = dbContext.tb_user.Where(o => o.userid == userId).FirstOrDefault();
            if (checkUser == null)
            {
                tb_user _user = new tb_user();
                _user.userid = userId;
                _user.username = "Quản trị viên";
                _user.password = passwordhash;
                _user.email = "admin@gamil.com";
                _user.phone = "0368444555";
                _user.record_stat = "O";
                _user.create_by = "AUTO";
                _user.create_date = DateTime.Now;
                _user.rolecode = role;
                dbContext.tb_user.Add(_user);
                dbContext.SaveChanges();
            }

            var checkUser1 = dbContext.tb_user.Where(o => o.userid == userId1).FirstOrDefault();
            if (checkUser1 == null)
            {
                tb_user _user = new tb_user();
                _user.userid = userId1;
                _user.username = "Doanh nghiệp";
                _user.password = passwordhash1;
                _user.email = "etp@gamil.com";
                _user.phone = "0368444666";
                _user.record_stat = "O";
                _user.create_by = "AUTO";
                _user.create_date = DateTime.Now;
                _user.rolecode = role_1;
                dbContext.tb_user.Add(_user);
                dbContext.SaveChanges();
            }

            var checkUser2 = dbContext.tb_user.Where(o => o.userid == userId2).FirstOrDefault();
            if (checkUser2 == null)
            {
                tb_user _user = new tb_user();
                _user.userid = userId2;
                _user.username = "Người dùng";
                _user.password = passwordhash2;
                _user.email = "user@gamil.com";
                _user.phone = "0368444777";
                _user.record_stat = "O";
                _user.create_by = "AUTO";
                _user.create_date = DateTime.Now;
                _user.rolecode = role_2;
                dbContext.tb_user.Add(_user);
                dbContext.SaveChanges();
            }

            var checkRole = dbContext.tb_roles.Where(o => o.rolecode == role).ToList();
            if (checkRole.Count() == 0)
            {
                tb_roles _role = new tb_roles();
                _role.rolecode = role;
                _role.rolename = "Quản trị";
                _role.record_stat = "O";
                _role.create_by = "AUTO";
                _role.create_date = DateTime.Now;
                dbContext.tb_roles.Add(_role);
                dbContext.SaveChanges();
            }
            var checkRole_1 = dbContext.tb_roles.Where(o => o.rolecode == role_1).ToList();
            if (checkRole_1.Count() == 0)
            {
                tb_roles _role = new tb_roles();
                _role.rolecode = role_1;
                _role.rolename = "Doanh nghiệp";
                _role.record_stat = "O";
                _role.create_by = "AUTO";
                _role.create_date = DateTime.Now;
                dbContext.tb_roles.Add(_role);
                dbContext.SaveChanges();
            }
            var checkRole_2 = dbContext.tb_roles.Where(o => o.rolecode == role_2).ToList();
            if (checkRole_2.Count() == 0)
            {
                tb_roles _role = new tb_roles();
                _role.rolecode = role_2;
                _role.rolename = "Người dùng";
                _role.record_stat = "O";
                _role.create_by = "AUTO";
                _role.create_date = DateTime.Now;
                dbContext.tb_roles.Add(_role);
                dbContext.SaveChanges();
            }

            //var checkScreen = dbContext.tb_screens.ToList();
            //if (checkScreen.Count() == 0)
            //{
            //    List<tb_screens> _screens = new List<tb_screens>
            //    {
            //        new tb_screens {screencode = "HOME", screenname = "Trang chủ", screenorg = "HOME", group = "APP", screenurl = "/home", icon = "feather icon-home", index = 1, record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_screens {screencode = "MAP", screenname = "Bản đồ", screenorg = "MAP", group = "APP", screenurl = "/map", icon = "feather icon-sidebar", index = 2, record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_screens {screencode = "REPORT", screenname = "Báo cáo", screenorg = "REPORT", group = "SUMARY", screenurl = "/report", icon = "feather icon-file-text", index = 1, record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_screens {screencode = "CONFIG", screenname = "Thiết lập chung", screenorg = "CONFIG", group = "SYSTEM", screenurl = "", icon = "feather icon-box", index = 1, record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_screens {screencode = "LOCATION", screenname = "Vị trí", screenorg = "CONFIG", group = "", screenurl = "/system/locations", icon = "", index = 1, record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_screens {screencode = "USER", screenname = "Tài khoản", screenorg = "CONFIG", group = "", screenurl = "/system/users", icon = "", index = 2, record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_screens {screencode = "ROLE", screenname = "Phân quyền", screenorg = "CONFIG", group = "", screenurl = "/system/roles", icon = "", index = 3, record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //    };
            //    dbContext.tb_screens.AddRange(_screens);
            //    dbContext.SaveChanges();
            //}
            //var checkRoleMap = dbContext.tb_role_mapping.ToList();
            //if(checkRoleMap.Count == 0)
            //{
            //    List<tb_role_mapping> _roleMapping = new List<tb_role_mapping>
            //    {
            //        new tb_role_mapping {rolecode = "ADMIN", screencode = "HOME", screenorg = "HOME", record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_role_mapping {rolecode = "ADMIN", screencode = "MAP", screenorg = "MAP", record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_role_mapping {rolecode = "ADMIN", screencode = "REPORT", screenorg = "REPORT", record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_role_mapping {rolecode = "ADMIN", screencode = "CONFIG", screenorg = "CONFIG", record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_role_mapping {rolecode = "ADMIN", screencode = "LOCATION", screenorg = "CONFIG", record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_role_mapping {rolecode = "ADMIN", screencode = "USER", screenorg = "CONFIG", record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //        new tb_role_mapping {rolecode = "ADMIN", screencode = "ROLE", screenorg = "CONFIG", record_stat = "O", create_by = "AUTO", create_date = DateTime.Now},
            //    };
            //    dbContext.tb_role_mapping.AddRange(_roleMapping);
            //    dbContext.SaveChanges();
            //}
        }
    }
}
