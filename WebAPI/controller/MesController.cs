using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.dto;
using WebAPI.entity;
using WebAPI.enums;
using WebAPI.service;

namespace WebAPI.controller {
    [Route("api/[controller]")]
    [ApiController]
    public class MesController : ControllerBase {

        private readonly IMesService mesService;

        public MesController(IMesService mesService) {
            this.mesService = mesService;
        }

        [HttpGet("{type}")]
        public IEnumerable<TestState> GetAll(MesType type) {
            return mesService.GetAll(type);
        }

        [HttpPost]
        public int Update([FromBody] MesStateDTO mesState) {
            return mesService.Update(mesState);
        }
    }
}
