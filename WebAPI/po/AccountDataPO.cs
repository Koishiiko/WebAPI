using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.po {
    public class AccountDataPO {

        public int Id { get; set; }
        public string AccountKey { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
