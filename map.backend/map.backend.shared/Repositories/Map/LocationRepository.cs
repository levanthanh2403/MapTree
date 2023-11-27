using DocumentFormat.OpenXml.Spreadsheet;
using map.backend.shared.DTO;
using map.backend.shared.Entities.Auth;
using map.backend.shared.Entities.Map;
using map.backend.shared.Helper;
using map.backend.shared.Interfaces.Map;
using map.backend.shared.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Npgsql;
using NPOI.SS.Formula.Functions;
using RTools_NTS.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace map.backend.shared.Repositories.Map
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public LocationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<locationlist_dto> getListLocation(string locationid, string projectid, string projectname,
            string locationname, string address, string locationstatus, string treecode,
            string treename, string treetype, string treestatus, string record_stat,
            string fromCreatedDate, string toCreatedDate)
        {
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);
            var _locationUserRepository = _unitOfWork.GetRepository<tb_location_users>(true);
            locationlist_dto res = new locationlist_dto();

            string _fromCreatedDate = fromCreatedDate + " 00:00:00";
            string _toCreatedDate = toCreatedDate + " 23:59:59";

            var _locationByRole = await (from a in _locationUserRepository.GetAll()
                                         where a.userid.ToUpper() == _locationUserRepository.GetUserFromToken()
                                         select a.locationid.ToUpper()).ToListAsync();

            var _data = await (from a in _locationRepository.GetAll()
                               join b in _projectRepository.GetAll() on a.projectid equals b.projectid
                               where a.locationid.ToUpper().Contains(string.IsNullOrEmpty(locationid) ? "" : locationid.ToUpper())
                                  && a.projectid.ToUpper().Contains(string.IsNullOrEmpty(projectid) ? "" : projectid.ToUpper())
                                  && b.projectname.ToUpper().Contains(string.IsNullOrEmpty(projectname) ? "" : projectname.ToUpper())
                                  && a.locationname.ToUpper().Contains(string.IsNullOrEmpty(locationname) ? "" : locationname.ToUpper())
                                  && a.address.ToUpper().Contains(string.IsNullOrEmpty(address) ? "" : address.ToUpper())
                                  && a.locationstatus.Contains(string.IsNullOrEmpty(locationstatus) ? "" : locationstatus)
                                  && (string.IsNullOrEmpty(a.treecode) ? " " : a.treecode.ToUpper()).Contains(string.IsNullOrEmpty(treecode) ? "" : treecode.ToUpper())
                                  && (string.IsNullOrEmpty(a.treename) ? " " : a.treename.ToUpper()).Contains(string.IsNullOrEmpty(treename) ? "" : treename.ToUpper())
                                  && (string.IsNullOrEmpty(a.treetype) ? " " : a.treetype.ToUpper()).Contains(string.IsNullOrEmpty(treetype) ? "" : treetype.ToUpper())
                                  && (string.IsNullOrEmpty(a.treestatus) ? " " : a.treestatus).Contains(string.IsNullOrEmpty(treestatus) ? "" : treestatus)
                                  && a.record_stat.Contains(string.IsNullOrEmpty(record_stat) ? "" : record_stat)
                                  && a.create_date >= (string.IsNullOrEmpty(fromCreatedDate) ? a.create_date : Utils.ConvertStringtoDatetime(fromCreatedDate, Const.DateTime))
                                  && a.create_date <= (string.IsNullOrEmpty(toCreatedDate) ? a.create_date : Utils.ConvertStringtoDatetime(_toCreatedDate, Const.DateTime))
                                  && (_locationRepository.GetRoleFromToken() == "STAFF" ? _locationByRole.Contains(a.locationid.ToUpper()) : "1" == "1")
                               orderby a.create_date descending
                               select new location_dto
                               {
                                   id = a.id.ToString(),
                                   locationid = a.locationid,
                                   projectid = a.projectid,
                                   projectname = b.projectname,
                                   locationname = a.locationname,
                                   locationinfo = a.locationinfo,
                                   address = a.address,
                                   location = a.location,
                                   locationstatus = (a.locationstatus == "0" ? "Không trồng cây" : a.locationstatus == "1" ? "Đã trồng cây" : ""),
                                   treecode = a.treecode,
                                   treename = a.treename,
                                   treeinfor = a.treeinfor,
                                   treetype = a.treetype,
                                   treestatus = (a.treestatus == "0" ? "Ổn định" : a.treestatus == "1" ? "Khô héo" : a.treestatus == "2" ? "Không phát triển" : a.treestatus == "3" ? "Đổ" : ""),
                                   color = "A",
                                   record_stat = (a.record_stat == "O" ? "Mở" : a.record_stat == "C" ? "Đóng" : "")
                               }).ToListAsync();
            var _dataProject = await (from a in _projectRepository.GetAll()
                                      where a.record_stat == "O"
                                      orderby a.create_date descending
                                      select new project_dto
                                      {
                                          projectid = a.projectid,
                                          projectname = a.projectname
                                      }).ToListAsync();
            res.lstlocations = _data;
            res.lstProject = _dataProject;
            return res;
        }
        public async Task<locationlist_dto> getListLocationHist(string locationid, string projectid, string projectname,
            string locationname, string address, string locationstatus, string treecode,
            string treename, string treetype, string treestatus, string record_stat,
            string fromCreatedDate, string toCreatedDate)
        {
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationHistRepository = _unitOfWork.GetRepository<tb_locations_history>(true);
            locationlist_dto res = new locationlist_dto();

            string _fromCreatedDate = fromCreatedDate + " 00:00:00";
            string _toCreatedDate = toCreatedDate + " 23:59:59";

            var _data = await (from a in _locationHistRepository.GetAll()
                               join b in _projectRepository.GetAll() on a.projectid equals b.projectid
                               where a.locationid.ToUpper() == locationid.ToUpper()
                                  && a.projectid.ToUpper().Contains(string.IsNullOrEmpty(projectid) ? "" : projectid.ToUpper())
                                  && b.projectname.ToUpper().Contains(string.IsNullOrEmpty(projectname) ? "" : projectname.ToUpper())
                                  && a.locationname.ToUpper().Contains(string.IsNullOrEmpty(locationname) ? "" : locationname.ToUpper())
                                  && a.address.ToUpper().Contains(string.IsNullOrEmpty(address) ? "" : address.ToUpper())
                                  && a.locationstatus.Contains(string.IsNullOrEmpty(locationstatus) ? "" : locationstatus)
                                  && (string.IsNullOrEmpty(a.treecode) ? " " : a.treecode.ToUpper()).Contains(string.IsNullOrEmpty(treecode) ? "" : treecode.ToUpper())
                                  && (string.IsNullOrEmpty(a.treename) ? " " : a.treename.ToUpper()).Contains(string.IsNullOrEmpty(treename) ? "" : treename.ToUpper())
                                  && (string.IsNullOrEmpty(a.treetype) ? " " : a.treetype.ToUpper()).Contains(string.IsNullOrEmpty(treetype) ? "" : treetype.ToUpper())
                                  && (string.IsNullOrEmpty(a.treestatus) ? " " : a.treestatus).Contains(string.IsNullOrEmpty(treestatus) ? "" : treestatus)
                                  && a.record_stat.Contains(string.IsNullOrEmpty(record_stat) ? "" : record_stat)
                                  && a.create_date >= (string.IsNullOrEmpty(fromCreatedDate) ? a.create_date : Utils.ConvertStringtoDatetime(fromCreatedDate, Const.DateTime))
                                  && a.create_date <= (string.IsNullOrEmpty(toCreatedDate) ? a.create_date : Utils.ConvertStringtoDatetime(_toCreatedDate, Const.DateTime))
                               orderby a.backupdt descending
                               select new location_dto
                               {
                                   id = a.id.ToString(),
                                   locationid = a.locationid,
                                   projectid = a.projectid,
                                   projectname = b.projectname,
                                   locationname = a.locationname,
                                   locationinfo = a.locationinfo,
                                   address = a.address,
                                   location = a.location,
                                   locationstatus = (a.locationstatus == "0" ? "Không trồng cây" : a.locationstatus == "1" ? "Đã trồng cây" : ""),
                                   treecode = a.treecode,
                                   treename = a.treename,
                                   treeinfor = a.treeinfor,
                                   treetype = a.treetype,
                                   treestatus = (a.treestatus == "0" ? "Ổn định" : a.treestatus == "1" ? "Khô héo" : a.treestatus == "2" ? "Không phát triển" : a.treestatus == "3" ? "Đổ" : ""),
                                   color = "A",
                                   record_stat = (a.record_stat == "O" ? "Mở" : a.record_stat == "C" ? "Đóng" : ""),
                                   created_by = a.create_by,
                                   created_date = (a.create_date == null ? "" : Utils.ConvertDatetimeToString(a.create_date.Value)),
                                   modify_by = a.modify_by,
                                   modify_date = (a.modify_date == null ? "" : Utils.ConvertDatetimeToString(a.modify_date.Value))
                               }).ToListAsync();
            res.lstlocations = _data;
            return res;
        }
        public async Task<locationdetail_dto> getDetailLocation(string locationid)
        {
            locationdetail_dto res = new locationdetail_dto();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);
            var _locationUserRepository = _unitOfWork.GetRepository<tb_location_users>(true);

            var _wardRepository = _unitOfWork.GetRepository<sttm_ward_standard>(true);
            var _districtRepository = _unitOfWork.GetRepository<sttm_district_standard>(true);
            var _provinceRepository = _unitOfWork.GetRepository<sttm_province_standard>(true);

            var _userRepository = _unitOfWork.GetRepository<tb_user>(true);
            var _roleRepository = _unitOfWork.GetRepository<tb_roles>(true);

            var _dataOther = await (from a in _locationRepository.GetAll()
                                    join b in _projectRepository.GetAll() on a.projectid equals b.projectid
                                    where a.locationid != locationid
                                    orderby a.modify_date descending
                                    select new location_dto
                                    {
                                        id = a.id.ToString(),
                                        locationid = a.locationid,
                                        projectid = a.projectid,
                                        projectname = b.projectname,
                                        locationname = a.locationname,
                                        locationinfo = a.locationinfo,
                                        ward_code = a.ward_code,
                                        district_code = a.district_code,
                                        province_code = a.province_code,
                                        address_detail = a.address_detail,
                                        address = a.address,
                                        location = a.location,
                                        locationstatus = a.locationstatus,
                                        treecode = a.treecode,
                                        treename = a.treename,
                                        treeinfor = a.treeinfor,
                                        treetype = a.treetype,
                                        treestatus = a.treestatus,
                                        color = "A",
                                        record_stat = a.record_stat
                                    }).ToListAsync();
            var _dataUser = await (from a in _locationUserRepository.GetAll()
                                   where a.locationid == locationid
                                   select a.userid).ToListAsync();
            var _data = await (from a in _locationRepository.GetAll()
                               join b in _projectRepository.GetAll() on a.projectid equals b.projectid
                               where a.locationid == locationid
                               select new location_dto
                               {
                                   locationid = a.locationid,
                                   projectid = a.projectid,
                                   projectname = b.projectname,
                                   locationname = a.locationname,
                                   locationinfo = a.locationinfo,
                                   ward_code = a.ward_code,
                                   district_code = a.district_code,
                                   province_code = a.province_code,
                                   address_detail = a.address_detail,
                                   address = a.address,
                                   location = a.location,
                                   locationstatus = a.locationstatus,
                                   treecode = a.treecode,
                                   treename = a.treename,
                                   treeinfor = a.treeinfor,
                                   treetype = a.treetype,
                                   treestatus = a.treestatus,
                                   color = "A",
                                   record_stat = a.record_stat,
                                   lstlocations = _dataOther,
                                   lstUser = _dataUser
                               }).FirstOrDefaultAsync();
            var _dataProject = await (from a in _projectRepository.GetAll()
                                      where a.record_stat == "O"
                                      orderby a.create_date descending
                                      select new project_dto
                                      {
                                          projectid = a.projectid,
                                          projectname = a.projectname
                                      }).ToListAsync();
            var _dataWard = await (from a in _wardRepository.GetAll()
                                   select a).ToListAsync();
            var _dataDistrict = await (from a in _districtRepository.GetAll()
                                       select a).ToListAsync();
            var _dataProvince = await (from a in _provinceRepository.GetAll()
                                       select a).ToListAsync();
            var _userdto = await (from a in _userRepository.GetAll()
                                  join b in _roleRepository.GetAll() on a.rolecode equals b.rolecode
                                  where b.rolecode == "STAFF"
                                     && a.record_stat == "O"
                                     && a.status == "O"
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
            res.data = _data;
            res.lstProject = _dataProject;
            res.lstWard = _dataWard;
            res.lstDistrict = _dataDistrict;
            res.lstProvince = _dataProvince;
            res.lstUser = _userdto;
            return res;
        }
        public async Task<locationdetail_dto> getDetailLocationHist(string locationid, string id)
        {
            locationdetail_dto res = new locationdetail_dto();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationHistRepository = _unitOfWork.GetRepository<tb_locations_history>(true);
            var _locationUserHistRepository = _unitOfWork.GetRepository<tb_location_users_history>(true);

            var _wardRepository = _unitOfWork.GetRepository<sttm_ward_standard>(true);
            var _districtRepository = _unitOfWork.GetRepository<sttm_district_standard>(true);
            var _provinceRepository = _unitOfWork.GetRepository<sttm_province_standard>(true);

            var _userRepository = _unitOfWork.GetRepository<tb_user>(true);
            var _roleRepository = _unitOfWork.GetRepository<tb_roles>(true);

            var _dataUser = await (from a in _locationUserHistRepository.GetAll()
                                   where a.locationid == locationid
                                      && a.histid == id
                                   select a.userid).ToListAsync();
            var _data = await (from a in _locationHistRepository.GetAll()
                               join b in _projectRepository.GetAll() on a.projectid equals b.projectid
                               where a.locationid == locationid
                                  && a.id.ToString() == id
                               select new location_dto
                               {
                                   id = a.id.ToString(),
                                   locationid = a.locationid,
                                   projectid = a.projectid,
                                   projectname = b.projectname,
                                   locationname = a.locationname,
                                   locationinfo = a.locationinfo,
                                   ward_code = a.ward_code,
                                   district_code = a.district_code,
                                   province_code = a.province_code,
                                   address_detail = a.address_detail,
                                   address = a.address,
                                   location = a.location,
                                   locationstatus = a.locationstatus,
                                   treecode = a.treecode,
                                   treename = a.treename,
                                   treeinfor = a.treeinfor,
                                   treetype = a.treetype,
                                   treestatus = a.treestatus,
                                   color = "A",
                                   record_stat = a.record_stat,
                                   lstUser = _dataUser
                               }).FirstOrDefaultAsync();

            var _dataProject = await (from a in _projectRepository.GetAll()
                                      where a.record_stat == "O"
                                      orderby a.create_date descending
                                      select new project_dto
                                      {
                                          projectid = a.projectid,
                                          projectname = a.projectname
                                      }).ToListAsync();
            var _dataWard = await (from a in _wardRepository.GetAll()
                                   select a).ToListAsync();
            var _dataDistrict = await (from a in _districtRepository.GetAll()
                                       select a).ToListAsync();
            var _dataProvince = await (from a in _provinceRepository.GetAll()
                                       select a).ToListAsync();
            var _userdto = await (from a in _userRepository.GetAll()
                                  join b in _roleRepository.GetAll() on a.rolecode equals b.rolecode
                                  where b.rolecode == "STAFF"
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
            res.data = _data;
            res.lstProject = _dataProject;
            res.lstWard = _dataWard;
            res.lstDistrict = _dataDistrict;
            res.lstProvince = _dataProvince;
            res.lstUser = _userdto;
            return res;
        }
        public async Task<crud_location_response> createLocation(crud_location_request req)
        {
            crud_location_response res = new crud_location_response();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);
            var _wardRepository = _unitOfWork.GetRepository<sttm_ward_standard>(true);
            var _districtRepository = _unitOfWork.GetRepository<sttm_district_standard>(true);
            var _provinceRepository = _unitOfWork.GetRepository<sttm_province_standard>(true);
            var _locationUserRepository = _unitOfWork.GetRepository<tb_location_users>(true);

            if (string.IsNullOrEmpty(req.projectid)) throw new Exception("Chưa nhập mã dự án!");
            if (string.IsNullOrEmpty(req.locationname)) throw new Exception("Chưa nhập tên vị trí!");
            if (string.IsNullOrEmpty(req.address)) throw new Exception("Chưa nhập địa chỉ!");
            if (req.location_lon == null || req.location_lon == 0 || req.location_lat == null || req.location_lat == 0)
                throw new Exception("Chưa nhập đủ định vị vị trí!");
            if (string.IsNullOrEmpty(req.locationstatus)) throw new Exception("Chưa nhập tình trạng vị trí!");
            if (string.IsNullOrEmpty(req.record_stat)) throw new Exception("Chưa nhập trạng thái vị trí!");
            if (string.IsNullOrEmpty(req.province_code)) throw new Exception("Chưa chọn tỉnh/thành phố!");
            if (string.IsNullOrEmpty(req.district_code)) throw new Exception("Chưa chọn quận/huyện!");
            if (string.IsNullOrEmpty(req.ward_code)) throw new Exception("Chưa chọn phường/xã!");

            var _wardName = _wardRepository.GetFirstOrDefault(o => o.ward_code == req.ward_code).ward_name_value;
            var _districtName = _districtRepository.GetFirstOrDefault(o => o.district_code == req.district_code).district_name_value;
            var _provinceName = _provinceRepository.GetFirstOrDefault(o => o.province_code == req.province_code).province_name_value;

            double _lon = 0;
            double _lat = 0;
            try
            {
                _lon = Convert.ToDouble(req.location_lon);
                _lat = Convert.ToDouble(req.location_lat);
            }
            catch (Exception e)
            {
                throw new Exception("Thông tin vị trí không hợp lệ");
            }
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var _project = await _projectRepository.GetFirstOrDefaultAsync(o => o.projectid == req.projectid);
            if (_project == null) throw new Exception("Mã dự án không tồn tại, vui lòng kiểm tra lại!");

            var result = new NpgsqlParameter("", System.Data.SqlDbType.NVarChar);
            result.Direction = System.Data.ParameterDirection.Output;
            string locationid = "";
            await _locationRepository.ExecuteSQL("SELECT lpad(CAST(nextval('seq_location_id') AS VARCHAR), 4, '0')", result);
            locationid = "LOC" + DateTime.Now.ToString("yyyyMMdd") + (string)result.Value;

            tb_locations _data = new tb_locations();
            _data.locationid = locationid;
            _data.projectid = req.projectid;
            _data.locationname = req.locationname;
            _data.locationinfo = req.locationinfo;
            _data.province_code = req.province_code;
            _data.district_code = req.district_code;
            _data.ward_code = req.ward_code;
            _data.address_detail = req.address_detail;
            _data.address = Utils.buildAddress(req.address_detail, _wardName, _districtName, _provinceName);
            _data.location = geometryFactory.CreatePoint(new Coordinate(_lon, _lat));
            _data.locationstatus = req.locationstatus;
            _data.treecode = req.treecode;
            _data.treename = req.treename;
            _data.treeinfor = req.treeinfor;
            _data.treetype = req.treetype;
            _data.treestatus = req.treestatus;
            _data.record_stat = req.record_stat;
            _data.create_by = _projectRepository.GetUserFromToken();
            _data.create_date = DateTime.Now;
            await _locationRepository.AddAsync(_data);

            if (req.lstUserid != null && req.lstUserid.Count() > 0)
            {
                List<tb_location_users> lstLocationUser = new List<tb_location_users>();
                foreach (var item in req.lstUserid)
                {
                    tb_location_users itemInsert = new tb_location_users();
                    itemInsert.locationid = locationid;
                    itemInsert.userid = item;
                    itemInsert.create_by = _projectRepository.GetUserFromToken();
                    itemInsert.create_date = DateTime.Now;
                    itemInsert.record_stat = "O";
                    itemInsert.mod_no = 0;
                    lstLocationUser.Add(itemInsert);
                }
                await _locationUserRepository.AddRangeAsync(lstLocationUser);
            }

            res.resDesc = "Tạo mới thành công!";
            return res;
        }
        public async Task<crud_location_response> updateLocation(crud_location_request req)
        {
            crud_location_response res = new crud_location_response();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);
            var _locationHistRepository = _unitOfWork.GetRepository<tb_locations_history>(true);
            var _wardRepository = _unitOfWork.GetRepository<sttm_ward_standard>(true);
            var _districtRepository = _unitOfWork.GetRepository<sttm_district_standard>(true);
            var _provinceRepository = _unitOfWork.GetRepository<sttm_province_standard>(true);
            var _locationUserRepository = _unitOfWork.GetRepository<tb_location_users>(true);
            var _locationUserHistRepository = _unitOfWork.GetRepository<tb_location_users_history>(true);

            if (string.IsNullOrEmpty(req.projectid)) throw new Exception("Chưa nhập mã dự án!");
            if (string.IsNullOrEmpty(req.locationname)) throw new Exception("Chưa nhập tên vị trí!");
            if (string.IsNullOrEmpty(req.address)) throw new Exception("Chưa nhập địa chỉ!");
            if (req.location_lon == null || req.location_lon == 0 || req.location_lat == null || req.location_lat == 0)
                throw new Exception("Chưa nhập đủ định vị vị trí!");
            if (string.IsNullOrEmpty(req.locationstatus)) throw new Exception("Chưa nhập trạng thái vị trí!");
            if (string.IsNullOrEmpty(req.record_stat)) throw new Exception("Chưa nhập trạng thái vị trí!");
            if (string.IsNullOrEmpty(req.province_code)) throw new Exception("Chưa chọn tỉnh/thành phố!");
            if (string.IsNullOrEmpty(req.district_code)) throw new Exception("Chưa chọn quận/huyện!");
            if (string.IsNullOrEmpty(req.ward_code)) throw new Exception("Chưa chọn phường/xã!");

            var _wardName = _wardRepository.GetFirstOrDefault(o => o.ward_code == req.ward_code).ward_name_value;
            var _districtName = _districtRepository.GetFirstOrDefault(o => o.district_code == req.district_code).district_name_value;
            var _provinceName = _provinceRepository.GetFirstOrDefault(o => o.province_code == req.province_code).province_name_value;
            var _locationUser = await _locationUserRepository.GetAsync(o => o.locationid == req.locationid);

            double _lon = 0;
            double _lat = 0;
            try
            {
                _lon = Convert.ToDouble(req.location_lon);
                _lat = Convert.ToDouble(req.location_lat);
            }
            catch (Exception e)
            {
                throw new Exception("Thông tin vị trí không hợp lệ");
            }
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var _project = await _projectRepository.GetFirstOrDefaultAsync(o => o.projectid == req.projectid);
            if (_project == null) throw new Exception("Mã dự án không tồn tại, vui lòng kiểm tra lại!");
            var _data = await _locationRepository.GetFirstOrDefaultAsync(o => o.locationid == req.locationid);
            if (_data == null) throw new Exception("Mã vị trí không tồn tại, vui lòng kiểm tra lại!");

            tb_locations_history _hist = new tb_locations_history();
            _hist.locationid = _data.locationid;
            _hist.projectid = _data.projectid;
            _hist.locationname = _data.locationname;
            _hist.locationinfo = _data.locationinfo;
            _hist.province_code = _data.province_code;
            _hist.district_code = _data.district_code;
            _hist.ward_code = _data.ward_code;
            _hist.address_detail = _data.address_detail;
            _hist.address = _data.address;
            _hist.location = _data.location;
            _hist.locationstatus = _data.locationstatus;
            _hist.treecode = _data.treecode;
            _hist.treename = _data.treename;
            _hist.treeinfor = _data.treeinfor;
            _hist.treetype = _data.treetype;
            _hist.treestatus = _data.treestatus;
            _hist.record_stat = _data.record_stat;
            _hist.create_by = _data.create_by;
            _hist.create_date = _data.create_date;
            _hist.mod_no = _data.mod_no;
            _hist.modify_by = _data.modify_by;
            _hist.modify_date = _data.modify_date;
            _hist.backupdt = DateTime.Now;
            await _locationHistRepository.AddAsync(_hist);

            _data.projectid = req.projectid;
            _data.locationname = req.locationname;
            _data.locationinfo = req.locationinfo;
            _data.province_code = req.province_code;
            _data.district_code = req.district_code;
            _data.ward_code = req.ward_code;
            _data.address_detail = req.address_detail;
            _data.address = Utils.buildAddress(req.address_detail, _wardName, _districtName, _provinceName);
            _data.location = geometryFactory.CreatePoint(new Coordinate(_lon, _lat));
            _data.locationstatus = req.locationstatus;
            _data.treecode = req.treecode;
            _data.treename = req.treename;
            _data.treeinfor = req.treeinfor;
            _data.treetype = req.treetype;
            _data.treestatus = req.treestatus;
            _data.record_stat = req.record_stat;
            _data.modify_by = _projectRepository.GetUserFromToken();
            _data.modify_date = DateTime.Now;
            await _locationRepository.UpdateAsync(_data);

            if (_locationUser != null && _locationUser.Count() > 0)
            {
                List<tb_location_users_history> userHist = new List<tb_location_users_history>();
                foreach (var item in _locationUser)
                {
                    tb_location_users_history itemBk = new tb_location_users_history();
                    itemBk.locationid = item.locationid;
                    itemBk.userid = item.userid;
                    itemBk.create_by = item.create_by;
                    itemBk.create_date = item.create_date;
                    itemBk.modify_by = item.modify_by;
                    itemBk.modify_date = item.modify_date;
                    itemBk.record_stat = "O";
                    itemBk.mod_no = 0;
                    itemBk.histid = _hist.id.ToString();
                    itemBk.backupdt = DateTime.Now;
                    userHist.Add(itemBk);
                }
                await _locationUserHistRepository.AddRangeAsync(userHist);
                await _locationUserRepository.DeleteRange(_locationUser);
            }
            if (req.lstUserid != null && req.lstUserid.Count() > 0)
            {
                List<tb_location_users> lstLocationUser = new List<tb_location_users>();
                foreach (var item in req.lstUserid)
                {
                    tb_location_users itemInsert = new tb_location_users();
                    itemInsert.locationid = req.locationid;
                    itemInsert.userid = item;
                    itemInsert.create_by = _projectRepository.GetUserFromToken();
                    itemInsert.create_date = DateTime.Now;
                    itemInsert.record_stat = "O";
                    itemInsert.mod_no = 0;
                    lstLocationUser.Add(itemInsert);
                }
                await _locationUserRepository.AddRangeAsync(lstLocationUser);
            }

            res.resDesc = "Cập nhật thành công!";
            return res;
        }
        public async Task<crud_location_response> deletelocation(crud_location_request req)
        {
            crud_location_response res = new crud_location_response();
            if (string.IsNullOrEmpty(req.locationid)) throw new Exception("Chưa chọn mã vị trí!");
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);
            var _locationHistRepository = _unitOfWork.GetRepository<tb_locations_history>(true);
            var _locationUserRepository = _unitOfWork.GetRepository<tb_location_users>(true);
            var _locationUserHistRepository = _unitOfWork.GetRepository<tb_location_users_history>(true);

            var _location = await _locationRepository.Get(o => o.locationid == req.locationid);
            if (_location.Count() > 0)
            {
                await _locationRepository.DeleteRange(_location);
                var _locationHist = await _locationHistRepository.Get(o => _location.Select(i => i.locationid).Contains(o.locationid));
                if (_locationHist.Count() > 0)
                {
                    await _locationHistRepository.DeleteRange(_locationHist);
                }
                var _locationUser = await _locationUserRepository.Get(o => _location.Select(i => i.locationid).Contains(o.locationid));
                if (_locationUser.Count() > 0)
                {
                    await _locationUserRepository.DeleteRange(_locationUser);
                }
                var _locationUserHist = await _locationUserHistRepository.Get(o => _location.Select(i => i.locationid).Contains(o.locationid));
                if (_locationUserHist.Count() > 0)
                {
                    await _locationUserHistRepository.DeleteRange(_locationUserHist);
                }
            } else
            {
                throw new Exception("Mã vị trí không tồn tại, vui lòng kiểm tra lại!");
            }
            res.resDesc = "Xóa vị trí thành công!";
            return res;
        }
    }
}
