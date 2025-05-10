using Dapper_Test.Models;
using Dapper_Test.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dapper_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController(StateRepository _stateRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var states = await _stateRepository.GetAllAsync();
            return Ok(states);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var state = await _stateRepository.GetByIdAsync(id);
            if (state == null)
                return NotFound();

            return Ok(state);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] State state)
        {
            var result = await _stateRepository.CreateAsync(state);
            return result > 0 ? Ok() : BadRequest("Insert failed");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, State state)
        {
            if (state.Id != id )
                return BadRequest();

            var result = await _stateRepository.UpdateAsync(state);
            return result > 0 ? Ok() : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stateRepository.DeleteAsync(id);
            return result > 0 ? Ok() : NotFound();
        }

    }
}
