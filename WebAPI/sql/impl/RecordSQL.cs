using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class RecordSQL : IRecordSQL {

        public List<Record> GetAll() {
            return DataSource.DB.Queryable<Record>().ToList();
        }
    }
}
