using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.controller {
	[Route("[controller]")]
	[ApiController]
	public class ItemsController : ControllerBase {

		private IItemService itemService { get; }

		public ItemsController(IItemService itemService) {
			this.itemService = itemService;
		}

		[HttpGet]
		public List<Item> GetByModuleId([FromQuery] string moduleId) {
			return itemService.GetByModuleId(moduleId);
		}

		[HttpGet("form")]
		public List<ItemDTO> GetFormByModuleId([FromQuery] string moduleId) {
			return itemService.GetDataByModuleId(moduleId);
		}

		[HttpGet("{moduleId}/{itemId}")]
		public ItemDTO GetById(string moduleId, string itemId) {
			return itemService.getByItemId(moduleId, itemId);
		}

		[HttpPost]
		public long Save([FromBody] ItemDTO item) {
			return item.Id == 0 ? itemService.Save(item) : itemService.Update(item);
		}

		[HttpDelete("{moduleId}/{itemId}")]
		public int Delete(string moduleId, string itemId) {
			return itemService.Delete(moduleId, itemId);
		}
	}
}
