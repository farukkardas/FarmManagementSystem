﻿using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CowController : Controller
    {
        private readonly ICowService _cowService;
        public CowController(ICowService cowService)
        {
            _cowService = cowService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _cowService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _cowService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Cow cow,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _cowService.Add(cow,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Cow cow,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _cowService.Delete(cow,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Cow cow,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _cowService.Update(cow,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getusercows")]
        public IActionResult GetUserCows([FromHeader]int id,[FromHeader] string securityKey)
        {
           
            
            var result = _cowService.GetUserCows(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }
    }
}