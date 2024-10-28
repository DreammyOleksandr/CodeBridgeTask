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
        public async Task<ActionResult<IEnumerable<DogDTO>>> GetRange(QueryParamsDTO queryParamsDto)
        {
            try
            {
                QueryParams queryParams = _mapper.Map<QueryParams>(queryParamsDto);
                return Ok(_mapper.Map<IEnumerable<DogDTO>>(await _dogManager.GetRangeAsync(queryParams)));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DogDTO>> Create(CreateDogDTO createDogDto)
        {
            try
            {
                await _dogManager.CreateAsync(_mapper.Map<Dog>(createDogDto));
                return StatusCode(StatusCodes.Status201Created, createDogDto);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}