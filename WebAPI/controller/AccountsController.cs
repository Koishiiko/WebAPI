using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.service;
using WebAPI.po;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.utils;
using Microsoft.Extensions.Options;

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase {

        private IAccountService accountService { get; }

		private readonly IOptions<WebAPISettings> configuration;


        public AccountsController(IAccountService accountService, IOptions<WebAPISettings> configuration) {
            this.accountService = accountService;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public string Login([FromBody] Account account) {
            return accountService.Login(account);
        }

        [HttpPost("loginByToken")]
        public string LoginByToken([FromHeader] string authorization) {
            return accountService.LoginByToken(authorization);
        }

        [HttpGet]
        public List<Account> GetAll() {
            return accountService.GetAll();
        }

        [HttpPost("page")]
        public AccountPageDTO GetByPage([FromBody] AccountPagination pagination) {
            return accountService.GetByPage(pagination);
        }

        [HttpGet("{id}")]
        public Account GetById(int id) {
            return accountService.GetById(id);
        }

        [HttpGet("data/{id}")]
        public AccountDTO GetDataById(int id) {
            return accountService.GetDataById(id);
        }

        [HttpGet("accountKey/{key}")]
        public Account GetByAccountKey(string key) {
            return accountService.GetByAccountKey(key);
        }

        [HttpGet("data/accountkey/{key}")]
        public AccountDTO GetDataByAccountKey(string key) {
            return accountService.GetDataByAccountKey(key);
        }

        [HttpPost]
        public long Save([FromBody] AccountDTO account) {
            return account.Id == 0 ? accountService.Save(account) :accountService.Update(account);
        }

        [HttpDelete("{id}")]
        public int Delete(int id) {
            return accountService.Delete(id);
        }
    }
}
