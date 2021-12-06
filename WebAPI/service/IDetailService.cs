using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.service {
    public interface IDetailService {

		DetailPageDTO GetPageByGuid(DetailPagination pagination);
	}
}
