using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.controller {
    [Route("api/[controller]")]
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

        [HttpGet("{reportId}")]
        public ItemDTO GetByReportId(string reportId) {
            return itemService.getByReportId(reportId);
        }

        [HttpPost]
        public long Save([FromBody] ItemDTO item) {
            return item.Id == 0 ? itemService.Save(item) : itemService.Update(item);
        }

        [HttpDelete("{reportId}")]
        public int Delete(string reportId) {
            return itemService.Delete(reportId);
        }

        [HttpGet("type")]
        public IEnumerable<ItemType> GetTypes() {
            return itemService.GetTypes();
        }
    }
}
