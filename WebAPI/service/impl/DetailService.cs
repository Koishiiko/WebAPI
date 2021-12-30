using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.sql;

namespace WebAPI.service.impl {
	public class DetailService : IDetailService {
		
		private readonly IDetailSQL detailSQL;

		public DetailService(IDetailSQL detailSQL) {
			this.detailSQL = detailSQL;
		}

		public DetailPageDTO GetPageByGuid(DetailPagination pagination) {
			return new DetailPageDTO() {
				Data = detailSQL.GetPageByGuid(pagination, out int total),
				Total = total
			};
		}
	}
}
