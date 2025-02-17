using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.Models;
using MIS.Services.Project.Api.Repository;

namespace MIS.Services.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerticalsController : ControllerBase
    {
        private readonly IVerticalRepository _verticalsRepository;

        public VerticalsController(IVerticalRepository verticalsRepository)
        {
            _verticalsRepository = verticalsRepository;
        }

        // GET: api/Verticals
        [HttpGet]
        public async Task<IEnumerable<Vertical>> GetVerticals()
        {
            var verticals = await _verticalsRepository.GetVerticals();


            return verticals;
        }

        // GET: api/Verticals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vertical>> GetVertical(int id)
        {
            var vertical = await _verticalsRepository.GetVertical(id);
            if (vertical == null)
            {
                return NotFound();
            }

            return vertical;
        }

        // PUT: api/Verticals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVertical(int id, Vertical vertical)
        {
            if (id != vertical.VerticalId)
            {
                return BadRequest();
            }

            var success = await _verticalsRepository.PutVertical(vertical.VerticalId, vertical);

            if (!_verticalsRepository.VerticalExists(id))
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Verticals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vertical>> PostVertical(Vertical vertical)
        {
            var success = await _verticalsRepository.PostVertical(vertical);
            if (success == null)
            {
                return Conflict();
            }

            return CreatedAtAction("GetVertical", new { id = vertical.VerticalId }, success);
        }

        // DELETE: api/Verticals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vertical>> DeleteVertical(int id)
        {
            var vertical = await _verticalsRepository.GetVertical(id);
            if (vertical == null)
            {
                return NotFound();
            }

            await _verticalsRepository.DeleteVertical(id);
            return vertical;
        }


    }
}
