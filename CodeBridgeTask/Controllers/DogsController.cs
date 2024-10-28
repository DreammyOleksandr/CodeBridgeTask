using AutoMapper;
using CodeBridgeTask.BusinessLogic.Managers.DogManager;
using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CodeBridgeTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController(IDogManager dogManager, IMapper mapper) : ControllerBase
    {
        private readonly IDogManager _dogManager = dogManager;
        private readonly IMapper _mapper = mapper;
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Dog>>> GetAll(QueryParams queryParams)
        {
            try
            {
                return Ok(await _dogManager.GetRange(queryParams));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DogDTO>> Post(DogDTO dogDto)
        {
            try
            {
                if(dogDto is null) return BadRequest();
                await _dogManager.Create(_mapper.Map<Dog>(dogDto));
                return Ok(dogDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
