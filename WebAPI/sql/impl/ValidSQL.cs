using System.Collections.Generic;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ValidSQL : IValidSQL {

        public List<Valid> GetByGuid(string guid) {
            return DataSource.DB.Queryable<Valid>().Where(v => v.Guid == guid).ToList();
        }

        public long Save(Valid valid) {
            return DataSource.Save(valid);
        }
    }
}
