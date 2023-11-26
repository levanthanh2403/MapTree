using map.backend.shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Interfaces.Map
{
    public interface ILocationRepository
    {
        Task<locationlist_dto> getListLocation(string locationid, string projectid, string projectname,
            string locationname, string address, string locationstatus, string treecode,
            string treename, string treetype, string treestatus, string record_stat,
            string fromCreatedDate, string toCreatedDate);
        Task<locationlist_dto> getListLocationHist(string locationid, string projectid, string projectname,
            string locationname, string address, string locationstatus, string treecode,
            string treename, string treetype, string treestatus, string record_stat,
            string fromCreatedDate, string toCreatedDate);
        Task<locationdetail_dto> getDetailLocation(string locationid);
        Task<locationdetail_dto> getDetailLocationHist(string locationid, string id);
        Task<crud_location_response> createLocation(crud_location_request req);
        Task<crud_location_response> updateLocation(crud_location_request req);
    }
}
