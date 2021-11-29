using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.dto;
using WebAPI.pagination;
using WebAPI.entity;
using WebAPI.service;

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase {

        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService) {
            this.accountService = accountService;
        }

        /// <summary>
        ///  登录
        /// </summary>
        /// <param name="account">
        ///     AccountKey: 账号
        ///     Password: 密码
        /// </param>
        /// <returns>JWT</returns>
        [HttpPost("login")]
        public string Login([FromBody] Account account) {
            return accountService.Login(account);
        }

        /// <summary>
        /// 使用JWT登录
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns>新的JWT</returns>
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
            return account.Id == 0 ? accountService.Save(account) : accountService.Update(account);
        }

        [HttpDelete("{id}")]
        public int Delete(int id) {
            return accountService.Delete(id);
        }
    }
}
