using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CowController : ControllerBase
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
        public IActionResult Add(Cow cow)
        {
            var result = _cowService.Add(cow);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Cow cow)
        {
            var result = _cowService.Delete(cow);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Cow cow)
        {
            var result = _cowService.Update(cow);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getbycowid")]
        public IActionResult Update(int cowId)
        {
            var result = _cowService.GetByCowId(cowId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getusercows")]
        public IActionResult GetUserCows(int id, string securityKey)
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