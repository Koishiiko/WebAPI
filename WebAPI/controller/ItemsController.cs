using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.dto;
using WebAPI.po;
using WebAPI.service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {

        private readonly IItemService itemService;

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
