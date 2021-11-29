using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;

namespace WebAPI.service {
    public interface IRecordService {

        List<Record> GetAll();
    }
}
