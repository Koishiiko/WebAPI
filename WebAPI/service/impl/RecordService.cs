using System.Collections.Generic;
using WebAPI.sql;
using WebAPI.entity;

namespace WebAPI.service.impl {
    public class RecordService : IRecordService {

        private readonly IRecordSQL recordSQL;

        public RecordService(IRecordSQL recordSQL) {
            this.recordSQL = recordSQL;
        }

        public List<Record> GetAll() {
            return recordSQL.GetAll();
        }

        public Record GetById(int id) {
            return recordSQL.GetByRecordId(id);
        }
    }
}
