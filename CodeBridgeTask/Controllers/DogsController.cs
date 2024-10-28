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
        public async Task<ActionResult<IEnumerable<Dog>>> GetRange(QueryParamsDTO queryParamsDto)
        {
            try
            {
                QueryParams queryParams = _mapper.Map<QueryParams>(queryParamsDto);
                return Ok(await _dogManager.GetRangeAsync(queryParams));
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
        public async Task<ActionResult<DogDTO>> Create(DogDTO dogDto)
        {
            try
            {
                await _dogManager.CreateAsync(_mapper.Map<Dog>(dogDto));
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
