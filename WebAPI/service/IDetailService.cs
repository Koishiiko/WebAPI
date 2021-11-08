using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.service {
	public interface IDetailService {
		DetailPageDTO GetPageByGuid(DetailPagination pagination);
	}
}
