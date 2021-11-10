﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FertilizerController : ControllerBase
    {
        private readonly IFertilizerService _fertilizerService;

        public FertilizerController(IFertilizerService fertilizerService)
        {
            _fertilizerService = fertilizerService;
        }
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _fertilizerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _fertilizerService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Fertilizer fertilizer)
        {
            var result = _fertilizerService.Add(fertilizer);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Fertilizer fertilizer)
        {
            var result = _fertilizerService.Delete(fertilizer);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Fertilizer fertilizer)
        {
            var result = _fertilizerService.Update(fertilizer);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpGet("getuserfertilizers")]
        public IActionResult GetUserFertilizers(int id,string securityKey)
        {
            var result = _fertilizerService.GetUserFertilizers(id,securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}