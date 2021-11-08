using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.sql {
	interface IDetailSQL {

		List<Detail> GetByGuid(string guid);

		List<DetailDTO> GetPageByGuid(DetailPagination pagination);

		int GetCount(DetailPagination pagination);

		List<DetailTemplateDTO> GetTemplates(string productId);

		long Save(Detail detail);
	}
}
