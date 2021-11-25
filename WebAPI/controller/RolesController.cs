using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.entity;
using WebAPI.pagination;
using WebAPI.dto;
using WebAPI.service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.controller {
	[Route("[controller]")]
	[ApiController]
	public class RolesController : ControllerBase {
		private IRoleService roleService { get; }

		public RolesController(IRoleService roleService) {
			this.roleService = roleService;
		}

		[HttpGet]
		public List<Role> GetAll() {
			return roleService.GetAll();
		}

		[HttpGet("{id}")]
		public RoleDTO GetDataById(int id) {
			return roleService.GetDataById(id);
		}

		[HttpPost("page")]
		public RolePageDTO GetByPage([FromBody] RolePagination pagination) {
			return roleService.GetByPage(pagination);
		}

		[HttpPost]
		public long Save([FromBody] RoleDTO role) {
			return role.Id == 0 ? roleService.Save(role) : roleService.Update(role);
		}

		[HttpDelete("{id}")]
		public int Delete(int id) {
			return roleService.Delete(id);
		}
	}
}
