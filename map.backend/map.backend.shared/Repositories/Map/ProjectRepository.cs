using map.backend.shared.Interfaces.Map;
using map.backend.shared.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using map.backend.shared.DTO;
using Org.BouncyCastle.Asn1.Ocsp;
using map.backend.shared.Entities.Auth;
using map.backend.shared.Entities.Map;
using map.backend.shared.Helper;

namespace map.backend.shared.Repositories.Map
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<projectlist_dto> getListProjects(string projectId, string projectName,
            string investor, string contractors, string total_value,
            string fromOpen, string toOpen,
            string fromEnd, string toEnd,
            string fromReceipt, string toReceipt)
        {
            projectlist_dto res = new projectlist_dto();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);

            string _fromOpen = fromOpen + " 00:00:00";
            string _toOpen = toOpen + " 23:59:59";
            string _fromEnd = fromEnd + " 00:00:00";
            string _toEnd = toEnd + " 23:59:59";
            string _fromReceipt = fromReceipt + " 00:00:00";
            string _toReceipt = toReceipt + " 23:59:59";

            var _data = await (from a in _projectRepository.GetAll()
                               where a.record_stat == "O"
                                  && a.projectid.Contains(string.IsNullOrEmpty(projectId) ? "" : projectId)
                                  && a.projectname.Contains(string.IsNullOrEmpty(projectName) ? "" : projectName)
                                  && a.investor.Contains(string.IsNullOrEmpty(investor) ? "" : investor)
                                  && a.contractors.Contains(string.IsNullOrEmpty(contractors) ? "" : contractors)
                                  && a.total_value.Contains(string.IsNullOrEmpty(total_value) ? "" : total_value)
                                  && a.opendate >= (string.IsNullOrEmpty(fromOpen) ? a.opendate : Utils.ConvertStringtoDatetime(_fromOpen, Const.DateTime))
                                  && a.opendate <= (string.IsNullOrEmpty(toOpen) ? a.opendate : Utils.ConvertStringtoDatetime(_toOpen, Const.DateTime))
                                  && a.enddate >= (string.IsNullOrEmpty(fromEnd) ? a.enddate : Utils.ConvertStringtoDatetime(_fromEnd, Const.DateTime))
                                  && a.enddate <= (string.IsNullOrEmpty(toEnd) ? a.enddate : Utils.ConvertStringtoDatetime(_toEnd, Const.DateTime))
                                  && a.receiptdate >= (string.IsNullOrEmpty(fromReceipt) ? a.receiptdate : Utils.ConvertStringtoDatetime(_fromReceipt, Const.DateTime))
                                  && a.receiptdate <= (string.IsNullOrEmpty(toReceipt) ? a.receiptdate : Utils.ConvertStringtoDatetime(_toReceipt, Const.DateTime))
                               orderby a.create_date descending
                               select new project_dto
                               {
                                   projectid = a.projectid,
                                   projectname = a.projectname,
                                   projectdesc = a.projectdesc,
                                   projectdetail = a.projectdetail,
                                   investor = a.investor,
                                   contractors = a.contractors,
                                   total_value = a.total_value,
                                   opendate = Utils.ConvertDatetimeToString(a.opendate),
                                   receiptdate = Utils.ConvertDatetimeToString(a.receiptdate),
                                   enddate = Utils.ConvertDatetimeToString(a.enddate),
                                   create_by = a.create_by,
                                   create_date = (a.create_date == null ? "" : Utils.ConvertDatetimeToString(a.create_date.Value)),
                                   modify_by = a.modify_by,
                                   modify_date = (a.modify_date == null ? "" : Utils.ConvertDatetimeToString(a.modify_date.Value)),
                                   img = a.img
                               }).ToListAsync();
            res.projects = _data;
            return res;
        }
        public async Task<project_dto> getDetailProjects(string projectId)
        {
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _data = await (from a in _projectRepository.GetAll()
                               where a.record_stat == "O"
                                  && a.projectid == projectId
                               select new project_dto
                               {
                                   projectid = a.projectid,
                                   projectname = a.projectname,
                                   projectdesc = a.projectdesc,
                                   projectdetail = a.projectdetail,
                                   investor = a.investor,
                                   contractors = a.contractors,
                                   total_value = a.total_value,
                                   opendate = Utils.ConvertDatetimeToString(a.opendate),
                                   receiptdate = Utils.ConvertDatetimeToString(a.receiptdate),
                                   enddate = Utils.ConvertDatetimeToString(a.enddate),
                                   create_by = a.create_by,
                                   create_date = (a.create_date == null ? "" : Utils.ConvertDatetimeToString(a.create_date.Value)),
                                   modify_by = a.modify_by,
                                   modify_date = (a.modify_date == null ? "" : Utils.ConvertDatetimeToString(a.modify_date.Value)),
                                   img = a.img
                               }).FirstOrDefaultAsync();
            if (_data == null) throw new Exception("Không tìm thấy dự án!");
            return _data;
        }
        public async Task<crud_project_response> createProject(crud_project_request req)
        {
            crud_project_response res = new crud_project_response();
            if (string.IsNullOrEmpty(req.projectid)) throw new Exception("Chưa nhập mã dự án!");
            if (string.IsNullOrEmpty(req.projectname)) throw new Exception("Chưa nhập tên dự án!");
            if (string.IsNullOrEmpty(req.projectdesc)) throw new Exception("Chưa nhập mô tả dự án!");
            if (string.IsNullOrEmpty(req.investor)) throw new Exception("Chưa nhập chủ đầu tư!");
            if (string.IsNullOrEmpty(req.contractors)) throw new Exception("Chưa nhập nhà thầu!");
            if (string.IsNullOrEmpty(req.total_value)) throw new Exception("Chưa nhập tổng giá trị dự án!");
            if (string.IsNullOrEmpty(req.img)) throw new Exception("Chưa chọn ảnh dự án!");
            if (string.IsNullOrEmpty(req.opendate)) throw new Exception("Chưa chọn ngày mở thầu!");
            if (string.IsNullOrEmpty(req.receiptdate)) throw new Exception("Chưa chọn ngày đấu thầu!");
            if (string.IsNullOrEmpty(req.enddate)) throw new Exception("Chưa chọn ngày kết thúc dự án!");

            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _project = await _projectRepository.GetFirstOrDefaultAsync(o => o.projectid == req.projectid);
            if (_project != null) throw new Exception("Mã dự án đã tồn tại, vui lòng kiểm tra lại!");

            tb_projects _data = new tb_projects();
            _data.projectid = req.projectid;
            _data.projectname = req.projectname;
            _data.projectdesc = req.projectdesc;
            _data.projectdetail = req.projectdetail;
            _data.investor = req.investor;
            _data.contractors = req.contractors;
            _data.total_value = req.total_value;
            _data.opendate = Utils.ConvertStringtoDatetime(req.opendate, Const.Date);
            _data.receiptdate = (string.IsNullOrEmpty(req.receiptdate) ? _data.receiptdate : Utils.ConvertStringtoDatetime(req.receiptdate, Const.Date));
            _data.enddate = Utils.ConvertStringtoDatetime(req.enddate, Const.Date);
            _data.record_stat = "O";
            _data.create_by = _projectRepository.GetUserFromToken();
            _data.create_date = DateTime.Now;
            _data.img = req.img;
            await _projectRepository.AddAsync(_data);
            res.resDesc = "Tạo mới thành công!";
            return res;
        }
        public async Task<crud_project_response> updateProject(crud_project_request req)
        {
            crud_project_response res = new crud_project_response();
            if (string.IsNullOrEmpty(req.projectid)) throw new Exception("Chưa nhập mã dự án!");
            if (string.IsNullOrEmpty(req.projectname)) throw new Exception("Chưa nhập tên dự án!");
            if (string.IsNullOrEmpty(req.projectdesc)) throw new Exception("Chưa nhập mô tả dự án!");
            if (string.IsNullOrEmpty(req.investor)) throw new Exception("Chưa nhập chủ đầu tư!");
            if (string.IsNullOrEmpty(req.contractors)) throw new Exception("Chưa nhập nhà thầu!");
            if (string.IsNullOrEmpty(req.total_value)) throw new Exception("Chưa nhập tổng giá trị dự án!");
            if (string.IsNullOrEmpty(req.img)) throw new Exception("Chưa chọn ảnh dự án!");
            if (string.IsNullOrEmpty(req.opendate)) throw new Exception("Chưa chọn ngày mở thầu!");
            if (string.IsNullOrEmpty(req.receiptdate)) throw new Exception("Chưa chọn ngày đấu thầu!");
            if (string.IsNullOrEmpty(req.enddate)) throw new Exception("Chưa chọn ngày kết thúc dự án!");

            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _data = await _projectRepository.GetFirstOrDefaultAsync(o => o.projectid == req.projectid);
            if (_data == null) throw new Exception("Mã dự án không tồn tại, vui lòng kiểm tra lại!");

            _data.projectname = req.projectname;
            _data.projectdesc = req.projectdesc;
            _data.projectdetail = req.projectdetail;
            _data.investor = req.investor;
            _data.contractors = req.contractors;
            _data.total_value = req.total_value;
            _data.opendate = Utils.ConvertStringtoDatetime(req.opendate, Const.Date);
            _data.receiptdate = (string.IsNullOrEmpty(req.receiptdate) ? _data.receiptdate : Utils.ConvertStringtoDatetime(req.receiptdate, Const.Date));
            _data.enddate = Utils.ConvertStringtoDatetime(req.enddate, Const.Date);
            _data.modify_by = _projectRepository.GetUserFromToken();
            _data.modify_date = DateTime.Now;
            _data.img = req.img;
            await _projectRepository.UpdateAsync(_data);
            res.resDesc = "Cập nhật thành công!";
            return res;
        }
    }
}
