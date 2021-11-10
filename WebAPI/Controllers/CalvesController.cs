﻿using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalvesController : ControllerBase
    {
        readonly ICalfService _calvesService;

        public CalvesController(ICalfService calvesService)
        {
            _calvesService = calvesService;
        }
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _calvesService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _calvesService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getusercalves")]
        public IActionResult GetUserCalves(int id,string securityKey)
        {
            var result = _calvesService.GetUserCalves(id,securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Calves calves)
        {
            var result = _calvesService.Add(calves);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Calves calves)
        {
            var result = _calvesService.Delete(calves);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Calves calves)
        {
            var result = _calvesService.Update(calves);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
