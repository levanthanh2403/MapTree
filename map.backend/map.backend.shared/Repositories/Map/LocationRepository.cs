using map.backend.shared.DTO;
using map.backend.shared.Entities.Map;
using map.backend.shared.Helper;
using map.backend.shared.Interfaces.Map;
using map.backend.shared.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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
            locationlist_dto res = new locationlist_dto();

            string _fromCreatedDate = fromCreatedDate + " 00:00:00";
            string _toCreatedDate = toCreatedDate + " 23:59:59";

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
                               orderby a.create_date descending
                               select new location_dto
                               {
                                   locationid = a.locationid,
                                   projectid = a.projectid,
                                   projectname = b.projectname,
                                   locationname = a.locationname,
                                   locationinfo = a.locationinfo,
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
            var _locationRepository = _unitOfWork.GetRepository<tb_locations_history>(true);
            locationlist_dto res = new locationlist_dto();

            string _fromCreatedDate = fromCreatedDate + " 00:00:00";
            string _toCreatedDate = toCreatedDate + " 23:59:59";

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
                                  && a.create_date <= (string.IsNullOrEmpty(_toCreatedDate) ? a.create_date : Utils.ConvertStringtoDatetime(_toCreatedDate, Const.DateTime))
                               orderby a.modify_date descending
                               select new location_dto
                               {
                                   locationid = a.locationid,
                                   projectid = a.projectid,
                                   projectname = b.projectname,
                                   locationname = a.locationname,
                                   locationinfo = a.locationinfo,
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
            res.lstlocations = _data;
            return res;
        }
        public async Task<locationdetail_dto> getDetailLocation(string locationid)
        {
            locationdetail_dto res = new locationdetail_dto();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);

            var _dataOther = await (from a in _locationRepository.GetAll()
                                    join b in _projectRepository.GetAll() on a.projectid equals b.projectid
                                    where a.locationid != locationid
                                    orderby a.modify_date descending
                                    select new location_dto
                                    {
                                        locationid = a.locationid,
                                        projectid = a.projectid,
                                        projectname = b.projectname,
                                        locationname = a.locationname,
                                        locationinfo = a.locationinfo,
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
                                   lstlocations = _dataOther
                               }).FirstOrDefaultAsync();
            var _dataProject = await (from a in _projectRepository.GetAll()
                                      where a.record_stat == "O"
                                      orderby a.create_date descending
                                      select new project_dto
                                      {
                                          projectid = a.projectid,
                                          projectname = a.projectname
                                      }).ToListAsync();
            res.data = _data;
            res.lstProject = _dataProject;
            return res;
        }
        public async Task<crud_location_response> createLocation(crud_location_request req)
        {
            crud_location_response res = new crud_location_response();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);

            if (string.IsNullOrEmpty(req.projectid)) throw new Exception("Chưa nhập mã dự án!");
            if (string.IsNullOrEmpty(req.locationname)) throw new Exception("Chưa nhập tên vị trí!");
            if (string.IsNullOrEmpty(req.address)) throw new Exception("Chưa nhập địa chỉ!");
            if (string.IsNullOrEmpty(req.location)) throw new Exception("Chưa nhập đủ định vị vị trí!");
            if (string.IsNullOrEmpty(req.locationstatus)) throw new Exception("Chưa nhập tình trạng vị trí!");
            if (string.IsNullOrEmpty(req.record_stat)) throw new Exception("Chưa nhập trạng thái vị trí!");

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
            _data.address = req.address;
            _data.location = JsonConvert.DeserializeObject<Geometry>(req.location);
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
            res.resDesc = "Tạo mới thành công!";
            return res;
        }
        public async Task<crud_location_response> updateLocation(crud_location_request req)
        {
            crud_location_response res = new crud_location_response();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);

            if (string.IsNullOrEmpty(req.projectid)) throw new Exception("Chưa nhập mã dự án!");
            if (string.IsNullOrEmpty(req.locationname)) throw new Exception("Chưa nhập tên vị trí!");
            if (string.IsNullOrEmpty(req.address)) throw new Exception("Chưa nhập địa chỉ!");
            if (string.IsNullOrEmpty(req.location)) throw new Exception("Chưa nhập đủ định vị vị trí!");
            if (string.IsNullOrEmpty(req.locationstatus)) throw new Exception("Chưa nhập trạng thái vị trí!");
            if (string.IsNullOrEmpty(req.record_stat)) throw new Exception("Chưa nhập trạng thái vị trí!");


            var _project = await _projectRepository.GetFirstOrDefaultAsync(o => o.projectid == req.projectid);
            if (_project == null) throw new Exception("Mã dự án không tồn tại, vui lòng kiểm tra lại!");
            var _data = await _locationRepository.GetFirstOrDefaultAsync(o => o.locationid.ToUpper() == req.locationid.ToUpper());
            if (_data == null) throw new Exception("Mã vị trí không tồn tại, vui lòng kiểm tra lại!");
            _data.projectid = req.projectid;
            _data.locationname = req.locationname;
            _data.locationinfo = req.locationinfo;
            _data.address = req.address;
            _data.location = JsonConvert.DeserializeObject<Geometry>(req.location);
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
            res.resDesc = "Cập nhật thành công!";
            return res;
        }
    }
}
