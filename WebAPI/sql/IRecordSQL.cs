using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.dto;

namespace WebAPI.sql {
	public interface IRecordSQL {

		List<Record> GetAll();
	}
}
