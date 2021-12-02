﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BullController : Controller
    {
        private readonly IBullService _bullService;

        public BullController(IBullService bullService)
        {
            _bullService = bullService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _bullService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _bullService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getuserbulls")]
        public IActionResult GetUserBulls([FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _bullService.GetUserBulls(id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Bull bull,[FromHeader]int id,[FromHeader]string securityKey)
        {
            
            var result = _bullService.Add(bull,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Bull bull,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _bullService.Delete(bull,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Bull bull,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _bullService.Update(bull,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
       
    }
}