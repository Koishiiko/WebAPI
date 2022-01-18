using WebAPI.dto;
using System.Collections.Generic;


namespace WebAPI.service {
    public interface IDetailService {

        IEnumerable<DetailDTO> GetByProductId(string productId);

        IEnumerable<DetailDTO> GetByGuid(string guid);
    }
}
