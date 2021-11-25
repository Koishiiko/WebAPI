using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.service;
using WebAPI.entity;

namespace WebAPI.controller {
    [Route("[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase {

        private IRecordService recordService { get; }

        public RecordsController(IRecordService recordService) {
            this.recordService = recordService;
        }

        [HttpGet]
        public List<Record> GetAll() {
            return recordService.GetAll();
        }

        [HttpGet("{id}")]
        public Record GetById(int id) {
            return recordService.GetById(id);
        }
    }
}
