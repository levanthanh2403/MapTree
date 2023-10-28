using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using map.backend.shared.DTO;
using map.backend.shared.Entities.Map;
using map.backend.shared.Interfaces.Report;
using map.backend.shared.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using NetTopologySuite.Geometries;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Repositories.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReportRepository(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }
        public async Task<rpt_param_dto> getParams()
        {
            rpt_param_dto res = new rpt_param_dto();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _wardRepository = _unitOfWork.GetRepository<sttm_ward_standard>(true);
            var _districtRepository = _unitOfWork.GetRepository<sttm_district_standard>(true);
            var _provinceRepository = _unitOfWork.GetRepository<sttm_province_standard>(true);

            List<tb_projects> lstProject = new List<tb_projects>();
            List<sttm_ward_standard> lstWard = new List<sttm_ward_standard>();
            List<sttm_district_standard> lstDistrict = new List<sttm_district_standard>();
            List<sttm_province_standard> lstProvince = new List<sttm_province_standard>();

            lstProject = await (from a in _projectRepository.GetAll()
                                select a).ToListAsync();
            lstWard = await (from a in _wardRepository.GetAll()
                             select a).ToListAsync();
            lstDistrict = await (from a in _districtRepository.GetAll()
                                 select a).ToListAsync();
            lstProvince = await (from a in _provinceRepository.GetAll()
                                 select a).ToListAsync();

            res.lstProject = lstProject;
            res.lstWard = lstWard;
            res.lstDistrict = lstDistrict;
            res.lstProvince = lstProvince;
            return res;
        }
        public async Task<rpt_dto> exportReportLocation(string projectId, string wardCode, string districtCode, string provinceCode)
        {
            rpt_dto response = new rpt_dto();
            var _projectRepository = _unitOfWork.GetRepository<tb_projects>(true);
            var _locationRepository = _unitOfWork.GetRepository<tb_locations>(true);
            var _wardRepository = _unitOfWork.GetRepository<sttm_ward_standard>(true);
            var _districtRepository = _unitOfWork.GetRepository<sttm_district_standard>(true);
            var _provinceRepository = _unitOfWork.GetRepository<sttm_province_standard>(true);

            List<rpt_project_dto> data = new List<rpt_project_dto>();

            data = await (from a in _locationRepository.GetAll()
                          join b in _projectRepository.GetAll() on a.projectid equals b.projectid
                          join c in _provinceRepository.GetAll() on a.province_code equals c.province_code
                          join d in _districtRepository.GetAll() on a.district_code equals d.district_code
                          join e in _wardRepository.GetAll() on a.ward_code equals e.ward_code
                          where b.projectid.ToUpper() == (string.IsNullOrEmpty(projectId) ? b.projectid.ToUpper() : projectId.ToUpper())
                             && c.province_code.ToUpper() == (string.IsNullOrEmpty(provinceCode) ? c.province_code : provinceCode.ToUpper())
                             && d.district_code.ToUpper() == (string.IsNullOrEmpty(districtCode) ? d.district_code : districtCode.ToUpper())
                             && e.ward_code.ToUpper() == (string.IsNullOrEmpty(wardCode) ? e.ward_code : wardCode.ToUpper())
                          orderby a.projectid, a.province_code, a.district_code, a.ward_code ascending
                          select new rpt_project_dto
                          {
                              projectid = a.projectid,
                              projectname = b.projectname,
                              province_name = c.province_name,
                              province_name_value = c.province_name_value,
                              district_name = d.district_name,
                              district_name_value = d.district_name_value,
                              ward_name = e.ward_name,
                              ward_name_value = e.ward_name_value,
                              address = a.address,
                              locationid = a.locationid,
                              locationname = a.locationname,
                              locationstatus = (a.locationstatus == "0" ? "Không trồng cây" : a.locationstatus == "1" ? "Đã trồng cây" : ""),
                              treename = a.treename,
                              treeinfor = a.treeinfor,
                              treestatus = (a.treestatus == "0" ? "Ổn định" : a.treestatus == "1" ? "Khô héo" : a.treestatus == "2" ? "Không phát triển" : a.treestatus == "3" ? "Đổ" : "")
                          }).ToListAsync();
            if (data.Count() == 0)
                throw new Exception("Không có dữ liệu!");
            string templatePath = _hostingEnvironment.ContentRootPath + "Template\\location_report_template.xlsx";
            //wb.Open(templatePath);
            //wb.AddDataSource("Data", data);
            //wb.ProcessTemplate();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("data");
                worksheet.Style.Font.SetBold();

                var currentRow = 1;
                worksheet.Range("A1:K1").Style.Fill.SetBackgroundColor(XLColor.BabyBlueEyes);
                worksheet.Range("A1:K1").SetAutoFilter();
                worksheet.ColumnWidth = 20;

                worksheet.Cell(currentRow, 1).Value = "Dự án";
                worksheet.Cell(currentRow, 2).Value = "Tỉnh/Thành phố";
                worksheet.Cell(currentRow, 3).Value = "Quận/Huyện";
                worksheet.Cell(currentRow, 4).Value = "Phường/Xã";
                worksheet.Cell(currentRow, 5).Value = "Địa chỉ";
                worksheet.Cell(currentRow, 6).Value = "Mã vị trí";
                worksheet.Cell(currentRow, 7).Value = "Tên vị trí";
                worksheet.Cell(currentRow, 8).Value = "Trạng thái";
                worksheet.Cell(currentRow, 9).Value = "Tên cây trồng";
                worksheet.Cell(currentRow, 10).Value = "Thông tin cây trồng";
                worksheet.Cell(currentRow, 11).Value = "Trạng thái cây trồng";
                foreach (var _item in data)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = _item.projectid;
                    worksheet.Cell(currentRow, 2).Value = _item.province_name;
                    worksheet.Cell(currentRow, 3).Value = _item.district_name;
                    worksheet.Cell(currentRow, 4).Value = _item.ward_name;
                    worksheet.Cell(currentRow, 5).Value = _item.address;
                    worksheet.Cell(currentRow, 6).Value = _item.locationid;
                    worksheet.Cell(currentRow, 7).Value = _item.locationname;
                    worksheet.Cell(currentRow, 8).Value = _item.locationstatus;
                    worksheet.Cell(currentRow, 9).Value = _item.treename;
                    worksheet.Cell(currentRow, 10).Value = _item.treeinfor;
                    worksheet.Cell(currentRow, 11).Value = _item.treestatus;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    string base64 = Convert.ToBase64String(content);
                    response.fileBase64 = base64;
                }
            }

            return response;
        }
    }
}
