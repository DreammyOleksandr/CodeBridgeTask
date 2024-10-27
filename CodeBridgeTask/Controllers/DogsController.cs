using AutoMapper;
using CodeBridgeTask.BusinessLogic.Managers.DogManager;
using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;
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
        public async Task<ActionResult<IEnumerable<Dog>>> GetAll(QueryParams queryParams)
        {
            try
            {
                return Ok(await _dogManager.GetRange(queryParams));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DogDTO>> Post([FromBody] DogDTO dogDto)
        {
            try
            {
                if(dogDto is null) 
                    return BadRequest();
                
                Dog dog = _mapper.Map<Dog>(dogDto);
                
                if (dog.Id != 0) 
                    return StatusCode(StatusCodes.Status500InternalServerError);
                
                return Ok(await _dogManager.Create(dog));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
