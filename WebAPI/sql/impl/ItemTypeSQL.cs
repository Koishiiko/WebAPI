using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.utils;

namespace WebAPI.sql.impl {
    public class ItemTypeSQL : IItemTypeSQL {

        public IEnumerable<ItemType> GetAll() {
            return DataSource.Switch.Queryable<ItemType>().ToList();
        }
    }
}
