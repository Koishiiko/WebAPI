using WebAPI.dto;
using WebAPI.pagination;
using System.Collections.Generic;


namespace WebAPI.service {
    public interface IDetailService {

        IEnumerable<DetailDTO> GetRecordDetail(string guid);
    }
}
