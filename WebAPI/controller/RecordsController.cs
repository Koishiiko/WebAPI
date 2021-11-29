using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.entity;
using WebAPI.service;

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase {

        private readonly IRecordService recordService;

        public RecordsController(IRecordService recordService) {
            this.recordService = recordService;
        }

        [HttpGet]
        public List<Record> GetAll() {
            return recordService.GetAll();
        }
    }
}
