using map.backend.shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Interfaces.Map
{
    public interface IProjectRepository
    {
        Task<projectlist_dto> getListProjects(string projectId, string projectName,
            string investor, string contractors, string total_value,
            string fromOpen, string toOpen,
            string fromEnd, string toEnd,
            string fromReceipt, string toReceipt);
        Task<project_dto> getDetailProjects(string projectId);
        Task<crud_project_response> createProject(crud_project_request req);
        Task<crud_project_response> updateProject(crud_project_request req);
    }
}
