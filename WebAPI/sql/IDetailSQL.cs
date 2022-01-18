using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.po;
using WebAPI.dto;
using WebAPI.pagination;

namespace WebAPI.sql {
	public interface IDetailSQL {

		List<Detail> GetByGuid(string guid);

		List<DetailPO> GetDataByGuid(string guid);

		long Save(Detail detail);
	}
}
